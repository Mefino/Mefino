using Mefino.LightJson;
using Mefino.Core.IO;
using Mefino.Core.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mefino.LightJson.Serialization;

namespace Mefino.Core
{
    public class WebManifestManager
    {
        // Manifests from GitHub
        internal static readonly Dictionary<string, PackageManifest> s_webManifests = new Dictionary<string, PackageManifest>();

        internal static readonly Dictionary<string, DateTime> s_repoCacheTimes = new Dictionary<string, DateTime>();
        internal static readonly HashSet<string> s_reposInLastResult = new HashSet<string>();

        internal static string MANIFEST_CACHE_FILEPATH => Folders.MEFINO_APPDATA_FOLDER + @"\manifest-cache.json";

        /// <summary>
        /// Check GitHub for new web manifests and reload/save the cache.
        /// </summary>
        public static void UpdateWebManifests()
        {
            LoadWebManifestCache();

            if (TryFetchMefinoGithubPackages())
            {
                // remove packages if repo is missing now
                for (int i = s_webManifests.Count - 1; i >= 0; i--)
                {
                    if (!s_webManifests.Any())
                        break;

                    var pkg = s_webManifests.Values.ElementAt(i);

                    if (s_reposInLastResult.Contains($"{pkg.author} {pkg.repository}"))
                        continue;

                    s_webManifests.Remove(pkg.GUID);
                }
            }

            SaveWebManifestCache();
        }

        #region WHITELIST/BLACKLIST/ETC

        public enum GuidFilterState
        {
            NONE,
            Whitelist,
            Blacklist,
            BrokenList
        }

        public static GuidFilterState GetStateForGuid(string guid)
        {
            if (s_authorWhitelist.Contains(guid.Split(' ')[0]))
                return GuidFilterState.Whitelist;

            if (s_guidBlacklist.Contains(guid))
                return GuidFilterState.Blacklist;

            if (s_guidBrokenList.Contains(guid))
                return GuidFilterState.BrokenList;

            return GuidFilterState.NONE;
        }

        internal static HashSet<string> s_authorWhitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Mefino",
            "sinai-dev",
            "random-facades",
            "Jaakko Kantojärvi",
            "raphendyr",
            "ehaugw",
            "IggyTheMad",
            "SpicerXD",
            "deathrat",
            "Vheos",
            "Zalamaur"
        };

        internal static HashSet<string> s_guidBlacklist = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        internal static HashSet<string> s_guidBrokenList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region MEFINO PACKAGE QUERY

        internal const string GITHUB_PACKAGE_QUERY_URL = @"https://api.github.com/search/repositories?q=%22outward%20mefino%20mod%22%20fork:true%20in:readme&sort=stars&order=desc";

        /// <summary>
        /// Actually fetch and process web packages. 
        /// </summary>
        private static bool TryFetchMefinoGithubPackages()
        {
            var githubQuery = GithubHelper.QueryForMefinoPackages();

            if (githubQuery == null)
            {
                Console.WriteLine("GITHUB QUERY RETURNED NULL! (are you offline?)");
                return false;
            }

            var items = ((JsonValue)githubQuery).AsJsonObject["items"].AsJsonArray;

            foreach (var result in items)
                CheckRepoSearchResult(result);

            //Console.WriteLine($"Found {WebManifestManager.s_cachedWebManifests.Count} Mefino packages!");
            return true;
        }

        /// <summary>
        /// Process a github search result for <c>"outward mefino mod"</c>
        /// </summary>
        internal static void CheckRepoSearchResult(JsonValue queryResult)
        {
            try
            {
                var repoUrl = queryResult["html_url"].AsString;
                var updatedAt = queryResult["updated_at"].AsString;

                var hostUsername = queryResult["owner"].AsJsonObject["login"].AsString;
                var repoName = queryResult["name"].AsString;

                bool isFork = queryResult["fork"].AsBoolean;

                //Console.WriteLine("Checking repository: " + hostUsername + "/" + repoName + " (fork: " + isFork + ")");

                // Mefino itself might be a result, ignore it.
                if (hostUsername == "Mefino" && repoName == "Mefino")
                    return;

                var repoGuid = $"{hostUsername} {repoName}";
                var updated = DateTime.Parse(updatedAt);

                s_reposInLastResult.Add(repoGuid);

                if (s_repoCacheTimes.ContainsKey(repoGuid))
                {
                    if ((DateTime.Now - updated).TotalMinutes < 5)
                    {
                        // This repo was updated less than 5 minutes ago.
                        // GitHub caches the "Raw" CDN for 5 minutes, so we need to wait for it to refresh.

                        return;
                    }

                    if (updated > s_repoCacheTimes[repoGuid])
                    {
                        // Console.WriteLine("Updating manifests in repo " + repoGuid);

                        s_repoCacheTimes[repoGuid] = updated;
                        var query = s_webManifests.Values.Where(it => it.author == hostUsername && it.repository == repoName);
                        if (query.Any())
                        {
                            for (int i = query.Count() - 1; i >= 0; i--)
                            {
                                Console.WriteLine("removing package " + query.ElementAt(i).GUID);
                                s_webManifests.Remove(query.ElementAt(i).GUID);
                            }
                        }
                    }
                    else
                        return;
                }
                else
                    s_repoCacheTimes.Add(repoGuid, updated);

                var manifestPath = repoUrl.Replace("github.com", "raw.githubusercontent.com")
                                 + $"/{queryResult["default_branch"].AsString}"
                                 + $"/mefino-manifest.json";

                // Console.WriteLine("Checking url " + manifestPath);

                var manifestString = WebClientManager.DownloadString(manifestPath);

                if (string.IsNullOrEmpty(manifestString))
                    return;

                ProcessManifestFile(manifestString, hostUsername, repoName, isFork);
            }
            //catch (IOException) { } // 404
            catch (Exception ex)
            {
                Console.WriteLine("Exception parsing manifest(s) from search query result!");
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Process a `mefino-manifest.json` file, which may be a list of manifests or just one.
        /// </summary>
        internal static void ProcessManifestFile(string manifestString, string hostUsername, string repoName, bool isFork)
        {
            var jsonVal = JsonReader.Parse(manifestString);
            if (jsonVal == default)
                return;

            var json = jsonVal.AsJsonObject;
            if (json == null)
                return;

            // Console.WriteLine("processing json " + manifestString);

            var array = json["packages"];
            if (array != JsonValue.Null && array.AsJsonArray is JsonArray packages)
            {
                string definedAuthor;
                var name = json["author"];
                if (name != JsonValue.Null && name.AsString is string)
                    definedAuthor = name.AsString;
                else
                    definedAuthor = null;

                foreach (var entry in packages)
                {
                    var manifest = PackageManifest.FromManifestJson(entry.ToString());

                    if (isFork && !CheckFork(hostUsername, manifest, definedAuthor))
                        continue;

                    if (manifest != null)
                        ProcessManifest(manifest, hostUsername, repoName);
                }
            }
            else
            {
                var manifest = PackageManifest.FromManifestJson(manifestString);

                if (isFork && !CheckFork(hostUsername, manifest))
                    return;

                if (manifest != null)
                    ProcessManifest(manifest, hostUsername, repoName);
            }
        }

        private static bool CheckFork(string hostUsername, PackageManifest manifest, string definedAuthor = null)
        {
            if (string.IsNullOrEmpty(hostUsername))
                return false;

            definedAuthor = definedAuthor ?? manifest.author;

            return string.Equals(hostUsername, definedAuthor, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Process an actual PackageManifest result, either from a list or just a standalone one.
        /// </summary>
        internal static void ProcessManifest(PackageManifest manifest, string hostUsername, string repoName)
        {
            if (manifest == null)
                return;

            if (string.IsNullOrEmpty(manifest.author) || !string.Equals(hostUsername, manifest.author, StringComparison.OrdinalIgnoreCase))
                manifest.author = hostUsername;

            manifest.repository = repoName;

            var state = GetStateForGuid(manifest.GUID);

            if (state == GuidFilterState.Blacklist)
                return;

            if (state == GuidFilterState.BrokenList)
                manifest.m_installState = InstallState.NotWorking;

            if (s_webManifests.ContainsKey(manifest.GUID))
            {
                Console.WriteLine("Duplicate web manifests found! Skipping this one: " + manifest.GUID);
                return;
            }

            s_webManifests.Add(manifest.GUID, manifest);

            if (LocalPackageManager.TryGetInstalledPackage(manifest.GUID) is PackageManifest installed)
            {
                installed.m_installState = installed.CompareVersionAgainst(manifest);
            }
        }

        /// <summary>
        /// Load the cached web manifests from disk.
        /// </summary>
        internal static void LoadWebManifestCache()
        {
            s_webManifests.Clear();

            if (!File.Exists(MANIFEST_CACHE_FILEPATH))
                return;

            try
            {
                JsonValue manifests = JsonReader.ParseFile(MANIFEST_CACHE_FILEPATH);

                var input = manifests.AsJsonObject;

                var times = input["cachetimes"].AsJsonObject;

                if (times != null)
                {
                    s_repoCacheTimes.Clear();

                    foreach (var entry in times)
                    {
                        var time = entry.Value.AsDateTime;

                        if (time == null)
                            continue;

                        if (s_repoCacheTimes.ContainsKey(entry.Key))
                            continue;

                        s_repoCacheTimes.Add(entry.Key, (DateTime)time);
                    }
                }

                var items = input["manifests"].AsJsonArray;

                foreach (var entry in items)
                {
                    var manifest = PackageManifest.FromManifestJson(entry.AsJsonObject.ToString());

                    if (manifest == default)
                        continue;

                    if (s_webManifests.ContainsKey(manifest.GUID))
                    {
                        Console.WriteLine("Duplicate manifest in web cache! Skipping: " + manifest.GUID);
                        continue;
                    }

                    manifest.m_installState = InstallState.NotInstalled;
                    s_webManifests.Add(manifest.GUID, manifest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception parsing manifest file cache!");
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Save the cached web manifests to disk.
        /// </summary>
        internal static void SaveWebManifestCache()
        {
            var manifests = new JsonArray();
            foreach (var entry in s_webManifests)
                manifests.Add(entry.Value.ToJsonObject());

            var cachetimes = new JsonObject();
            foreach (var entry in s_repoCacheTimes)
                cachetimes.Add(entry.Key, entry.Value);

            var output = new JsonObject
            {
                { "manifests", manifests },
                { "cachetimes", cachetimes }
            };

            IOHelper.CreateDirectory(Folders.MEFINO_OTWFOLDER_PATH);

            if (File.Exists(MANIFEST_CACHE_FILEPATH))
                File.Delete(MANIFEST_CACHE_FILEPATH);

            File.WriteAllText(MANIFEST_CACHE_FILEPATH, output.ToString(true));
        }

        #endregion
    }
}

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

        internal static string MANIFEST_CACHE_FILEPATH => Folders.MEFINO_APPDATA_FOLDER + @"\manifest-cache.json";

        /// <summary>
        /// Check GitHub for new web manifests and reload/save the cache.
        /// </summary>
        public static void UpdateWebManifests()
        {
            LoadWebManifestCache();

            TryFetchMefinoGithubPackages();

            SaveWebManifestCache();
        }

        public static HashSet<string> AcceptedTags
        {
            get
            {
                if (s_acceptedTags == null)
                {
                    s_acceptedTags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var value in Enum.GetValues(typeof(PackageTags)))
                        s_acceptedTags.Add(value.ToString());
                }
                return s_acceptedTags;
            }
        }
        private static HashSet<string> s_acceptedTags;

        public static bool IsValidTag(string tag, bool showLibraries = true)
        {
            if (!showLibraries && string.Equals(tag, "library", StringComparison.OrdinalIgnoreCase))
                return false;

            return AcceptedTags.Contains(tag);
        }

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

        // ======= github Package query ======= //

        internal const string GITHUB_PACKAGE_QUERY_URL = @"https://api.github.com/search/repositories?q=""outward mefino mod""%20in:readme&sort=stars&order=desc";

        /// <summary>
        /// Actually fetch and process web packages. 
        /// </summary>
        private static void TryFetchMefinoGithubPackages()
        {
            var githubQuery = GithubHelper.QueryForMefinoPackages();

            if (githubQuery == null)
            {
                Console.WriteLine("GITHUB QUERY RETURNED NULL! (are you offline?)");
                return;
            }

            var items = ((JsonValue)githubQuery).AsJsonObject["items"].AsJsonArray;

            foreach (var result in items)
            {
                CheckRepoSearchResult(result);
            }

            //Console.WriteLine($"Found {WebManifestManager.s_cachedWebManifests.Count} Mefino packages!");
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

                var author = queryResult["owner"].AsJsonObject["login"].AsString;
                var repoName = queryResult["name"].AsString;

                //Console.WriteLine("checking repo " + author + " " + repoName);

                // Mefino itself might be a result, ignore it.
                if (author == "Mefino" && repoName == "Mefino")
                    return;

                var repoGuid = $"{author} {repoName}";
                var updated = DateTime.Parse(updatedAt);

                if (s_repoCacheTimes.ContainsKey(repoGuid))
                {
                    if (updated > s_repoCacheTimes[repoGuid])
                    {
                        s_repoCacheTimes[repoGuid] = updated;
                        var query = s_webManifests.Values.Where(it => it.author == author && it.repository == repoName);
                        if (query.Any())
                        {
                            for (int i = query.Count() - 1; i >= 0; i--)
                                s_webManifests.Remove(query.ElementAt(i).GUID);
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

                ProcessManifestFile(manifestString, author, repoName);
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
        internal static void ProcessManifestFile(string manifestString, string authorName, string repoName)
        {
            var json = JsonReader.Parse(manifestString);

            if (json == default)
                return;

            try
            {
                var list = json["packages"].AsJsonArray;
                if (list != null)
                {
                    foreach (var entry in list)
                    {
                        var manifest = PackageManifest.FromManifestJson(entry.ToString());
                        if (manifest != null)
                        {
                            ProcessManifest(manifest, authorName, repoName);
                        }
                    }
                }
                else throw new Exception();
            }
            catch
            {
                var manifest = PackageManifest.FromManifestJson(manifestString);
                if (manifest != null)
                {
                    ProcessManifest(manifest, authorName, repoName);
                }
            }
        }

        /// <summary>
        /// Process an actual PackageManifest result, either from a list or just a standalone one.
        /// </summary>
        internal static void ProcessManifest(PackageManifest manifest, string author, string repoName)
        {
            if (manifest == null)
                return;

            manifest.author = author;
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

            if (LocalPackageManager.TryGetInstalledPackage(manifest.GUID) is PackageManifest installed
                    && manifest.IsGreaterVersionThan(installed))
            {
                installed.m_installState = InstallState.Outdated;
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
    }
}

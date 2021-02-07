using Mefino.LightJson;
using Mefino.IO;
using Mefino.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Core
{
    public class WebManifestManager
    {
        // Manifests from GitHub (cached on disk)
        internal static readonly Dictionary<string, PackageManifest> s_cachedWebManifests = new Dictionary<string, PackageManifest>();

        internal static string MANIFEST_CACHE_FILEPATH => Folders.MEFINO_APPDATA_FOLDER + @"\manifest-cache.json";

        public static void UpdateWebManifests()
        {
            LoadWebManifestCache();

            TryFetchMefinoGithubPackages();

            SaveWebManifestCache();
        }

        // ======= github Package query ======= //

        internal const string GITHUB_PACKAGE_QUERY_URL = @"https://api.github.com/search/repositories?q=""outward mefino mod""%20in:readme&sort=stars&order=desc";

        public static void TryFetchMefinoGithubPackages()
        {
            var githubQuery = GithubHelper.QueryForMefinoPackages();

            if (githubQuery == null)
            {
                Console.WriteLine("GITHUB QUERY RETURNED NULL! (are you offline?)");
                return;
            }

            WebManifestManager.s_cachedWebManifests.Clear();

            var items = ((JsonValue)githubQuery).AsJsonObject["items"].AsJsonArray;

            foreach (var entry in items)
            {
                CheckAndAddQueryResult(entry);

                //if (result == null)
                //    continue;

                //if (LocalPackageManager.TryGetInstalledPackage(result.GUID) is PackageManifest installed
                //        && result.IsGreaterVersionThan(installed))
                //{
                //    installed.m_installState = InstallState.Outdated;
                //}
            }

            //Console.WriteLine($"Found {WebManifestManager.s_cachedWebManifests.Count} Mefino packages!");
        }
        internal static PackageManifest CheckAndAddQueryResult(JsonValue queryResult)
        {
            try
            {
                var repoUrl = queryResult["html_url"].AsString;
                var updatedAt = queryResult["updated_at"].AsString;

                var author = queryResult["owner"].AsJsonObject["login"].AsString;
                var repoName = queryResult["name"].AsString;

                var guid = $"{author} {repoName}";

                //Console.WriteLine("Checking github result '" + guid + "'");

                if (WebManifestManager.s_cachedWebManifests.ContainsKey(guid))
                {
                    var existing = WebManifestManager.s_cachedWebManifests[guid];

                    if (!existing.IsManifestCachedSince(updatedAt))
                        WebManifestManager.s_cachedWebManifests.Remove(guid);
                    else
                    {
                        //Console.WriteLine("Existing cached manifest for '" + guid + "' is up to date.");
                        return existing;
                    }
                }

                var manifestPath = repoUrl.Replace("github.com", "raw.githubusercontent.com")
                                 + $"/{queryResult["default_branch"].AsString}"
                                 + $"/manifest.json";

                // Console.WriteLine("Checking url " + manifestPath);

                var manifestString = WebClientManager.DownloadString(manifestPath);

                if (string.IsNullOrEmpty(manifestString))
                    return null;

                var manifest = PackageManifest.FromManifestJson(manifestString);

                if (manifest == default)
                {
                    Console.WriteLine("Unable to parse query result at '" + manifestPath + "'");
                    return default;
                }

                manifest.m_manifestCachedTime = updatedAt;
                manifest.author = author;
                manifest.name = repoName;

                WebManifestManager.s_cachedWebManifests.Add(manifest.GUID, manifest);

                return manifest;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception parsing manifest from query result!");
                Console.WriteLine(ex);

                return null;
            }
        }


        // ======== loading/saving ========
        internal static void LoadWebManifestCache()
        {
            s_cachedWebManifests.Clear();

            if (!File.Exists(MANIFEST_CACHE_FILEPATH))
                return;

            try
            {
                var manifests = LightJson.Serialization.JsonReader.ParseFile(MANIFEST_CACHE_FILEPATH);

                var input = manifests.AsJsonObject;

                var items = input["manifests"].AsJsonArray;

                foreach (var entry in items)
                {
                    var manifest = PackageManifest.FromManifestJson(entry.AsJsonObject.ToString());

                    if (manifest == default)
                        continue;

                    if (s_cachedWebManifests.ContainsKey(manifest.GUID))
                    {
                        Console.WriteLine("Duplicate manifest in web cache! Skipping: " + manifest.GUID);
                        continue;
                    }

                    manifest.m_installState = InstallState.NotInstalled;
                    s_cachedWebManifests.Add(manifest.GUID, manifest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception parsing manifest file cache!");
                Console.WriteLine(ex);
            }
        }

        internal static void SaveWebManifestCache()
        {
            var array = new JsonArray();

            foreach (var entry in s_cachedWebManifests)
                array.Add(entry.Value.ToJsonObject());

            var output = new JsonObject
            {
                { "manifests", array }
            };

            Directory.CreateDirectory(Folders.MEFINO_FOLDER_PATH);

            if (File.Exists(MANIFEST_CACHE_FILEPATH))
                File.Delete(MANIFEST_CACHE_FILEPATH);

            File.WriteAllText(MANIFEST_CACHE_FILEPATH, output.ToString(true));
        }
    }
}

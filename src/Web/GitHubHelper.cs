using Mefino.LightJson;
using Mefino.LightJson.Serialization;
using Mefino.Loader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Loader.Web
{
    public static class GitHubHelper
    {
        internal const string QUERY_URL = @"https://api.github.com/search/repositories?q=""outward mefino mod""%20in:readme&sort=stars&order=desc";

        public static void TryFetchGithubPackages()
        {
            var githubQuery = FetchSearchResults();

            if (githubQuery == null)
            {
                Console.WriteLine("GITHUB QUERY RETURNED NULL (are you offline?)!");
                return;
            }

            var items = ((JsonValue)githubQuery).AsJsonObject["items"].AsJsonArray;

            Console.WriteLine($"Found {items.Count} mods on GitHub, checking...");

            foreach (var entry in items)
            {
                var result = CheckQueryResult(entry);

                if (result is PackageManifest webManifest)
                {
                    ManifestManager.s_cachedWebManifests.Add(webManifest.GUID, webManifest);

                    Console.WriteLine($"Updated manifest for package '{webManifest.GUID}'");
                }
            }
        }

        public static JsonValue? FetchSearchResults()
        {
            string query;
            try
            {
                WebClientManager.Reset();
                query = WebClientManager.WebClient.DownloadString(QUERY_URL);
            }
            catch
            {
                return null;
            }

            if (!string.IsNullOrEmpty(query))
                return JsonReader.Parse(query);

            return null;
        }

        internal static PackageManifest CheckQueryResult(JsonValue queryResult)
        {
            try
            {
                var repoUrl = queryResult["html_url"].AsString;
                var updatedAt = queryResult["updated_at"].AsString;

                var author = queryResult["owner"].AsJsonObject["login"].AsString;
                var repoName = queryResult["name"].AsString;

                var guid = $"{author}.{repoName}";

                Console.WriteLine("Checking github result '" + guid + "'");

                if (ManifestManager.s_cachedWebManifests.ContainsKey(guid))
                {
                    var existing = ManifestManager.s_cachedWebManifests[guid];

                    var overwrite = !existing.IsManifestCachedSince(updatedAt);

                    if (overwrite)
                        ManifestManager.s_cachedWebManifests.Remove(guid);
                    else
                    {
                        Console.WriteLine("Existing cached manifest for '" + guid + "' is up to date.");
                        return null;
                    }
                }

                var manifestPath = repoUrl.Replace("github.com", "raw.githubusercontent.com")
                                 + $"/{queryResult["default_branch"].AsString}"
                                 + $"/manifest.json";

                // Console.WriteLine("Checking url " + manifestPath);

                var manifestString = WebClientManager.WebClient.DownloadString(manifestPath);

                if (string.IsNullOrEmpty(manifestString))
                    return null;

                var manifest = PackageManifest.FromManifestJson(manifestString);

                if (manifest == default)
                {
                    Console.WriteLine("Unable to parse query result at '" + manifestPath + "'");
                    return default;
                }

                manifest.m_manifestCachedTime = updatedAt;
                manifest.Author = author;
                manifest.PackageName = repoName;

                return manifest;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception parsing manifest from query result!");
                Console.WriteLine(ex);

                return null;
            }
        }
    }
}

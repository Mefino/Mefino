using Mefino.LightJson;
using Mefino.Loader.IO;
using Mefino.Loader.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Loader.Core
{
    public class ManifestManager
    {
        // Manifests from GitHub (cached on disk)
        internal static readonly Dictionary<string, PackageManifest> s_cachedWebManifests = new Dictionary<string, PackageManifest>();

        internal static string MANIFEST_CACHE_FILENAME => MefinoLoader.MEFINO_FOLDER_PATH + @"\manifest-cache.json";

        internal static void RefreshManifestCache(bool onlyUseLocalCache = false)
        {
            // RefreshInstalledMods();

            LoadManifestCache();

            if (!onlyUseLocalCache)
                GitHubHelper.TryFetchGithubPackages();

            SaveManifestCache();
        }

        internal static void LoadManifestCache()
        {
            if (File.Exists(MANIFEST_CACHE_FILENAME))
            {
                s_cachedWebManifests.Clear();

                var manifests = LightJson.Serialization.JsonReader.ParseFile(MANIFEST_CACHE_FILENAME);

                try
                {
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

                        s_cachedWebManifests.Add(manifest.GUID, manifest);
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Exception parsing manifest file cache!");
                    Console.WriteLine(ex);
                }
            }
        }

        internal static void SaveManifestCache()
        {
            var array = new JsonArray();

            foreach (var entry in s_cachedWebManifests)
                array.Add(entry.Value.ToJsonObject());

            var output = new JsonObject
            {
                { "manifests", array }
            };

            Directory.CreateDirectory(MefinoLoader.MEFINO_FOLDER_PATH);

            if (File.Exists(MANIFEST_CACHE_FILENAME))
                File.Delete(MANIFEST_CACHE_FILENAME);

            File.WriteAllText(MANIFEST_CACHE_FILENAME, output.ToString(true));
        }
    }
}

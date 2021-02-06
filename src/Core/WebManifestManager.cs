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

        internal static string MANIFEST_CACHE_FILEPATH => Folders.MEFINO_FOLDER_PATH + @"\manifest-cache.json";

        public static void UpdateWebManifests()
        {
            LoadWebManifestCache();

            GithubHelper.TryFetchMefinoGithubPackages();

            SaveWebManifestCache();
        }

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

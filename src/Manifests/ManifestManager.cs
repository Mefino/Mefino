using Mefino.LightJson;
using Mefino.Loader.IO;
using Mefino.Loader.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Loader.Manifests
{
    public class ManifestManager
    {
        // Manifests from GitHub (cached on disk)
        internal static readonly Dictionary<string, PackageManifest> s_cachedWebManifests = new Dictionary<string, PackageManifest>();

        // Actual installed manifests, serialized from mod folders.
        internal static readonly Dictionary<string, PackageManifest> s_installedManifests = new Dictionary<string, PackageManifest>();

        internal const string MANIFEST_CACHE_FILENAME = @"manifestcache.json";
        internal const string MANIFEST_FILENAME = "manifest.json";
        internal const string MEFINO_PACKAGE_NAME = "mefino-package.zip";

        internal static void RefreshModList(bool onlyUseLocalCache = false)
        {
            GetInstalledMods();

            LoadManifestCache();
            if (!onlyUseLocalCache)
                GitHubHelper.TryFetchGithubPackages();
            SaveManifestCache();
        }

        public static void TryInstallPackage(PackageManifest manifest)
        {
            if (manifest == default || manifest.GUID.Trim() == ".")
                return;

            if (s_installedManifests.ContainsKey(manifest.GUID))
            {
                var existing = s_installedManifests[manifest.GUID];
                if (existing.IsGreaterVersionThan(manifest, true))
                {
                    Console.WriteLine("Installed package is already greater or equal version, skipping install.");
                    return;
                }
                else
                    Console.WriteLine("Updating package to new version...");
            }

            try
            {
                string folderName;
                if (!string.IsNullOrEmpty(manifest.OverrideFolderName))
                    folderName = manifest.OverrideFolderName;
                else
                    folderName = manifest.PackageName;

                var dirPath = $@"{MefinoLoader.OutwardFolderPath}\BepInEx\plugins\{folderName}";

                var zipUrl = $"{manifest.GithubURL}/releases/latest/download/{MEFINO_PACKAGE_NAME}";

                if (ZipHelper.DownloadAndExtractZip(zipUrl, dirPath))
                {
                    var manifestPath = $@"{dirPath}\manifest.json";

                    if (File.Exists(manifestPath))
                        File.Delete(manifestPath);

                    File.WriteAllText(manifestPath, manifest.ToJsonObject().ToString(true));

                    Console.WriteLine($"Installed package: {manifest.GUID} {manifest.Version}");
                }
                else
                    throw new Exception("Zip download/extraction failed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception isntalling package '" + manifest.GUID + "'");
                Console.WriteLine($"{ex.GetType()}: {ex.Message}");
            }
        }

        internal static void GetInstalledMods()
        {
            s_installedManifests.Clear();

            var pluginsPath = MefinoLoader.OutwardFolderPath + @"\BepInEx\plugins";

            if (!Directory.Exists(pluginsPath))
                return;

            foreach (var dir in Directory.GetDirectories(pluginsPath))
            {
                var manifestPath = dir + $@"\{MANIFEST_FILENAME}";
                if (File.Exists(manifestPath))
                {
                    try
                    {
                        var json = File.ReadAllText(manifestPath);

                        var manifest = PackageManifest.FromManifestJson(json);

                        if (manifest == default)
                        {
                            Console.WriteLine($"Unable to parse manifest file: '{manifestPath}'");
                            continue;
                        }

                        if (s_installedManifests.ContainsKey(manifest.GUID))
                        {
                            Console.WriteLine("Duplicate manifests trying to load from install! Skipping: " + manifest.GUID);
                            continue;
                        }

                        s_installedManifests.Add(manifest.GUID, manifest);

                        Console.WriteLine($"Found installed package: {manifest.GUID}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception loading installed manifest json!");
                        Console.WriteLine(ex);
                    }
                }
            }
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

            if (File.Exists(MANIFEST_CACHE_FILENAME))
                File.Delete(MANIFEST_CACHE_FILENAME);

            File.WriteAllText(MANIFEST_CACHE_FILENAME, output.ToString(true));
        }
    }
}

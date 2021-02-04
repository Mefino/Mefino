using Mefino.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mefino.Core
{
    public static class MefinoPackageManager
    {
        internal const string PKG_MANIFEST_FILENAME = "manifest.json";
        internal const string PKG_MEFINO_PACKAGE_NAME = "mefino-package.zip";

        // Actual installed manifests, serialized from mod folders.
        internal static readonly Dictionary<string, PackageManifest> s_installedManifests = new Dictionary<string, PackageManifest>();

        internal static readonly Dictionary<string, PackageManifest> s_disabledManifests = new Dictionary<string, PackageManifest>();

        internal static void RefreshInstalledMods()
        {
            if (!Mefino.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("Cannot retrieve installed mods as Outward folder path is not set!");
                return;
            }

            s_installedManifests.Clear();

            var pluginsPath = Mefino.OUTWARD_PLUGINS;
            if (Directory.Exists(pluginsPath))
            {
                foreach (var dir in Directory.GetDirectories(pluginsPath))
                    LoadLocalPackageManifest(dir);
            }

            s_disabledManifests.Clear();
            if (Directory.Exists(Mefino.MEFINO_DISABLED_FOLDER))
            {
                foreach (var dir in Directory.GetDirectories(Mefino.MEFINO_DISABLED_FOLDER))
                    LoadLocalPackageManifest(dir, true);
            }
        }

        private static void LoadLocalPackageManifest(string dir, bool isInDisabledFolder = false)
        {
            var manifestPath = dir + $@"\{PKG_MANIFEST_FILENAME}";
            if (File.Exists(manifestPath))
            {
                try
                {
                    var json = File.ReadAllText(manifestPath);

                    var manifest = PackageManifest.FromManifestJson(json);

                    if (manifest == default)
                    {
                        Console.WriteLine($"Unable to parse manifest file: '{manifestPath}'");
                        return;
                    }

                    if (!isInDisabledFolder)
                    {
                        if (s_installedManifests.ContainsKey(manifest.GUID))
                        {
                            Console.WriteLine("Duplicate manifests found! Skipping: " + manifest.GUID);
                            return;
                        }

                        s_installedManifests.Add(manifest.GUID, manifest);
                    }
                    else
                    {
                        if (s_installedManifests.ContainsKey(manifest.GUID))
                        {
                            Console.WriteLine("Duplicate package found in both installed and Disabled folders! Removing disabled: " + manifest.GUID);
                            TryRemovePackage(manifest.GUID, true);
                            return;
                        }

                        if (s_disabledManifests.ContainsKey(manifest.GUID))
                        {
                            Console.WriteLine("Duplicate package GUID found in Disabled folder: " + manifest.GUID);
                            return;
                        }

                        s_disabledManifests.Add(manifest.GUID, manifest);
                    }

                    //Console.WriteLine($"Found installed package: {manifest.GUID} (disabled: {isInDisabledFolder})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception loading installed manifest json!");
                    Console.WriteLine(ex);
                }
            }
        }

        // ======== Install ========

        public static void TryInstallPackage(string guid)
        {
            if (!Mefino.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("Outward folder is not set! Cannot install package.");
                return;
            }

            if (s_disabledManifests.ContainsKey(guid))
            {
                Console.WriteLine("Package '" + guid + "' is installed but disabled, trying to enable...");
                TryEnablePackage(guid);
                return;
            }

            ManifestManager.s_cachedWebManifests.TryGetValue(guid, out PackageManifest manifest);

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
            }
            
            Console.WriteLine($"Installing {manifest.GUID} version {manifest.Version}");

            try
            {
                var dirPath = $@"{Mefino.OUTWARD_PLUGINS}\{manifest.InstallFolder}";

                var tempFile = TemporaryFile.CreateFile();

                var zipUrl = $"{manifest.GithubURL}/releases/latest/download/{PKG_MEFINO_PACKAGE_NAME}";

                Web.WebClientManager.DownloadFileAsync(zipUrl, tempFile);

                while (!Web.WebClientManager.WebClient.IsBusy)
                    Thread.Sleep(20);
                while (Web.WebClientManager.WebClient.IsBusy)
                    Thread.Sleep(20);

                if (ZipHelper.ExtractZip(tempFile, dirPath))
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

        // ======== Uninstall / Deletion ========

        internal static void TryRemovePackage(string guid)
        {
            if (s_installedManifests.ContainsKey(guid))
                TryRemovePackage(guid, false);
            else if (s_disabledManifests.ContainsKey(guid))
                TryRemovePackage(guid, true);
            else
                Console.WriteLine("Package '" + guid + "' does not seem to be installed.");
        }

        internal static void TryRemovePackage(string guid, bool inDisabledFolder = false)
        {
            if (!inDisabledFolder)
            {
                if (!s_installedManifests.ContainsKey(guid))
                {
                    Console.WriteLine("Package '" + guid + "' is not installed!");
                    return;
                }

                var package = s_installedManifests[guid];

                var dir = Mefino.OUTWARD_PLUGINS + $@"\{package.InstallFolder}";

                if (TryDeleteDirectory(dir))
                    s_installedManifests.Remove(guid);
                else
                    Console.WriteLine("Unable to remove package '" + guid + "' at path '" + dir + "'!");
            }
            else
            {
                if (!s_disabledManifests.ContainsKey(guid))
                {
                    Console.WriteLine("Package '" + guid + "' is not installed!");
                    return;
                }

                var package = s_disabledManifests[guid];

                var dir = Mefino.MEFINO_DISABLED_FOLDER + $@"\{package.InstallFolder}";

                if (TryDeleteDirectory(dir))
                    s_disabledManifests.Remove(guid);
                else
                    Console.WriteLine("Unable to remove package '" + guid + "' at path '" + dir + "'!");
            }
        }

        internal static bool TryDeleteDirectory(string dir)
        {
            if (!Directory.Exists(dir))
                return false;

            try
            {
                Directory.Delete(dir, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ======== Enable/Disable ========

        public static void TryEnablePackage(string guid)
        {
            if (s_installedManifests.ContainsKey(guid))
            {
                Console.WriteLine("Package '" + guid + "' is already enabled!");
                return;
            }

            if (!s_disabledManifests.ContainsKey(guid))
            {
                Console.WriteLine("Cannot enable package '" + guid + "' as it is not disabled!");
                return;
            }

            var package = s_disabledManifests[guid];

            string toDir = Mefino.OUTWARD_PLUGINS + $@"\{package.InstallFolder}";
            string fromDir = Mefino.MEFINO_DISABLED_FOLDER + $@"\{package.InstallFolder}";

            if (TryMoveDirectory(guid, fromDir, toDir))
            {
                Console.WriteLine("Enabled package: " + guid);
                s_disabledManifests.Remove(guid);
                s_installedManifests.Add(guid, package);
            }
        }

        public static void TryDisablePackage(string guid)
        {
            if (s_disabledManifests.ContainsKey(guid))
            {
                Console.WriteLine("Package '" + guid + "' is already disabled!");
                return;
            }

            if (!s_installedManifests.ContainsKey(guid))
            {
                Console.WriteLine("Cannot disable package '" + guid + "' as it is not installed!");
                return;
            }

            var package = s_installedManifests[guid];

            string toDir = Mefino.MEFINO_DISABLED_FOLDER + $@"\{package.InstallFolder}";
            string fromDir = Mefino.OUTWARD_PLUGINS + $@"\{package.InstallFolder}";

            if (TryMoveDirectory(guid, fromDir, toDir))
            {
                Console.WriteLine("Disable package: " + guid);
                s_installedManifests.Remove(guid);
                s_disabledManifests.Add(guid, package);
            }
        }

        private static bool TryMoveDirectory(string guid, string fromDir, string toDir)
        {
            if (Directory.Exists(toDir))
            {
                Console.WriteLine($"Trying to disable '{guid}' but a folder already exists at: {toDir}! Aborting!");
                return false;
            }

            if (!Directory.Exists(fromDir))
            {
                Console.WriteLine($"Trying to disable '{guid}' but no folder exists at: {fromDir}! Aborting!");
                return false;
            }

            try
            {
                Directory.Move(fromDir, toDir);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

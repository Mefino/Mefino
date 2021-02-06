using Mefino.GUI;
using Mefino.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.Core
{
    public static class LocalPackageManager
    {
        internal const string PKG_MANIFEST_FILENAME = "manifest.json";
        internal const string PKG_MEFINO_PACKAGE_NAME = "mefino-package.zip";

        // Actual installed manifests, serialized from mod folders.
        internal static readonly Dictionary<string, PackageManifest> s_enabledPackages = new Dictionary<string, PackageManifest>();
        internal static readonly Dictionary<string, PackageManifest> s_disabledPackages = new Dictionary<string, PackageManifest>();

        public static PackageManifest TryGetInstalledPackage(string guid)
        {
            s_enabledPackages.TryGetValue(guid, out PackageManifest package);

            if (package == null)
                s_disabledPackages.TryGetValue(guid, out package);

            return package;
        }

        // ===== installed package management =====

        internal static void RefreshInstalledPackages()
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("Cannot retrieve installed mods as Outward folder path is not set!");
                return;
            }

            // load enabled plugins
            s_enabledPackages.Clear();
            var pluginsPath = Folders.OUTWARD_PLUGINS;
            if (Directory.Exists(pluginsPath))
            {
                foreach (var dir in Directory.GetDirectories(pluginsPath))
                    LoadLocalPackageManifest(dir, false);
            }

            // load disabled plugins
            s_disabledPackages.Clear();
            if (Directory.Exists(Folders.MEFINO_DISABLED_FOLDER))
            {
                foreach (var dir in Directory.GetDirectories(Folders.MEFINO_DISABLED_FOLDER))
                    LoadLocalPackageManifest(dir, true);
            }

            // update dependency statuses now that all packages loaded
            foreach (var package in s_enabledPackages.Values)
            {
                if (package.Dependencies == null || !package.Dependencies.Any())
                    continue;

                foreach (var dep in package.Dependencies)
                {
                    if (s_disabledPackages.ContainsKey(dep))
                        package.m_installState = InstallState.MissingDependency;
                    else if (!s_enabledPackages.ContainsKey(dep))
                        package.m_installState = InstallState.MissingDependency;
                }
            }
        }

        private static void LoadLocalPackageManifest(string dir, bool disabled)
        {
            var manifestPath = dir + $@"\{PKG_MANIFEST_FILENAME}";
            if (!File.Exists(manifestPath))
            {
                Console.WriteLine($"Unable to find expected manifest.json at path '" + dir + "'");
                return;
            }

            try
            {
                var json = File.ReadAllText(manifestPath);

                var manifest = PackageManifest.FromManifestJson(json);

                if (manifest == default)
                {
                    Console.WriteLine($"Unable to parse manifest file: '{manifestPath}'");
                    return;
                }

                manifest.m_isDisabled = disabled;

                if (!disabled)
                {
                    if (s_enabledPackages.ContainsKey(manifest.GUID))
                    {
                        Console.WriteLine("Duplicate manifests found! Skipping: " + manifest.GUID);
                        return;
                    }

                    manifest.m_installState = InstallState.Installed;
                    s_enabledPackages.Add(manifest.GUID, manifest);
                }
                else
                {
                    if (s_enabledPackages.ContainsKey(manifest.GUID))
                    {
                        Console.WriteLine("Duplicate package found in both installed and Disabled folders! Removing disabled: " + manifest.GUID);
                        TryRemovePackage(manifest);
                        return;
                    }

                    if (s_disabledPackages.ContainsKey(manifest.GUID))
                    {
                        Console.WriteLine("Duplicate package GUID found in Disabled folder: " + manifest.GUID);
                        return;
                    }

                    manifest.m_installState = InstallState.Installed;
                    s_disabledPackages.Add(manifest.GUID, manifest);
                }

                //Console.WriteLine($"Found installed package: {manifest.GUID} (disabled: {isInDisabledFolder})");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception loading installed manifest json!");
                Console.WriteLine(ex);
            }
        }

        // true = continue or no dependency, false = cancel 
        internal static bool CheckDependencyBeforeRemoval(PackageManifest package, bool disabling)
        {
            var dependencies = package.GetDependantEnabledPackagesOfThis();

            bool ret = true;

            if (dependencies.Any())
            {
                string msg = "";
                foreach (var dep in dependencies)
                {
                    msg += $"\n{dep}";
                }

                string msg2 = disabling ? "disable" : "remove";

                var msgResult = MessageBox.Show("This package is depended upon by the following packages:" +
                    $"\n{msg}" +
                    $"\n\nReally {msg2} it?",
                    "Warning",
                    MessageBoxButtons.OKCancel);

                if (msgResult == DialogResult.Cancel)
                    ret = false;
            }

            return ret;
        }

        // ======== Install ========

        public static bool TryInstallWebPackage(string guid)
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("Outward folder is not set! Cannot install package.");
                return false;
            }

            WebManifestManager.s_cachedWebManifests.TryGetValue(guid, out PackageManifest webManifest);

            if (webManifest == null || string.IsNullOrEmpty(webManifest.GUID?.Trim()))
            {
                if (s_disabledPackages.ContainsKey(guid))
                {
                    Console.WriteLine("Could not find online package by GUID '" + guid + ", but a disabled package exists with that GUID, enabling.");
                    TryEnablePackage(guid);
                    return true;
                }

                Console.WriteLine("Could not find web package by GUID '" + guid + "'");
                return false;
            }

            var existing = TryGetInstalledPackage(guid);

            if (existing != null && existing.IsGreaterVersionThan(webManifest))
            {
                if (existing.m_isDisabled)
                    TryEnablePackage(guid);

                //Console.WriteLine("Installed package is already greater or equal version, skipping install.");
                return true;
            }

            if (webManifest.Dependencies != null && webManifest.Dependencies.Length > 0)
            {
                int i = 1;
                int count = webManifest.Dependencies.Length;
                foreach (var dependency in webManifest.Dependencies)
                {
                    MefinoGUI.SetProgressMessage($"Installing dependency {i} of {count}: {dependency}");

                    if (!TryInstallWebPackage(dependency))
                    {
                        //Console.WriteLine("Installing dependency '" + dependency + "' failed!");
                        var msgResult = MessageBox.Show("Installing dependency '" + dependency + "' failed!\n\nContinue anyway?", "Depdency failed!", MessageBoxButtons.OKCancel);
                        if (msgResult == DialogResult.OK)
                            continue;
                        else
                            return false;
                    }

                    i++;
                }
            }

            // check that the package itself wasn't one of the dependencies.
            existing = TryGetInstalledPackage(guid);
            if (!webManifest.IsGreaterVersionThan(existing))
                return true;

            MefinoGUI.SetProgressMessage($"Installing package {webManifest.GUID}");

            return DownloadAndInstallPackage(webManifest);
        }

        internal static bool DownloadAndInstallPackage(PackageManifest manifest)
        {
            Console.WriteLine($"Installing {manifest.GUID} version {manifest.Version}");

            try
            {
                var dirPath = $@"{Folders.OUTWARD_PLUGINS}\{manifest.InstallFolder}";

                var tempFile = TemporaryFile.CreateFile();

                var zipUrl = $"{manifest.GithubURL}/releases/latest/download/{PKG_MEFINO_PACKAGE_NAME}";

                Web.WebClientManager.DownloadFileAsync(zipUrl, tempFile);

                while (Web.WebClientManager.IsBusy)
                {
                    Thread.Sleep(20);
                    MefinoApp.SendAsyncProgress(Web.WebClientManager.LastDownloadProgress);
                }

                if (ZipHelper.ExtractZip(tempFile, dirPath))
                {
                    var manifestPath = $@"{dirPath}\manifest.json";

                    if (File.Exists(manifestPath))
                        File.Delete(manifestPath);

                    File.WriteAllText(manifestPath, manifest.ToJsonObject().ToString(true));

                    Console.WriteLine($"Installed package: {manifest.GUID} {manifest.Version}");

                    return true;
                }
                else
                    throw new Exception("Zip extraction failed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception isntalling package '" + manifest.GUID + "'");
                Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                //Mefino.SendAsyncCompletion(false);
                return false;
            }
        }

        // ======== Uninstall / Deletion ========

        public static void TryRemovePackage(string guid)
        {
            var package = TryGetInstalledPackage(guid);
            if (package != null)
                TryRemovePackage(package);
            else
                Console.WriteLine("Package '" + guid + "' does not seem to be installed.");
        }

        internal static void TryRemovePackage(PackageManifest package)
        {
            string dir;
            Dictionary<string, PackageManifest> dict;

            if (!package.m_isDisabled)
            {
                dir = Folders.OUTWARD_PLUGINS + $@"\{package.InstallFolder}";
                dict = s_enabledPackages;
            }
            else
            {
                dir = Folders.MEFINO_DISABLED_FOLDER + $@"\{package.InstallFolder}";
                dict = s_disabledPackages;
            }

            if (CheckDependencyBeforeRemoval(package, false))
            {
                if (TryDeleteDirectory(dir))
                    dict.Remove(package.GUID);
                else
                    Console.WriteLine("Unable to remove package '" + package.GUID + "' at path '" + dir + "'!");
            }
        }

        internal static bool TryDeleteDirectory(string dir)
        {
            if (!Directory.Exists(dir))
                return true;

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
            if (s_enabledPackages.ContainsKey(guid))
            {
                Console.WriteLine("Package '" + guid + "' is already enabled!");
                return;
            }

            if (!s_disabledPackages.ContainsKey(guid))
            {
                Console.WriteLine("Cannot enable package '" + guid + "' as it is not disabled!");
                return;
            }

            var package = s_disabledPackages[guid];

            string toDir = Folders.OUTWARD_PLUGINS + $@"\{package.InstallFolder}";
            string fromDir = Folders.MEFINO_DISABLED_FOLDER + $@"\{package.InstallFolder}";

            if (TryMoveDirectory(guid, fromDir, toDir))
            {
                Console.WriteLine("Enabled package: " + guid);
                s_disabledPackages.Remove(guid);
                s_enabledPackages.Add(guid, package);
            }
        }

        public static void TryDisableAllPackages()
        {
            for (int i = s_enabledPackages.Count; i >= 0; i--)
            {
                var pkg = s_enabledPackages.ElementAt(i);
                TryDisablePackage(pkg.Key, true);
            }
        }

        public static void TryDisablePackage(string guid, bool skipDependencyWarning = false)
        {
            if (s_disabledPackages.ContainsKey(guid))
            {
                Console.WriteLine("Package '" + guid + "' is already disabled!");
                return;
            }

            if (!s_enabledPackages.ContainsKey(guid))
            {
                Console.WriteLine("Cannot disable package '" + guid + "' as it is not installed!");
                return;
            }

            var package = s_enabledPackages[guid];

            if (!skipDependencyWarning)
            {
                if (!CheckDependencyBeforeRemoval(package, true))
                    return;
            }

            string toDir = Folders.MEFINO_DISABLED_FOLDER + $@"\{package.InstallFolder}";
            string fromDir = Folders.OUTWARD_PLUGINS + $@"\{package.InstallFolder}";

            if (TryMoveDirectory(guid, fromDir, toDir))
            {
                Console.WriteLine("Disable package: " + guid);
                s_enabledPackages.Remove(guid);
                s_disabledPackages.Add(guid, package);
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

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

        // Events used by various parts of Mefino
        public static event Action<PackageManifest> OnPackageEnabled;
        public static event Action<PackageManifest> OnPackageDisabled;
        public static event Action<PackageManifest> OnPackageInstalled;
        public static event Action<PackageManifest> OnPackageUninstalled;

        /// <summary>
        /// Try to get an installed package from a given <see cref="PackageManifest.GUID"/>.
        /// </summary>
        /// <param name="guid">The GUID to check for</param>
        /// <returns>The PackageManifest if installed, otherwise null.</returns>
        public static PackageManifest TryGetInstalledPackage(string guid)
        {
            s_enabledPackages.TryGetValue(guid, out PackageManifest package);

            if (package == null)
                s_disabledPackages.TryGetValue(guid, out package);

            return package;
        }

        #region INTERNAL REFRESHING / CACHING

        /// <summary>
        /// Updates packages, and check that all enabled packages are OK.
        /// </summary>
        /// <returns>true if all enabled packages are up to date and have dependencies enabled, otherwise false.</returns>
        public static bool RefreshInstalledPackages(bool onlyRefreshState = false)
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("Cannot retrieve installed mods as Outward folder path is not set!");
                return false;
            }

            if (!onlyRefreshState)
            {
                s_enabledPackages.Clear();
                s_disabledPackages.Clear();

                // load enabled plugins
                var pluginsPath = Folders.OUTWARD_PLUGINS;
                if (Directory.Exists(pluginsPath))
                {
                    foreach (var dir in Directory.GetDirectories(pluginsPath))
                        TryReadInstalledManifest(dir, false);
                }

                // load disabled plugins
                if (Directory.Exists(Folders.MEFINO_DISABLED_FOLDER))
                {
                    foreach (var dir in Directory.GetDirectories(Folders.MEFINO_DISABLED_FOLDER))
                        TryReadInstalledManifest(dir, true);
                }
            }

            // update statuses now that all packages loaded
            bool enabledPacksAreOk = true;
            foreach (var package in s_enabledPackages.Values)
            {
                if (WebManifestManager.s_cachedWebManifests.ContainsKey(package.GUID))
                {
                    var web = WebManifestManager.s_cachedWebManifests[package.GUID];
                    if (web.IsGreaterVersionThan(package))
                    {
                        package.m_installState = InstallState.Outdated;
                        enabledPacksAreOk = false;
                    }
                }

                if (package.m_installState == InstallState.Outdated)
                    continue;

                if (package.HasAnyEnabledConflicts(out _))
                {
                    package.m_installState = InstallState.HasConflict;
                    enabledPacksAreOk = false;
                }
                else if (!package.AreAllDependenciesEnabled(out _))
                {
                    package.m_installState = InstallState.MissingDependency;
                    enabledPacksAreOk = false;
                }
            }

            return enabledPacksAreOk;
        }

        /// <summary>
        /// Attempts to read and process an installed PackageManifest from the provided directory.
        /// </summary>
        private static void TryReadInstalledManifest(string dir, bool inDisabledFolder)
        {
            var manifestPath = dir + $@"\{PKG_MANIFEST_FILENAME}";
            if (!File.Exists(manifestPath))
            {
                //Console.WriteLine($"Unable to find expected manifest.json at path '" + dir + "'");
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

                if (!inDisabledFolder) // package is in enabled folder
                {
                    if (s_enabledPackages.ContainsKey(manifest.GUID))
                    {
                        Console.WriteLine("Duplicate enabled manifests found! Skipping: " + manifest.GUID);
                        return;
                    }

                    s_enabledPackages.Add(manifest.GUID, manifest);
                }
                else // package is in disabled folder
                {
                    if (s_enabledPackages.ContainsKey(manifest.GUID))
                    {
                        Console.WriteLine("Duplicate package found in both installed and disabled folders! Removing disabled: " + manifest.GUID);
                        IOHelper.TryDeleteDirectory(dir);
                        return;
                    }

                    if (s_disabledPackages.ContainsKey(manifest.GUID))
                    {
                        Console.WriteLine("Duplicate disabled manifests found! Skipping: " + manifest.GUID);
                        return;
                    }

                    s_disabledPackages.Add(manifest.GUID, manifest);
                }

                manifest.m_installState = InstallState.Installed;

                //Console.WriteLine($"Found installed package: {manifest.GUID} (disabled: {isInDisabledFolder})");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception loading installed manifest json!");
                Console.WriteLine(ex);
            }
        }

        #endregion

        #region ENABLING AND INSTALLING

        /// <summary>
        /// Try to enable the package GUID. 
        /// If this GUID is not installed or this method fails it will return false,
        /// otherwise it will return true and enable/install all dependencies.
        /// </summary>
        /// <param name="guid">The guid to try to enable</param>
        /// <param name="tryEnableDependencies">If any dependencies are disabled, should we try to automatically install/enable them?</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool TryEnablePackage(string guid, bool tryEnableDependencies = true)
        {
            var package = TryGetInstalledPackage(guid);

            if (package == null)
                return false;

            if (!PreEnableConflictCheck(package))
            {
                //Console.WriteLine("PreEnableConflictCheck returned false");
                return false;
            }

            if (!package.AreAllDependenciesEnabled(out List<string> missing))
            {
                if (!tryEnableDependencies || !PreEnableDependencyCheck(package, missing))
                    return false;
            }

            if (s_enabledPackages.ContainsKey(guid))
            {
                OnPackageEnabled?.Invoke(package);
                return true;
            }

            string toDir = Folders.OUTWARD_PLUGINS + $@"\{package.InstallFolder}";
            string fromDir = Folders.MEFINO_DISABLED_FOLDER + $@"\{package.InstallFolder}";

            if (IOHelper.TryMoveDirectory(fromDir, toDir))
            {
                //s_disabledPackages.Remove(guid);
                //s_enabledPackages.Add(guid, package);

                RefreshInstalledPackages();

                OnPackageEnabled?.Invoke(package);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempt to update the provided package to a newer version, if any is available.
        /// </summary>
        /// <param name="guid">The guid to try to update.</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool TryUpdatePackage(string guid)
        {
            var installed = TryGetInstalledPackage(guid);
            if (installed == null)
                return false;

            if (!WebManifestManager.s_cachedWebManifests.TryGetValue(guid, out PackageManifest webManifest))
                return false;

            if (installed.IsGreaterVersionThan(webManifest))
                return true;

            string dir = installed.IsDisabled
                            ? Folders.MEFINO_DISABLED_FOLDER
                            : Folders.OUTWARD_PLUGINS;

            dir = Path.Combine(dir, installed.InstallFolder);

            if (!IOHelper.TryDeleteDirectory(dir))
                return false;

            if (s_enabledPackages.ContainsKey(guid))
                s_enabledPackages.Remove(guid);
            else
                s_disabledPackages.Remove(guid);

            return TryInstallWebPackage(webManifest.GUID, !installed.IsDisabled);
        }

        /// <summary>
        /// Attempt to install a GUID from a GitHub web package, if such a package can be found.
        /// </summary>
        /// <param name="guid">The guid to try to install.</param>
        /// <param name="andEnable">Should the package be enabled after installing? Otherwise it will be disabled.</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool TryInstallWebPackage(string guid, bool andEnable = true)
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("Outward folder is not set! Cannot install package.");
                return false;
            }

            WebManifestManager.s_cachedWebManifests.TryGetValue(guid, out PackageManifest webManifest);

            if (webManifest == null)
            {
                if (!string.IsNullOrEmpty(guid) && TryGetInstalledPackage(guid) != null)
                {
                    Console.WriteLine("Could not find online package by GUID '" + guid + ", but an installed package exists with that GUID, enabling.");
                    TryEnablePackage(guid);
                    return true;
                }

                Console.WriteLine("Could not find web package by GUID '" + guid + "'");
                return false;
            }

            var existing = TryGetInstalledPackage(guid);

            if (existing != null && existing.IsGreaterVersionThan(webManifest))
            {
                if (existing.IsDisabled)
                    TryEnablePackage(guid);

                //Console.WriteLine("Installed package is already greater or equal version, skipping install.");
                return true;
            }

            if (webManifest.dependencies != null && webManifest.dependencies.Length > 0)
            {
                int i = 1;
                int count = webManifest.dependencies.Length;
                foreach (var dependency in webManifest.dependencies)
                {
                    MefinoGUI.SetProgressMessage($"Installing dependency {i} of {count}: {dependency}");

                    if (!TryInstallWebPackage(dependency))
                    {
                        var msgResult = MessageBox.Show("Installing dependency '" + dependency + "' failed!\n\nContinue anyway?", "Dependency failed!", MessageBoxButtons.OKCancel);
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

            if (!DownloadAndInstallPackage(webManifest))
            {
                MessageBox.Show("Failed to download and install " + guid + "!");
                return false;
            }

            RefreshInstalledPackages();

            OnPackageInstalled?.Invoke(webManifest);

            if (andEnable)
                return TryEnablePackage(guid);
            else
                return true;
        }

        /// <summary>
        /// Actually download and install the provided PackageManifest instance, which would presumably be a web manifest.
        /// </summary>
        /// <param name="manifest">The PackageManifest to install.</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        internal static bool DownloadAndInstallPackage(PackageManifest manifest)
        {
            try
            {
                MefinoGUI.SetProgressBarActive(true);

                MefinoGUI.DisableSensitiveControls();

                var dirPath = $@"{Folders.MEFINO_DISABLED_FOLDER}\{manifest.InstallFolder}";

                if (Directory.Exists(dirPath))
                    Directory.Delete(dirPath, true);

                var tempFile = TemporaryFile.CreateFile();

                var zipUrl = $"{manifest.GithubURL}/releases/latest/download/{PKG_MEFINO_PACKAGE_NAME}";

                Web.WebClientManager.DownloadFileAsync(zipUrl, tempFile);

                while (Web.WebClientManager.IsBusy)
                {
                    Thread.Sleep(20);
                    MefinoApp.SendAsyncProgress(Web.WebClientManager.LastDownloadProgress);
                }

                MefinoGUI.SetProgressMessage($"Installing {manifest.GUID} version {manifest.version}");

                if (ZipHelper.ExtractZip(tempFile, dirPath))
                {
                    var manifestPath = $@"{dirPath}\manifest.json";

                    if (File.Exists(manifestPath))
                        File.Delete(manifestPath);

                    File.WriteAllText(manifestPath, manifest.ToJsonObject().ToString(true));

                    //Console.WriteLine($"Installed package: {manifest.GUID} {manifest.version}");

                    MefinoGUI.SetProgressBarActive(false);

                    MefinoGUI.ReEnableSensitiveControls();

                    return true;
                }
                else
                    throw new Exception("Zip extraction failed!");
            }
            catch (Exception ex)
            {
                MefinoGUI.SetProgressBarActive(false);
                MefinoGUI.ReEnableSensitiveControls();

                Console.WriteLine("Exception isntalling package '" + manifest.GUID + "'");
                Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                //Mefino.SendAsyncCompletion(false);
                return false;
            }
        }

        #endregion

        #region DEPENDENCY AND CONFLICT RESOLUTION

        /// <summary>
        /// Check a package for conflicts before enabling it.
        /// </summary>
        /// <returns><see langword="true"/> if successful or no conflicts, otherwise <see langword="false"/></returns>
        internal static bool PreEnableConflictCheck(PackageManifest package)
        {
            // Check for packages which declare a conflict with this package.

            HashSet<string> conflicts = new HashSet<string>();

            if (package.HasAnyEnabledConflicts(out List<string> normConflicts))
            {
                foreach (var c in normConflicts)
                    if (!conflicts.Contains(c))
                        conflicts.Add(c);
            }

            var altConflicts = package.GetEnabledConflictsAlternate();
            if (altConflicts.Any())
            {
                foreach (var c in altConflicts)
                    if (!conflicts.Contains(c))
                        conflicts.Add(c);
            }

            if (!conflicts.Any())
                return true;

            var conflictTxt = "";
            foreach (var conflict in conflicts)
                conflictTxt += $"\n{conflict}";

            var msgBox = MessageBox.Show($"The following packages conflict with {package.name} and need to be disabled:\n" +
                $"{conflictTxt}\n\n" +
                $"Disable them and continue?",
                "Conflicts detected",
                MessageBoxButtons.OKCancel);

            if (msgBox == DialogResult.OK)
            {
                foreach (var pkg in conflicts)
                {
                    if (!TryDisablePackage(pkg, true))
                        return false;
                }

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Check a package for enabled dependant packages before removing/disabling it.
        /// </summary>
        /// <param name="package"></param>
        /// <param name="disabling">Is the package just being disabled? (Just used for the dialog prompt)</param>
        /// <returns><see langword="true"/> if successful or no dependants, otherwise <see langword="false"/></returns>
        internal static bool PreRemovalDependencyCheck(PackageManifest package, bool disabling)
        {
            var dependencies = package.GetCurrentlyEnabledDependantPackages();

            bool ret = true;

            if (dependencies.Any())
            {
                string msg = "";
                foreach (var dep in dependencies)
                {
                    msg += $"\n{dep}";
                }

                string action = disabling ? "disable" : "remove";

                var msgResult = MessageBox.Show($"The following packages depend on {package.name}:" +
                    $"\n{msg}" +
                    $"\n\nThese will be disabled. Really {action} it?",
                    "Dependency conflict!",
                    MessageBoxButtons.OKCancel);

                if (msgResult != DialogResult.OK)
                    ret = false;
                else
                {
                    foreach (var guid in dependencies)
                    {
                        TryDisablePackage(guid, true);
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Check a package for its dependencies before enabling it.
        /// </summary>
        /// <param name="missing">If there are dependencies which are not installed at all, they will be added to this list.</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        internal static bool PreEnableDependencyCheck(PackageManifest package, List<string> missing)
        {
            if (!package.TryEnableAllDependencies(false))
            {
                missing.RemoveAll(it => s_enabledPackages.ContainsKey(it));

                if (missing.Any())
                {
                    string miss = "";
                    foreach (var entry in missing)
                        miss += $"\n{entry}";

                    var result = MessageBox.Show($"To enable '{package.name}', the following dependencies need to be installed:" +
                        $"\n" +
                        $"{miss}" +
                        $"\n\n" +
                        $"Do you want to install them?",
                        "Missing dependencies",
                        MessageBoxButtons.OKCancel);

                    if (result == DialogResult.Cancel)
                        return false;
                    else
                    {
                        var check = package.TryEnableAllDependencies(true);

                        if (check == false)
                        {
                            Console.WriteLine("Check returned false!");
                            MessageBox.Show("Unable to install all dependencies!");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        #endregion

        #region PACKAGE DISABLING AND UNINSTALLATION

        /// <summary>
        /// Disable ALL packages.
        /// </summary>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool DisableAllPackages()
        {
            bool ret = true;
            for (int i = s_enabledPackages.Count - 1; i >= 0; i--)
            {
                var pkg = s_enabledPackages.ElementAt(i);
                ret &= TryDisablePackage(pkg.Key, true);
            }
            return ret;
        }

        /// <summary>
        /// Try to disable the package. This will disable dependant packages as well.
        /// </summary>
        /// <param name="skipDependencyWarning">Skip the user prompt and just automatically disable all dependants?</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool TryDisablePackage(string guid, bool skipDependencyWarning = false)
        {
            var package = TryGetInstalledPackage(guid);

            if (package == null)
                return true;

            if (s_disabledPackages.ContainsKey(guid))
            {
                OnPackageDisabled.Invoke(package);
                //Console.WriteLine("Package '" + guid + "' is already disabled!");
                return true;
            }

            if (!skipDependencyWarning)
            {
                if (!PreRemovalDependencyCheck(package, true))
                    return false;
            }
            else
                package.TryDisableAllDependencies();

            string toDir = Folders.MEFINO_DISABLED_FOLDER + $@"\{package.InstallFolder}";
            string fromDir = Folders.OUTWARD_PLUGINS + $@"\{package.InstallFolder}";

            if (IOHelper.TryMoveDirectory(fromDir, toDir))
            {
                ////Console.WriteLine("Disable package: " + guid);
                //s_enabledPackages.Remove(guid);
                //s_disabledPackages.Add(guid, package);

                RefreshInstalledPackages();

                OnPackageDisabled.Invoke(package);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to uninstall the package.
        /// </summary>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool TryUninstallPackage(string guid)
        {
            var package = TryGetInstalledPackage(guid);
            if (package != null)
                return TryUninstallPackage(package);
            else
                //Console.WriteLine("Package '" + guid + "' does not seem to be installed.");
                return false;
        }

        /// <summary>
        /// Try to uninstall the package.
        /// </summary>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        internal static bool TryUninstallPackage(PackageManifest package)
        {
            string dir;
            Dictionary<string, PackageManifest> dict;

            if (!package.IsDisabled)
            {
                dir = Folders.OUTWARD_PLUGINS + $@"\{package.InstallFolder}";
                dict = s_enabledPackages;
            }
            else
            {
                dir = Folders.MEFINO_DISABLED_FOLDER + $@"\{package.InstallFolder}";
                dict = s_disabledPackages;
            }

            if (PreRemovalDependencyCheck(package, false))
            {
                if (IOHelper.TryDeleteDirectory(dir))
                {
                    dict.Remove(package.GUID);

                    OnPackageUninstalled?.Invoke(package);
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}

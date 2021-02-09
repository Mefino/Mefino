using Mefino.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.Core
{
    public static class OutwardHelper
    {
        /// <summary>
        /// Verify packages and try to launch Outward.
        /// </summary>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool TryLaunchOutward()
        {
            if (!Folders.IsCurrentOutwardPathValid())
                return false;

            if (!LocalPackageManager.RefreshInstalledPackages())
            {
                var result = MessageBox.Show($"Some enabled packages are not Ready. Fix them and launch?", $"Warning", MessageBoxButtons.OKCancel);

                if (result == DialogResult.Cancel)
                    return false;

                for (int i = LocalPackageManager.s_enabledPackages.Count - 1; i >= 0; i--)
                {
                    var entry = LocalPackageManager.s_enabledPackages.ElementAt(i);
                    if (entry.Value.m_installState != InstallState.Outdated)
                        continue;

                    if (!LocalPackageManager.TryUpdatePackage(entry.Key))
                        return false;
                }

                // update package states
                LocalPackageManager.RefreshInstalledPackages(true);

                // Pass 1: enable missing dependencies.
                for (int i = LocalPackageManager.s_enabledPackages.Count - 1; i >= 0; i--)
                {
                    var entry = LocalPackageManager.s_enabledPackages.ElementAt(i);
                    if (entry.Value.m_installState != InstallState.MissingDependency)
                        continue;

                    if (!entry.Value.TryEnableAllDependencies(true))
                        return false;
                }

                // update package states
                LocalPackageManager.RefreshInstalledPackages(true);

                // Pass 2: disable packages WITH conflicts
                for (int i = LocalPackageManager.s_enabledPackages.Count - 1; i >= 0; i--)
                {
                    var entry = LocalPackageManager.s_enabledPackages.ElementAt(i);
                    if (entry.Value.m_installState != InstallState.HasConflict)
                        continue;

                    if (!LocalPackageManager.TryDisablePackage(entry.Key, true))
                        return false;
                }
            }

            try
            {
                if (Folders.OUTWARD_FOLDER.Contains(Path.Combine("Steam", "steamapps", "common", "Outward")))
                {
                    Process.Start($"steam://rungameid/794260");
                }
                else
                {
                    Process.Start(Path.Combine(Folders.OUTWARD_FOLDER, "Outward.exe"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error launching Outward:\n\n{ex}", "Error!", MessageBoxButtons.OK);
            }

            return false;
        }

        /// <summary>
        /// Attempt to completely uninstall BepInEx and all packages from the Outward folder.
        /// </summary>
        /// <param name="warningMessage">Display a warning message?</param>
        /// <returns><see cref="DialogResult.OK"/> if successful, otherwise <see cref="DialogResult.Cancel"/>.</returns>
        public static DialogResult UninstallFromOutward(bool warningMessage = true)
        {
            if (warningMessage)
            {
                var result = MessageBox.Show(
                    $"Really uninstall everything?" +
                    $"\n\nThis will delete BepInEx and all Mefino packages from the Outward folder." +
                    $"\n\nThis will not delete your Mefino profiles.",
                    $"Are you sure?",
                    MessageBoxButtons.OKCancel);

                if (result != DialogResult.OK)
                    return result;
            }

            try
            {
                IOHelper.SetDoingIO = true;

                Directory.Delete(Folders.MEFINO_OTWFOLDER_PATH, true);
                Directory.Delete(Path.Combine(Folders.OUTWARD_FOLDER, "BepInEx"), true);
                File.Delete(Path.Combine(Folders.OUTWARD_FOLDER, "winhttp.dll"));
                File.Delete(Path.Combine(Folders.OUTWARD_FOLDER, "doorstop_config.ini"));
                File.Delete(Path.Combine(Folders.OUTWARD_FOLDER, "changelog.txt"));

                IOHelper.SetDoingIO = false;

                LocalPackageManager.s_enabledPackages.Clear();
                LocalPackageManager.s_disabledPackages.Clear();

                return DialogResult.OK;
            }
            catch (Exception ex)
            {
                IOHelper.SetDoingIO = false;

                MessageBox.Show($"Failed uninstalling Mefino!\n\n{ex}");
                return DialogResult.Cancel;
            }
        }
    }
}

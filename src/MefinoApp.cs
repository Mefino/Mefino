using Mefino.LightJson;
using Mefino.Core;
using Mefino.IO;
using Mefino.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mefino.GUI;
using Mefino.CLI;
using Mefino.Core.Profiles;

namespace Mefino
{
    public class MefinoApp
    {
        public const string VERSION = "0.2.0.0";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            try
            {
                CoreInit();

                if (args.Any())
                {
                    CLIHandler.Execute(args);
                }
                else
                {
#if RELEASE
                    CLIHandler.HideConsole();
#endif

                    if (CheckUpdatedWanted())
                        return;

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new GUI.MefinoGUI());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fatal unhandled exception in Mefino.exe:" +
                    $"\n\n" +
                    $"{ex}",
                    "Error!",
                    MessageBoxButtons.OK);
            }
        }

        internal static void CoreInit()
        {
            AppDataManager.LoadConfig();

            WebClientManager.Initialize();

            RefreshAllPackages();

            ProfileManager.LoadProfileOrSetDefault();
        }

        public static void RefreshAllPackages(bool refreshOnline = false)
        {
            if (refreshOnline)
                WebManifestManager.UpdateWebManifests();
            else
                WebManifestManager.LoadWebManifestCache();

            // refresh installed packages
            LocalPackageManager.RefreshInstalledPackages();
        }

        internal static void SendAsyncProgress(int progress)
        {
            MefinoGUI.SetProgressPercent(progress);
            // let UI actually refresh for the progress.
            Application.DoEvents();
        }

        // ========= self update ===========

        // Github URLs
        private const string MEFINO_RELEASE_API_QUERY = @"https://api.github.com/repos/Mefino/Mefino/releases/latest";
        private const string MEFINO_RELEASE_URL = @"https://github.com/Mefino/Mefino/releases/latest";

        internal static bool CheckUpdatedWanted()
        {
            var fetchedVersion = GithubHelper.GetLatestReleaseVersion(MEFINO_RELEASE_API_QUERY);

            if (fetchedVersion == null)
                return false;

            if (new Version(fetchedVersion) > new Version(VERSION))
            {
                var result = MessageBox.Show(
                    $"A new version of Mefino is available: {fetchedVersion}.\n\nDo you want to open the release page?", 
                    "Update Available", 
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    Process.Start(MEFINO_RELEASE_URL);
                    return true;
                }
            }

            return false;
        }

        // ========== complete uninstall ==========

        public static DialogResult CompleteUninstall(bool warningMessage = true)
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
                Directory.Delete(Folders.MEFINO_FOLDER_PATH, true);
                Directory.Delete(Path.Combine(Folders.OUTWARD_FOLDER, "BepInEx"), true);
                File.Delete(Path.Combine(Folders.OUTWARD_FOLDER, "winhttp.dll"));
                File.Delete(Path.Combine(Folders.OUTWARD_FOLDER, "doorstop_config.ini"));
                File.Delete(Path.Combine(Folders.OUTWARD_FOLDER, "changelog.txt"));

                return DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed uninstalling Mefino!\n\n{ex}");
                return DialogResult.Cancel;
            }
        }

        // ========= launch ========

        public static bool TryLaunchOutward()
        {
            if (!Folders.IsCurrentOutwardPathValid())
                return false;

            if (!LocalPackageManager.RefreshInstalledPackages())
            {
                var result = MessageBox.Show($"Some enabled packages are outdated or missing dependencies. Launch anyway?", $"Warning", MessageBoxButtons.OKCancel);

                if (result == DialogResult.Cancel)
                    return false;
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
    }
}

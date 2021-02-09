using Mefino.LightJson;
using Mefino.LightJson.Serialization;
using Mefino.Core;
using Mefino.GUI;
using Mefino.IO;
using Mefino.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mefino.Web
{
    /// <summary>
    /// Handles interaction with BepInEx releases.
    /// </summary>
    public static class BepInExHandler
    {
        public static string InstalledBepInExVersion;

        internal static string s_latestBepInExVersion;
        internal static InstallState s_lastInstallStateResult;

        public static string BepInExFilePath => Path.Combine(Folders.OUTWARD_FOLDER, "BepInEx", "core", "BepInEx.dll");

        internal const string BEPINEX_RELEASE_API_QUERY = @"https://api.github.com/repos/BepInEx/BepInEx/releases/latest";

        /// <summary>
        /// Is BepInEx installed at all?
        /// </summary>
        public static bool IsBepInExInstalled()
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                //Console.WriteLine("Current Outward install path not set or invalid!");
                s_lastInstallStateResult = InstallState.NotInstalled;
                return false;
            }

            return File.Exists(BepInExFilePath)
                && File.Exists(Path.Combine(Folders.OUTWARD_FOLDER, "winhttp.dll"));
        }

        /// <summary>
        /// Check if BepInEx is oudated, if so update it.
        /// </summary>
        public static void UpdateBepInExIfNeeded()
        {
            if (!IsBepInExUpdated())
            {
                if (!string.IsNullOrEmpty(s_latestBepInExVersion))
                    UpdateBepInEx();
            }
        }

        /// <summary>
        /// Is BepInEx up to date?
        /// </summary>
        public static bool IsBepInExUpdated()
        {
            // Only query the version once per launch.
            // This is to limit GitHub API queries, since we are limited to 60 per hour.
            if (string.IsNullOrEmpty(s_latestBepInExVersion))
            {
                s_latestBepInExVersion = GithubHelper.GetLatestReleaseVersion(BEPINEX_RELEASE_API_QUERY);
            }
            
            if (string.IsNullOrEmpty(s_latestBepInExVersion))
            {
                Console.WriteLine("BepInEx GitHub release query returned null! Are you offline?");

                if (File.Exists(BepInExFilePath))
                {
                    s_lastInstallStateResult = InstallState.Installed;
                    return true;
                }

                s_lastInstallStateResult = InstallState.NotInstalled;
                return false;
            }

            if (!File.Exists(BepInExFilePath))
            {
                //Console.WriteLine("BepInEx not installed at '" + existingFilePath + "'");
                s_lastInstallStateResult = InstallState.NotInstalled;
                return false;
            }

            string file_version = FileVersionInfo.GetVersionInfo(BepInExFilePath).FileVersion;

            if (new Version(file_version) >= new Version(s_latestBepInExVersion))
            {
                // Console.WriteLine($"BepInEx {latestVersion} is up to date!");
                s_lastInstallStateResult = InstallState.Installed;
                return true;
            }
            else
            {
                Console.WriteLine($"Your current BepInEx version {file_version} is older than latest version: {s_latestBepInExVersion}");
                s_lastInstallStateResult = InstallState.Outdated;
                return false;
            }
        }

        /// <summary>
        /// Attempt to update BepInEx to the latest version.
        /// </summary>
        public static void UpdateBepInEx()
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("Current Outward folder path not set or invalid! Cannot update BepInEx.");
                return;
            }

            // If an update check hasn't been done this launch, do one now.
            if (IsBepInExUpdated())
                return;

            // If the check we just did failed (no query result), we need to abort.
            if (string.IsNullOrEmpty(s_latestBepInExVersion))
                return;

            try
            {
                string releaseURL = $@"https://github.com/BepInEx/BepInEx/releases/latest/download/BepInEx_x64_{s_latestBepInExVersion}.zip";

                var tempFile = TemporaryFile.CreateFile();

                MefinoGUI.SetProgressMessage($"Downloading BepInEx {s_latestBepInExVersion}");

                WebClientManager.DownloadFileAsync(releaseURL, tempFile);

                while (WebClientManager.IsBusy)
                {
                    MefinoApp.SendAsyncProgress(WebClientManager.LastDownloadProgress);
                }

                MefinoGUI.SetProgressMessage($"Extracting BepInEx {s_latestBepInExVersion}");

                ZipHelper.ExtractZip(tempFile, Folders.OUTWARD_FOLDER);

                Console.WriteLine("Updated BepInEx to version '" + s_latestBepInExVersion + "'");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception downloading and installing BepInEx!");
                Console.WriteLine(ex);
            }
        }
    }
}

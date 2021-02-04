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
    public static class BepInExHandler
    {
        public static string InstalledBepInExVersion;

        internal static string s_latestBepInExVersion;
        internal static InstallState s_lastInstallStateResult;

        internal const string BEPINEX_RELEASE_API_QUERY = @"https://api.github.com/repos/BepInEx/BepInEx/releases/latest";

        public static void CheckAndUpdateBepInEx()
        {
            if (!IsBepInExUpdated())
            {
                if (!string.IsNullOrEmpty(s_latestBepInExVersion))
                    InstallLatestBepInEx();
            }
        }

        internal static bool IsBepInExUpdated()
        {
            if (!Mefino.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("Current Outward install path not set or invalid!");
                s_lastInstallStateResult = InstallState.NotInstalled;
                return false;
            }

            string existingFilePath = Path.Combine(Mefino.OUTWARD_FOLDER, "BepInEx", "core", "BepInEx.dll");

            var latestVersion = GithubHelper.GetLatestReleaseVersion(BEPINEX_RELEASE_API_QUERY);
            if (string.IsNullOrEmpty(latestVersion))
            {
                s_latestBepInExVersion = null;
                Console.WriteLine("BepInEx GitHub release query returned null! Are you offline?");

                if (File.Exists(existingFilePath))
                {
                    s_lastInstallStateResult = InstallState.Installed;
                    return true;
                }

                s_lastInstallStateResult = InstallState.NotInstalled;
                return false;
            }

            s_latestBepInExVersion = latestVersion;

            if (!File.Exists(existingFilePath))
            {
                //Console.WriteLine("BepInEx not installed at '" + existingFilePath + "'");
                s_lastInstallStateResult = InstallState.NotInstalled;
                return false;
            }

            string file_version = FileVersionInfo.GetVersionInfo(existingFilePath).FileVersion;

            if (new Version(file_version) >= new Version(latestVersion))
            {
                // Console.WriteLine($"BepInEx {latestVersion} is up to date!");
                s_lastInstallStateResult = InstallState.Installed;
                return true;
            }
            else
            {
                Console.WriteLine($"Your current BepInEx version {file_version} is older than latest version: {latestVersion}");
                s_lastInstallStateResult = InstallState.Outdated;
                return false;
            }
        }

        public static void InstallLatestBepInEx()
        {
            if (!Mefino.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("Current Outward folder path not set or invalid! Cannot update BepInEx.");
                return;
            }

            if (string.IsNullOrEmpty(s_latestBepInExVersion))
            {
                Console.WriteLine("Latest BepInEx version not fetched! Cannot update!");
                return;
            }

            try
            {
                string releaseURL = $@"https://github.com/BepInEx/BepInEx/releases/latest/download/BepInEx_x64_{s_latestBepInExVersion}.zip";

                var tempFile = TemporaryFile.CreateFile();

                WebClientManager.DownloadFileAsync(releaseURL, tempFile);

                while (!WebClientManager.WebClient.IsBusy)
                {
                    if (Bootloader.BepProgressBar != null)
                        Bootloader.BepProgressBar.Value = WebClientManager.s_lastDownloadProgressPercent;
                    Thread.Sleep(20);
                }

                while (WebClientManager.WebClient.IsBusy)
                {
                    if (Bootloader.BepProgressBar != null)
                        Bootloader.BepProgressBar.Value = WebClientManager.s_lastDownloadProgressPercent;
                    Thread.Sleep(20);
                }

                ZipHelper.ExtractZip(tempFile, Mefino.OUTWARD_FOLDER);

                Console.WriteLine("Updated BepInEx to version '" + s_latestBepInExVersion + "'");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception downloading and installing BepInEx!");
                Console.WriteLine(ex);
            }

            //ExtractZip(dirpath, tempfilepath);
        }
    }
}

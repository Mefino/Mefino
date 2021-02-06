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
            LoadConfig();

            WebClientManager.Initialize();

            RefreshLocalManifests();
        }

        public static void RefreshLocalManifests()
        {
            // load cached web manifests
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

        // ========== config ============

        public static bool LoadConfig()
        {
            if (!File.Exists(Folders.MEFINO_CONFIG_PATH))
                return false;

            var jsonObject = LightJson.Serialization.JsonReader.ParseFile(Folders.MEFINO_CONFIG_PATH);

            if (jsonObject == default)
                return false;

            if (jsonObject[nameof(Folders.OUTWARD_FOLDER)].AsString is string path)
            {
                if (!Folders.SetOutwardFolderPath(path))
                    Console.WriteLine("Saved Outward path '" + path + "' is invalid! Needs to be set again.");

                return true;
            }

            return false;
        }

        public static void SaveConfig()
        {
            if (File.Exists(Folders.MEFINO_CONFIG_PATH))
                File.Delete(Folders.MEFINO_CONFIG_PATH);

            var jsonObject = new JsonObject
            {
                { nameof(Folders.OUTWARD_FOLDER), Folders.OUTWARD_FOLDER }
            };

            File.WriteAllText(Folders.MEFINO_CONFIG_PATH, jsonObject.ToString(true));
        }
    }
}

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

namespace Mefino
{
    public static class Mefino
    {
        public const string VERSION = "0.1.0.0";

        // Path to chosen or saved Outward folder.
        public static string OUTWARD_FOLDER => m_outwardPath;
        private static string m_outwardPath = "";

        // Relative path to Outward\BepInEx\plugins
        public static string OUTWARD_PLUGINS => OUTWARD_FOLDER + @"\BepInEx\plugins";

        // Relative path to Mefino's Outward folder
        internal static string MEFINO_FOLDER_PATH => OUTWARD_FOLDER + @"\Mefino";
        internal static string MEFINO_DISABLED_FOLDER => MEFINO_FOLDER_PATH + @"\Disabled";

        // Mefino AppData config json path
        internal static string MEFINO_CONFIG_PATH 
            => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\mefino-config.json";

        // Github URLs
        internal const string MEFINO_RELEASE_API_QUERY = @"https://api.github.com/repos/Mefino/Mefino/releases/latest";
        internal const string MEFINO_RELEASE_URL = @"https://github.com/Mefino/Mefino/releases/latest";

        // ====== outward folder paths =======

        public static bool SetOutwardFolderPath(string path)
        {
            path = Path.GetFullPath(path);

            if (!IsValidOutwardMonoPath(path))
            {
                Console.WriteLine($"'{path}' is not a valid Outward Mono install path!");
                return false;
            }

            m_outwardPath = path;

            CheckOutwardMefinoInstall();

            SaveConfig(); 
            
            //Console.WriteLine($"Set Outward folder to '{OUTWARD_FOLDER}'");

            return true;
        }

        public static bool IsCurrentOutwardPathValid() => IsValidOutwardMonoPath(OUTWARD_FOLDER);

        public static bool IsValidOutwardMonoPath(string path)
        {
            var suf = @"\Outward.exe";
            if (path.EndsWith(suf))
                path = path.Substring(0, path.Length - suf.Length);
        
            return !File.Exists(path + @"\GameAssembly.dll")
                && File.Exists(path + @"\Outward_Data\Managed\Assembly-CSharp.dll")
                && Directory.Exists(path + @"\MonoBleedingEdge");
        }

        internal static void CheckOutwardMefinoInstall()
        {
            if (!IsCurrentOutwardPathValid())
                return;

            Directory.CreateDirectory(MEFINO_FOLDER_PATH);
            Directory.CreateDirectory(MEFINO_DISABLED_FOLDER);

            // todo install the mefino plugin(?)
        }

        // ========= self update ===========

        internal static bool CheckSelfUpdate()
        {
            var fetchedVersion = GithubHelper.GetLatestReleaseVersion(MEFINO_RELEASE_API_QUERY);

            if (fetchedVersion == null)
                return false;

            if (new Version(fetchedVersion) > new Version(VERSION))
            {
                var result = MessageBox.Show($"A new version of Mefino is available: {fetchedVersion}.\n\nDo you want to open the release page?", 
                    "Update Available", 
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    Process.Start(MEFINO_RELEASE_URL);
                }
            }

            return false;
        }

        // ========== config ============

        public static bool LoadConfig()
        {
            if (!File.Exists(MEFINO_CONFIG_PATH))
                return false;

            var jsonObject = LightJson.Serialization.JsonReader.ParseFile(MEFINO_CONFIG_PATH);

            if (jsonObject == default)
                return false;

            if (jsonObject[nameof(OUTWARD_FOLDER)].AsString is string path)
            {
                if (!SetOutwardFolderPath(path))
                    Console.WriteLine("Saved Outward path '" + path + "' is invalid! Needs to be set again.");

                return true;
            }

            return false;
        }

        public static void SaveConfig()
        {
            if (File.Exists(MEFINO_CONFIG_PATH))
                File.Delete(MEFINO_CONFIG_PATH);

            var jsonObject = new JsonObject
            {
                { nameof(OUTWARD_FOLDER), OUTWARD_FOLDER }
            };

            File.WriteAllText(MEFINO_CONFIG_PATH, jsonObject.ToString(true));
        }
    }
}

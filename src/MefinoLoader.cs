using Mefino.LightJson;
using Mefino.Loader.Core;
using Mefino.Loader.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Loader
{
    public static class MefinoLoader
    {
        public const string VERSION = "0.1.0.0";

        public static string OUTWARD_FOLDER => m_outwardPath;
        private static string m_outwardPath = "";

        public static string OUTWARD_PLUGINS => OUTWARD_FOLDER + @"\BepInEx\plugins";

        internal static string MEFINO_FOLDER_PATH => OUTWARD_FOLDER + @"\Mefino";
        internal static string MEFINO_DISABLED_FOLDER => MEFINO_FOLDER_PATH + @"\Disabled";

        internal static string MEFINO_CONFIG_PATH => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\mefino-config.json";

        public static bool SetOutwardFolderPath(string path)
        {
            path = Path.GetFullPath(path);

            if (!IsValidOutwardMonoPath(path))
            {
                Console.WriteLine($"'{path}' is not a valid Outward Mono install path!");
                return false;
            }

            m_outwardPath = path;

            CheckMefinoFolder();

            SaveConfig();

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

        internal static void CheckMefinoFolder()
        {
            if (!IsCurrentOutwardPathValid())
                return;

            Directory.CreateDirectory(MEFINO_FOLDER_PATH);
            Directory.CreateDirectory(MEFINO_DISABLED_FOLDER);
        }

        public static void LoadConfig()
        {
            if (!File.Exists(MEFINO_CONFIG_PATH))
                return;

            var jsonObject = LightJson.Serialization.JsonReader.ParseFile(MEFINO_CONFIG_PATH);

            if (jsonObject == default)
                return;

            if (jsonObject[nameof(OUTWARD_FOLDER)].AsString is string path)
            {
                if (!SetOutwardFolderPath(path))
                    Console.WriteLine("Saved Outward path '" + path + "' is invalid! Needs to be set again.");
            }
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

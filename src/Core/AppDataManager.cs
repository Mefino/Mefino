using Mefino.LightJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.Core
{
    public static class AppDataManager
    {
        // Mefino AppData config json path
        internal static string MEFINO_CONFIG_PATH => Path.Combine(Folders.MEFINO_APPDATA_FOLDER, "mefino-config.json");

        // ========== config ============

        public static bool LoadConfig()
        {
            if (!Directory.Exists(Folders.MEFINO_APPDATA_FOLDER))
            {
                Directory.CreateDirectory(Folders.MEFINO_APPDATA_FOLDER);
            }

            if (!File.Exists(MEFINO_CONFIG_PATH))
                return false;

            var jsonObject = LightJson.Serialization.JsonReader.ParseFile(MEFINO_CONFIG_PATH);

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
            if (File.Exists(MEFINO_CONFIG_PATH))
                File.Delete(MEFINO_CONFIG_PATH);

            var jsonObject = new JsonObject
            {
                { nameof(Folders.OUTWARD_FOLDER), Folders.OUTWARD_FOLDER }
            };

            File.WriteAllText(MEFINO_CONFIG_PATH, jsonObject.ToString(true));
        }
    }
}

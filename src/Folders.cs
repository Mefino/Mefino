using System;
using System.IO;

namespace Mefino
{
    public static class Folders
    {
        // Path to chosen or saved Outward folder.
        public static string OUTWARD_FOLDER => m_outwardPath;
        internal static string m_outwardPath = "";

        // Relative path to Outward\BepInEx\plugins
        public static string OUTWARD_PLUGINS => OUTWARD_FOLDER + @"\BepInEx\plugins";

        // Relative path to Mefino's Outward folder
        internal static string MEFINO_FOLDER_PATH => OUTWARD_FOLDER + @"\Mefino";
        internal static string MEFINO_DISABLED_FOLDER => MEFINO_FOLDER_PATH + @"\Disabled";

        // Mefino AppData config json path
        internal static string MEFINO_CONFIG_PATH
            => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\mefino-config.json";

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

            Mefino.SaveConfig();

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
        }
    }
}
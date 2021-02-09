using Mefino.Core;
using Mefino.Core.IO;
using System;
using System.IO;

namespace Mefino
{
    public static class Folders
    {
        /// <summary>
        /// Path to chosen or saved Outward folder.
        /// </summary>
        public static string OUTWARD_FOLDER => m_outwardPath;
        internal static string m_outwardPath = "";

        /// <summary>
        /// Relative path to <c>\BepInEx\plugins</c> from <see cref="OUTWARD_FOLDER"/>
        /// </summary>
        public static string OUTWARD_PLUGINS => Path.Combine(OUTWARD_FOLDER, "BepInEx", "plugins");

        /// <summary>
        /// Path to Mefino's folder in the actual Outward folder
        /// </summary>
        internal static string MEFINO_OTWFOLDER_PATH => Path.Combine(OUTWARD_FOLDER, "Mefino");

        /// <summary>
        /// Path to Mefino's <c>Disabled</c> folder, inside the <see cref="MEFINO_OTWFOLDER_PATH"/> directory.
        /// </summary>
        internal static string MEFINO_DISABLED_FOLDER => Path.Combine(MEFINO_OTWFOLDER_PATH, "Disabled");

        /// <summary>
        /// Mefino's folder in the AppData\Roaming\ folder.
        /// </summary>
        internal static string MEFINO_APPDATA_FOLDER =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mefino");

        /// <summary>
        /// Set the Outward folder path to the provided path.
        /// </summary>
        /// <returns><see langword="true"/> if successful and a valid Mono path, otherwise <see langword="false"/></returns>
        public static bool SetOutwardFolderPath(string path) => SetOutwardFolderPath(path, out _);

        /// <summary>
        /// Set the Outward folder path to the provided path.
        /// </summary>
        /// <returns><see langword="true"/> if successful and a valid Mono path, otherwise <see langword="false"/></returns>
        public static bool SetOutwardFolderPath(string path, out InstallState state)
        {
            path = Path.GetFullPath(path);

            if (!IsValidOutwardMonoPath(path, out state))
            {
                Console.WriteLine($"'{path}' is not a valid Outward Mono install path!");
                return false;
            }

            m_outwardPath = path;

            CheckOutwardMefinoInstall();

            //Console.WriteLine($"Set Outward folder to '{OUTWARD_FOLDER}'");

            return true;
        }

        /// <summary>
        /// Makes sure the Mefino folder exists in the Outward folder.
        /// </summary>
        internal static void CheckOutwardMefinoInstall()
        {
            if (!IsCurrentOutwardPathValid())
                return;

            IOHelper.CreateDirectory(MEFINO_OTWFOLDER_PATH);
            IOHelper.CreateDirectory(MEFINO_DISABLED_FOLDER);
        }

        /// <summary>
        /// Is the currently set Outward folder path a valid Mono install?
        /// </summary>
        public static bool IsCurrentOutwardPathValid() 
            => IsValidOutwardMonoPath(OUTWARD_FOLDER, out _);

        /// <summary>
        /// Is the currently set Outward folder path a valid Mono install?
        /// </summary>
        public static bool IsCurrentOutwardPathValid(out InstallState state) 
            => IsValidOutwardMonoPath(OUTWARD_FOLDER, out state);

        /// <summary>
        /// Is the provided path a valid Mono install?
        /// </summary>
        public static bool IsValidOutwardMonoPath(string path) 
            => IsValidOutwardMonoPath(path, out _);

        /// <summary>
        /// Is the provided path a valid Mono install?
        /// </summary>
        public static bool IsValidOutwardMonoPath(string path, out InstallState state)
        {
            var suf = $@"{Path.DirectorySeparatorChar}Outward.exe";
            if (path.EndsWith(suf))
                path = path.Substring(0, path.Length - suf.Length);

            if (File.Exists(Path.Combine(path, "GameAssembly.dll")))
            {
                // il2cpp install. using "Outdated" for this result.
                state = InstallState.Outdated;
                return false;
            }

            if (File.Exists(Path.Combine(path, "Outward_Data", "Managed", "Assembly-CSharp.dll"))
                && Directory.Exists(Path.Combine(path, "MonoBleedingEdge")))
            {
                state = InstallState.Installed;
                return true;
            }

            state = InstallState.NotInstalled;
            return false;
        }
    }
}
using Mefino.Core.Profiles;
using Mefino.Core.IO;
using Mefino.LightJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mefino.GUI.Models;

namespace Mefino.Core
{
    public static class AppDataManager
    {
        /// <summary>
        /// The path to Mefino's JSON config file in the AppData folder.
        /// </summary>
        internal static string MEFINO_CONFIG_PATH => Path.Combine(Folders.MEFINO_APPDATA_FOLDER, "mefino-config.json");

        /// <summary>
        /// Try to load Mefino's config from the AppData folder.
        /// </summary>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool LoadConfig()
        {
            if (!Directory.Exists(Folders.MEFINO_APPDATA_FOLDER))
                IOHelper.CreateDirectory(Folders.MEFINO_APPDATA_FOLDER);

            if (!File.Exists(MEFINO_CONFIG_PATH))
                return false;

            var jsonObject = LightJson.Serialization.JsonReader.ParseFile(MEFINO_CONFIG_PATH);

            if (jsonObject == default)
                return false;

            if (jsonObject[nameof(Folders.OUTWARD_FOLDER)].AsString is string path)
                Folders.SetOutwardFolderPath(path);

            if (jsonObject[nameof(ProfileManager.ActiveProfile)].AsString is string activeProfile)
                ProfileManager.s_activeProfile = activeProfile;

            if (jsonObject[nameof(BrowseModsPage.ShowInstalledPackages)].AsBoolean is bool showInstalled)
                BrowseModsPage.ShowInstalledPackages = showInstalled;

            if (jsonObject[nameof(BrowseModsPage.OnlyShowTrusted)].AsBoolean is bool onlyTrusted)
                BrowseModsPage.OnlyShowTrusted = onlyTrusted;

            if (jsonObject[nameof(BrowseModsPage.ShowLibraries)].AsBoolean is bool showLibrary)
                BrowseModsPage.ShowLibraries = showLibrary;

            return true;
        }

        /// <summary>
        /// Try to save Mefino's config to the AppData folder.
        /// </summary>
        public static void SaveConfig()
        {
            if (!Directory.Exists(Folders.MEFINO_APPDATA_FOLDER))
                IOHelper.CreateDirectory(Folders.MEFINO_APPDATA_FOLDER);

            if (File.Exists(MEFINO_CONFIG_PATH))
                File.Delete(MEFINO_CONFIG_PATH);

            var jsonObject = new JsonObject
            {
                { nameof(Folders.OUTWARD_FOLDER), Folders.OUTWARD_FOLDER },
                { nameof(ProfileManager.ActiveProfile), ProfileManager.s_activeProfile },
                { nameof(BrowseModsPage.ShowInstalledPackages), BrowseModsPage.ShowInstalledPackages },
                { nameof(BrowseModsPage.OnlyShowTrusted), BrowseModsPage.OnlyShowTrusted },
                { nameof(BrowseModsPage.ShowLibraries), BrowseModsPage.ShowLibraries },
            };

            File.WriteAllText(MEFINO_CONFIG_PATH, jsonObject.ToString(true));
        }
    }
}

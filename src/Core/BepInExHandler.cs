using Mefino.LightJson;
using Mefino.LightJson.Serialization;
using Mefino.Loader.IO;
using Mefino.Loader.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Loader.Core
{
    public static class BepInExHandler
    {
        public static string InstalledBepInExVersion;

        internal static string s_latestBepInExVersion;

        internal const string BEPINEX_API_QUERY = @"https://api.github.com/repos/BepInEx/BepInEx/releases/latest";

        public static void CheckAndUpdateBepInEx()
        {
            if (!CheckBepInEx())
            {
                if (!string.IsNullOrEmpty(s_latestBepInExVersion))
                    InstallLatestBepInEx();
            }
        }

        internal static bool CheckBepInEx()
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("Current Outward install path not set or invalid!");
                return false;
            }

            string existingFilePath = Path.Combine(MefinoLoader.OUTWARD_FOLDER, "BepInEx", "core", "BepInEx.dll");

            var githubQuery = FetchBepInExReleases();
            if (githubQuery == null)
            {
                s_latestBepInExVersion = null;
                Console.WriteLine("BepInEx GitHub release query returned null! Are you offline?");

                return File.Exists(existingFilePath);
            }

            var latest = ((JsonValue)githubQuery).AsJsonObject;

            if (latest != default)
            {
                var tagname = latest["tag_name"].AsString;
                if (tagname.StartsWith("v"))
                    tagname = tagname.Substring(1, tagname.Length - 1);

                if (tagname.Split('.').Length != 4)
                    tagname += ".0";

                s_latestBepInExVersion = tagname;

                if (!File.Exists(existingFilePath))
                {
                    Console.WriteLine("BepInEx not installed at '" + existingFilePath + "'");
                    return false;
                }

                string file_version = FileVersionInfo.GetVersionInfo(existingFilePath).FileVersion;

                if (new Version(file_version) >= new Version(tagname))
                {
                    Console.WriteLine($"BepInEx {tagname} is up to date!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Your current BepInEx version {file_version} is older than latest version: {tagname}");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Unable to parse BepInEx release query from github json!");
                return File.Exists(existingFilePath);
            }
        }

        public static JsonValue? FetchBepInExReleases()
        {
            string query;
            try
            {
                WebClientManager.Reset();
                query = WebClientManager.WebClient.DownloadString(BEPINEX_API_QUERY);
            }
            catch
            {
                return null;
            }

            if (!string.IsNullOrEmpty(query))
            {
                return JsonReader.Parse(query);
            }

            return null;
        }

        public static void InstallLatestBepInEx()
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
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

                ZipHelper.DownloadAndExtractZip(releaseURL, MefinoLoader.OUTWARD_FOLDER);

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

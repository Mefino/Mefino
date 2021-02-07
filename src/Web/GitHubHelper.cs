using Mefino.LightJson;
using Mefino.LightJson.Serialization;
using Mefino.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Web
{
    public static class GithubHelper
    {
        // ======= API Query ======== //

        public static JsonValue? FetchJsonApiQuery(string apiQuery)
        {
            string query = WebClientManager.DownloadString(apiQuery);

            if (!string.IsNullOrEmpty(query))
                return JsonReader.Parse(query);

            return null;
        }

        public static string GetLatestReleaseVersion(string githubQueryUrl)
        {
            var queryResult = FetchJsonApiQuery(githubQueryUrl);

            if (queryResult == null)
                return null;

            var latest = ((JsonValue)queryResult).AsJsonObject;

            if (latest == null)
                return null;

            var version = latest["tag_name"].AsString;
            if (version.StartsWith("v"))
                version = version.Substring(1, version.Length - 1);

            while (version.Split('.').Length != 4)
                version += ".0";

            return version;
        }

        // used by WebManifestManager
        public static JsonValue? QueryForMefinoPackages()
        {
            try
            {
                string query = WebClientManager.DownloadString(WebManifestManager.GITHUB_PACKAGE_QUERY_URL);

                if (!string.IsNullOrEmpty(query))
                    return JsonReader.Parse(query);

                return null;
            }
            catch
            {
                Console.WriteLine("Exception getting Mefino packages from github!");
                return null;
            }
        }
    }
}

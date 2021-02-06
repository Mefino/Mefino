using Mefino.LightJson;
using Mefino.LightJson.Serialization;
using Mefino.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Core
{
    public class PackageManifest
    {
        /// <summary>
        /// Unique ID for this package, which is generated from: 'Author.PackageName'
        /// </summary>
        public string GUID => $"{Author} {PackageName}";

        public string InstallFolder => Author + " " + (string.IsNullOrEmpty(OverrideFolderName)
                                                           ? PackageName
                                                           : OverrideFolderName);

        /// <summary>Your GitHub username where the package repository is hosted.</summary>
        public string Author;
        /// <summary>Name of the package, and must also be the name of the repository.</summary>
        public string PackageName;

        /// <summary>
        /// [OPTIONAL] You can set this to the name of the folder in BepInEx\plugins\ that your package uses.<br />
        /// You only need to use this if your BepInEx\plugins folder is NOT the same as the name of your repository (PackageName).
        /// </summary>
        public string OverrideFolderName;

        /// <summary>Version of the package</summary>
        public string Version;
        /// <summary>Short description of the package</summary>
        public string Description;
        /// <summary>List of dependency GUIDs for this package</summary>
        public string[] Dependencies = new string[0];
        /// <summary>True if this package should be installed by all players online, false if it doesn't matter.</summary>
        public bool RequiresOnlineSync;

        public string m_manifestCachedTime;

        public string GithubURL => $"https://github.com/{Author}/{PackageName}";

        internal InstallState m_installState;
        internal bool m_isDisabled;

        public bool IsGreaterVersionThan(PackageManifest other, bool greaterOrEqual = false)
        {
            Version otherVersion;
            try
            {
                otherVersion = new Version(other.Version);
            }
            catch { return true; }

            Version thisVersion;
            try
            {
                thisVersion = new Version(this.Version);
            }
            catch { return false; }

            return greaterOrEqual
                    ? thisVersion >= otherVersion
                    : thisVersion > otherVersion;
        }

        internal bool IsManifestCachedSince(string utcTimeString)
        {
            DateTime timeToCheck;
            try
            {
                timeToCheck = DateTime.Parse(utcTimeString);
            }
            catch
            {
                Console.WriteLine("Exception parsing utc time string " + utcTimeString);
                // default to true if unable to parse the string to check against
                return true;
            }

            DateTime thisTime;
            try
            {
                thisTime = DateTime.Parse(this.m_manifestCachedTime);
            }
            catch
            {
                Console.WriteLine("Exception parsing manifest cache time: " + m_manifestCachedTime);
                // default to false if unable to parse existing manifest time
                return false;
            }

            return timeToCheck <= thisTime;
        }

        public bool IsDependantUpon(string otherGuid)
        {
            if (this.Dependencies == null || !this.Dependencies.Any())
                return false;

            return this.Dependencies.Contains(otherGuid);
        }

        public List<string> GetDependantEnabledPackagesOfThis()
        {
            var ret = new List<string>();

            foreach (var package in LocalPackageManager.s_enabledPackages)
            {
                if (package.Value.IsDependantUpon(this.GUID))
                    ret.Add(package.Key);
            }

            return ret;
        }

        public bool AreAllDependenciesEnabled(out List<string> missing)
        {
            missing = new List<string>();

            if (this.Dependencies == null || !this.Dependencies.Any())
                return true;

            bool ret = true;
            foreach (var dep in this.Dependencies)
            {
                if (!LocalPackageManager.s_enabledPackages.ContainsKey(dep))
                {
                    missing.Add(dep);
                    ret = false;
                }
            }

            return ret;
        }

        public JsonObject ToJsonObject()
        {
            return new JsonObject
            {
                { nameof(GUID),         this.GUID },
                { nameof(Author),       this.Author },
                { nameof(PackageName),  this.PackageName },
                { nameof(Version),      this.Version },
                { nameof(Description),  this.Description },
                {
                    nameof(Dependencies),
                    new JsonArray(this.Dependencies.Select(it => new JsonValue(it))
                                                   .ToArray())
                },
                { nameof(OverrideFolderName), this.OverrideFolderName },
                { nameof(RequiresOnlineSync), this.RequiresOnlineSync },
                { nameof(m_manifestCachedTime), this.m_manifestCachedTime },
            };
        }

        internal static PackageManifest FromManifestJson(string jsonString)
        {
            try
            {
                var json = JsonReader.Parse(jsonString);

                var ret = new PackageManifest
                {
                    Author = json[nameof(Author)].AsString,
                    PackageName = json[nameof(PackageName)].AsString,
                    Version = json[nameof(Version)].AsString
                };

                if (json[nameof(Description)].AsString is string desc)
                    ret.Description = desc;

                if (json[nameof(Dependencies)].AsJsonArray is JsonArray deps)
                    ret.Dependencies = deps.Select(it => it.AsString)?.ToArray();

                if (json[nameof(OverrideFolderName)].AsString is string folder)
                    ret.OverrideFolderName = folder;

                if (json[nameof(RequiresOnlineSync)].AsBoolean is bool requiresSync)
                    ret.RequiresOnlineSync = requiresSync;

                if (json[nameof(m_manifestCachedTime)].AsString is string cachetime)
                    ret.m_manifestCachedTime = cachetime;

                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception parsing PackageManifest from Json!");
                Console.WriteLine(ex);
                Console.WriteLine("Json string: " + jsonString);
                return default;
            }
        }
    }
}

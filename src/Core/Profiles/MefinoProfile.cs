using Mefino.LightJson;
using Mefino.LightJson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Core.Profiles
{
    public class MefinoProfile
    {
        /// <summary>
        /// Name of this Profile.
        /// </summary>
        public string name;

        /// <summary>
        /// The list of enabled Package GUIDs.
        /// </summary>
        public readonly List<string> packages = new List<string>();

        /// <summary>
        /// Enable all packages in this profile, and disable all other packages.
        /// </summary>
        public void EnableProfile()
        {
            if (this.packages == null || this.packages.Count == 0)
            {
                if (LocalPackageManager.s_enabledPackages.Any())
                    LocalPackageManager.TryDisableAllPackages();
                return;
            }

            // Disable currently enabled packages which are not in this profile
            if (LocalPackageManager.s_enabledPackages.Any())
            {
                foreach (var pkg in LocalPackageManager.s_enabledPackages.Where(it => !packages.Contains(it.Key)))
                {
                    LocalPackageManager.TryDisablePackage(pkg.Key, true);
                }
            }

            // Enable remaining packages in this profile which aren't enabled
            foreach (var pkg in this.packages)
            {
                if (LocalPackageManager.s_enabledPackages.ContainsKey(pkg))
                    continue;

                LocalPackageManager.TryEnablePackage(pkg);
            }
        }

        public JsonObject ToJson()
        {
            var ret = new JsonObject()
            {
                { nameof(name), this.name },
                { nameof(packages), new JsonArray(this.packages.Select(it => (JsonValue)it).ToArray()) }
            };

            return ret;
        }

        public static MefinoProfile FromJson(string jsonString)
        {
            MefinoProfile ret = null;

            try
            {
                var json = JsonReader.Parse(jsonString);

                ret = new MefinoProfile
                {
                    name = json[nameof(name)].AsString
                };

                if (json[nameof(packages)].AsJsonArray is JsonArray array)
                {
                    ret.packages.AddRange(array.Select(it => it.AsString));
                }

                // verify all GUIDs.
                for (int i = ret.packages.Count - 1; i >= 0; i--)
                {
                    var guid = ret.packages[i];

                    if (!WebManifestManager.s_cachedWebManifests.ContainsKey(guid)
                        && LocalPackageManager.TryGetInstalledPackage(guid) == null)
                    {
                        // GUID in package list does not exist, locally or online!
                        ret.packages.RemoveAt(i);
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception parsing Profile from json!");
                Console.WriteLine(ex);
                return ret;
            }
        }
    }
}

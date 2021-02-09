using Mefino.LightJson;
using Mefino.LightJson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    LocalPackageManager.DisableAllPackages();
                return;
            }

            // Try enable all packages (does nothing if already OK)
            // its possible our 'packages' list will change during this process, so copy it now.
            //var copy = packages.ToList();
            List<string> missing = new List<string>();
            for (int i = 0; i < packages.Count; i++)
            {
                var pkg = packages[i];
                if (LocalPackageManager.TryGetInstalledPackage(pkg) == null)
                {
                    missing.Add(pkg);
                }
                else
                {
                    if (!LocalPackageManager.TryEnablePackage(pkg))
                    {
                        LocalPackageManager.TryDisablePackage(pkg, true);
                        packages.Remove(pkg);
                    }
                }
            }

            if (missing.Any())
            {
                string miss = "";
                foreach (var entry in missing)
                    miss += $"\n{entry}";

                var msgResult = MessageBox.Show($"The following packages in your profile are missing and need to be re-installed:" +
                    $"\n{miss}" +
                    $"\n\n" +
                    $"Do you want to re-install them?",
                    "Missing packages!",
                    MessageBoxButtons.YesNo);
                if (msgResult == DialogResult.Yes)
                {
                    foreach (var entry in missing)
                        LocalPackageManager.TryInstallWebPackage(entry);
                }
            }

            // Disable currently enabled packages which are not in this profile
            if (LocalPackageManager.s_enabledPackages.Any())
            {
                for (int i = LocalPackageManager.s_enabledPackages.Count - 1; i >= 0; i--)
                {
                    var pkg = LocalPackageManager.s_enabledPackages.ElementAt(i);

                    if (!packages.Any(it => it == pkg.Key))
                        LocalPackageManager.TryDisablePackage(pkg.Key, true);
                }
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

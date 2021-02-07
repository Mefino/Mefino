using Mefino.LightJson;
using Mefino.LightJson.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Core.Profiles
{
    public static class ProfileManager
    {
        internal static string PROFILE_CONFIG_PATH => Path.Combine(Folders.MEFINO_APPDATA_FOLDER, "profiles.json");

        internal const string DEFAULT_PROFILE_NAME = "default";

        public static MefinoProfile ActiveProfile => s_activeProfile;
        private static MefinoProfile s_activeProfile;

        internal static readonly Dictionary<string, MefinoProfile> s_profiles = new Dictionary<string, MefinoProfile>();

        public static void LoadProfileOrSetDefault()
        {
            if (LoadProfiles())
                return;

            // LoadProfiles returning false means no active profile was loaded.

            if (s_profiles.ContainsKey(DEFAULT_PROFILE_NAME))
            {
                SetActiveProfile(DEFAULT_PROFILE_NAME);
            }
            else if (s_profiles.Any())
            {
                SetActiveProfile(s_profiles.First().Value);
            }
            else
            {
                CreateProfile(DEFAULT_PROFILE_NAME);
            }
        }

        public static bool SetActiveProfile(string name)
        {
            if (!s_profiles.ContainsKey(name))
                return false;

            return SetActiveProfile(s_profiles[name]);
        }

        public static bool SetActiveProfile(MefinoProfile profile)
        {
            try
            {
                s_activeProfile = profile;
                profile.EnableProfile();
                SaveProfiles();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CreateProfile(string name, bool andSetActive = true)
        {
            name = name?.Trim();

            if (string.IsNullOrEmpty(name))
                return false;

            if (s_profiles.ContainsKey(name))
                return false;

            s_profiles.Add(name, new MefinoProfile { name = name });

            if (andSetActive)
                return SetActiveProfile(name);
            else
            {
                SaveProfiles();
                return true;
            }
        }

        public static void DeleteProfile(string name)
        {
            if (!s_profiles.ContainsKey(name))
                return;

            s_profiles.Remove(name);

            if (s_activeProfile.name == name)
                s_activeProfile = null;

            SaveProfiles();
            LoadProfileOrSetDefault();
        }

        // ========== Saving and loading ==========

        // Returns true if an active profile was set, otherwise false.
        public static bool LoadProfiles()
        {
            s_profiles.Clear();
            s_activeProfile = null;

            if (!File.Exists(PROFILE_CONFIG_PATH))
                return false;

            try
            {
                var json = JsonReader.ParseFile(PROFILE_CONFIG_PATH);

                if (json[nameof(s_profiles)].AsJsonArray is JsonArray profileArray)
                {
                    foreach (var entry in profileArray)
                    {
                        if (MefinoProfile.FromJson(entry) is MefinoProfile profile)
                        {
                            if (s_profiles.ContainsKey(profile.name))
                                continue;

                            s_profiles.Add(profile.name, profile);
                        }
                    }
                }

                if (json[nameof(s_activeProfile)].AsString is string activeProfile)
                {
                    if (s_profiles.ContainsKey(activeProfile))
                    {
                        s_activeProfile = s_profiles[activeProfile];
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception loading Mefino Profiles!");
                Console.WriteLine(ex);
            }

            return false;
        }

        public static void SaveProfiles()
        {
            var json = new JsonObject
            {
                { nameof(s_activeProfile), s_activeProfile?.name ?? null },
                { nameof(s_profiles), new JsonArray(s_profiles.Values.Select(it => (JsonValue)it.ToJson()).ToArray()) } 
            };

            if (!File.Exists(PROFILE_CONFIG_PATH))
                File.Delete(PROFILE_CONFIG_PATH);

            File.WriteAllText(PROFILE_CONFIG_PATH, json.ToString(true));

            Console.WriteLine("saved to " + PROFILE_CONFIG_PATH);
        }
    }
}

using Mefino.GUI.Models;
using Mefino.LightJson;
using Mefino.LightJson.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.Core.Profiles
{
    public static class ProfileManager
    {
        static ProfileManager()
        {
            LocalPackageManager.OnPackageEnabled += OnPackageEnabled;
            LocalPackageManager.OnPackageDisabled += OnPackageDisabled;
            LocalPackageManager.OnPackageUninstalled += OnPackageUninstalled;
        }

        internal static string PROFILE_SAVE_PATH => Path.Combine(Folders.MEFINO_APPDATA_FOLDER, "profiles.json");

        internal const string DEFAULT_PROFILE_NAME = "default";

        // All profiles
        public static readonly Dictionary<string, MefinoProfile> AllProfiles = new Dictionary<string, MefinoProfile>();

        // used to keep track of if there have been any changes since the last save.
        internal static bool s_changesSinceLastSave;

        /// <summary>
        /// Get the current active Mefino Profile.
        /// </summary>
        public static MefinoProfile ActiveProfile
        {
            get
            {
                if (string.IsNullOrEmpty(s_activeProfile))
                    return null;
                AllProfiles.TryGetValue(s_activeProfile, out MefinoProfile profile);
                return profile;
            }
        }
        internal static string s_activeProfile;

        /// <summary>
        /// Set the <see cref="s_changesSinceLastSave"/> value, and update the LauncherPage for this.
        /// </summary>
        /// <param name="changes"></param>
        internal static void SetChangesSinceSave(bool changes)
        {
            s_changesSinceLastSave = changes;

            LauncherPage.OnChangesSinceLastSaveChanged(changes);
        }

        #region EVENT LISTENERS 

        private static void OnPackageDisabled(PackageManifest obj)
        {
            if (ActiveProfile == null)
                return;

            if (ActiveProfile.packages.Contains(obj.GUID))
            {
                ActiveProfile.packages.Remove(obj.GUID);
                SetChangesSinceSave(true);
            }
        }

        private static void OnPackageEnabled(PackageManifest obj)
        {
            if (ActiveProfile == null)
                return;

            if (!ActiveProfile.packages.Contains(obj.GUID))
            {
                ActiveProfile.packages.Add(obj.GUID);
                SetChangesSinceSave(true);
            }
        }

        private static void OnPackageUninstalled(PackageManifest obj)
        {
            if (ActiveProfile == null)
                return;

            if (ActiveProfile.packages.Contains(obj.GUID))
            {
                ActiveProfile.packages.Remove(obj.GUID);
                SetChangesSinceSave(true);
            }
        }

        #endregion

        #region PROFILES CORE

        /// <summary>
        /// Get the current "default" profile name. This will be literally "default" if there is such a profile, otherwise the first profile name.
        /// </summary>
        public static string GetFirstOrDefaultProfileName()
        {
            if (AllProfiles.ContainsKey(DEFAULT_PROFILE_NAME))
                return DEFAULT_PROFILE_NAME;
            else if (AllProfiles.Any())
                return AllProfiles.First().Key;
            else
                return DEFAULT_PROFILE_NAME;
        }

        /// <summary>
        /// Load the currently saved profile (if any/valid), otherwise load or create the default profile (from <see cref="GetFirstOrDefaultProfileName"/>)
        /// </summary>
        /// <param name="overwriteWithManualChanges">Should any current manual IO changes (user moving files around) take priority over the save?</param>
        public static void LoadProfileOrSetDefault(bool overwriteWithManualChanges = true)
        {
            LoadProfiles(overwriteWithManualChanges);

            if (!string.IsNullOrEmpty(s_activeProfile) && AllProfiles.ContainsKey(s_activeProfile))
            {
                SetActiveProfile(s_activeProfile, true);
            }
            else if (AllProfiles.Any())
            {
                var name = GetFirstOrDefaultProfileName();
                SetActiveProfile(name, true);
            }
            else
            {
                CreateProfile(DEFAULT_PROFILE_NAME);
            }
        }

        /// <summary>
        /// Set the current profile to the given name.
        /// </summary>
        /// <param name="name">The name of the profile to activate</param>
        /// <param name="skipSavePrompt">Skip saving the current profile?</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool SetActiveProfile(string name, bool skipSavePrompt)
        {
            s_activeProfile = name;

            if (!AllProfiles.ContainsKey(name))
                return false;

            return SetActiveProfile(AllProfiles[name], skipSavePrompt);
        }

        /// <summary>
        /// Set the current profile to the provided profile.
        /// </summary>
        /// <param name="profile">The profile to activate</param>
        /// <param name="skipSavePrompt">Skip saving the current profile?</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool SetActiveProfile(MefinoProfile profile, bool skipSavePrompt)
        {
            if (!skipSavePrompt && !string.IsNullOrEmpty(s_activeProfile))
            {
                SavePrompt();
            }

            try
            {
                s_activeProfile = profile.name;
                profile.EnableProfile();

                AppDataManager.SaveConfig();

                LauncherPage.SendRebuildPackageList();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        #endregion

        #region CREATION AND DELETION

        /// <summary>
        /// Try to create a profile with the given name.
        /// </summary>
        /// <param name="name">The name you want to use</param>
        /// <param name="andSetActive">And set it as the current profile too?</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool CreateProfile(string name, bool andSetActive = true)
        {
            if (andSetActive && ActiveProfile != null)
                SavePrompt();

            name = name?.Trim();

            if (string.IsNullOrEmpty(name))
                return false;

            if (AllProfiles.ContainsKey(name))
                return false;

            AllProfiles.Add(name, new MefinoProfile { name = name });

            if (andSetActive)
                SetActiveProfile(name, true);

            SaveProfiles();

            return true;
        }

        /// <summary>
        /// Try to delete the given profile name.
        /// </summary>
        /// <param name="name">The name of the profile to delete</param>
        public static void DeleteProfile(string name)
        {
            if (AllProfiles.Count < 2)
            {
                MessageBox.Show("Sorry, you cannot delete your only profile!");
                //Console.WriteLine("Cannot delete the only profile. Clearing instead");
                //// cannot delete your only profile. wipe it instead and rename it to 'default'.
                //ActiveProfile.name = DEFAULT_PROFILE_NAME;
                //s_activeProfile = DEFAULT_PROFILE_NAME;
                //ActiveProfile.packages.Clear();
                //SaveProfiles();
                //LoadProfiles();
                return;
            }

            if (!AllProfiles.ContainsKey(name))
                return;

            AllProfiles.Remove(name);

            if (s_activeProfile == name)
                s_activeProfile = GetFirstOrDefaultProfileName();

            SaveProfiles();

            LoadProfileOrSetDefault(false);
        }

        #endregion

        #region SAVING AND LOADING

        /// <summary>
        /// Prompt the user to save their current profile, if <see cref="s_changesSinceLastSave"/> is true.
        /// </summary>
        /// <param name="revertOnCancel">If the user cancels, should their profile be reloaded from their last save and applied?</param>
        /// <param name="dontApplyLoadedProfile">Set to true if you are going to apply a profile yourself right after this, so no reason to reload the last saved on cancel.</param>
        public static void SavePrompt(bool revertOnCancel = true, bool dontApplyLoadedProfile = false)
        {
            if (s_changesSinceLastSave && ActiveProfile != null)
            {
                var msg = MessageBox.Show($"Save changes to '{s_activeProfile}'?", "Unsaved changes", MessageBoxButtons.YesNo);
                if (msg == DialogResult.Yes)
                {
                    SaveProfiles();
                }
                else if (revertOnCancel)
                {
                    // reload profiles from disk so changes are reverted.
                    LoadProfiles(false, dontApplyLoadedProfile);
                }
            }
        }

        /// <summary>
        /// Load saved profiles off disk, and optionally apply the loaded profile.
        /// </summary>
        /// <param name="overwriteWithManualChanges">Should any manual IO changes from the user take priority over the last active save?</param>
        /// <param name="dontApplyLoadedProfile">Set to true if you are going to apply a profile yourself right after this, so no reason to reload the last saved on cancel.</param>
        /// <returns><see langword="true"/> if a profile was loaded, otherwise <see langword="false"/></returns>
        public static bool LoadProfiles(bool overwriteWithManualChanges = true, bool dontApplyLoadedProfile = false)
        {
            AllProfiles.Clear();
            SetChangesSinceSave(false);

            if (!File.Exists(PROFILE_SAVE_PATH))
                return false;

            try
            {
                var json = JsonReader.ParseFile(PROFILE_SAVE_PATH);

                if (json[nameof(AllProfiles)].AsJsonArray is JsonArray profileArray)
                {
                    foreach (var entry in profileArray)
                    {
                        if (MefinoProfile.FromJson(entry.ToString()) is MefinoProfile profile)
                        {
                            if (AllProfiles.ContainsKey(profile.name))
                                continue;

                            AllProfiles.Add(profile.name, profile);
                        }
                    }
                }

                if (dontApplyLoadedProfile)
                    return true;

                if (!string.IsNullOrEmpty(s_activeProfile)
                    && AllProfiles.TryGetValue(s_activeProfile, out MefinoProfile active))
                {
                    if (overwriteWithManualChanges && DoManualChangesOverwrite(active))
                    {
                        //Console.WriteLine("overwrote with manual changes");
                        return true;
                    }

                    return SetActiveProfile(active, true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception loading Mefino Profiles!");
                Console.WriteLine(ex);
            }

            return false;
        }

        /// <summary>
        /// Is the current active profile different to the current enabled packages? This is generally NOT a safe check, only used right after loading a save and comparing
        /// it to the current packages.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns><see langword="true"/> if different, otherwise <see langword="false"/></returns>
        internal static bool IsProfileDifferentToEnabledPackages()
            => IsProfileDifferentToEnabledPackages(ActiveProfile);

        /// <summary>
        /// Is the provided profile different to the current enabled packages? This is generally NOT a safe check, only used right after loading a save and comparing
        /// it to the current packages.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns><see langword="true"/> if different, otherwise <see langword="false"/></returns>
        internal static bool IsProfileDifferentToEnabledPackages(MefinoProfile profile)
        {
            if (profile == null)
                return true;

            return profile.packages.Count != LocalPackageManager.s_enabledPackages.Count
                || profile.packages.Any(it => !LocalPackageManager.s_enabledPackages.ContainsKey(it));
        }

        /// <summary>
        /// True if there are manual changes and we overwrote the profile, otherwise false.
        /// If true, it will also set that as the active profile.</summary>
        private static bool DoManualChangesOverwrite(MefinoProfile active)
        {
            if (IsProfileDifferentToEnabledPackages(active))
            {
                List<string> uninstalled = new List<string>();
                foreach (var pkg in active.packages)
                {
                    if (LocalPackageManager.TryGetInstalledPackage(pkg) == null)
                        uninstalled.Add(pkg);
                }
                if (uninstalled.Any())
                {
                    var msg = "";
                    foreach (var entry in uninstalled)
                        msg += $"\n{entry}";

                    var msgResult = MessageBox.Show($"The following packages from the profile '{active.name}' are no longer installed:" +
                        $"\n{msg}" +
                        $"\n\n" +
                        $"Do you want to reinstall them?",
                        "Missing packages!",
                        MessageBoxButtons.YesNo);
                    if (msgResult == DialogResult.Yes)
                    {
                        foreach (var entry in uninstalled)
                        {
                            LocalPackageManager.TryInstallWebPackage(entry, true);
                        }
                    }
                }

                active.packages.Clear();
                active.packages.AddRange(LocalPackageManager.s_enabledPackages.Keys);

                SetChangesSinceSave(true);
                SetActiveProfile(active, true);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Save all current profiles to disk.
        /// </summary>
        public static void SaveProfiles()
        {
            var json = new JsonObject
            {
                { nameof(AllProfiles), new JsonArray(AllProfiles.Values.Select(it => (JsonValue)it.ToJson()).ToArray()) } 
            };

            if (!File.Exists(PROFILE_SAVE_PATH))
                File.Delete(PROFILE_SAVE_PATH);

            File.WriteAllText(PROFILE_SAVE_PATH, json.ToString(true));

            SetChangesSinceSave(false);
        }

        #endregion
    }
}

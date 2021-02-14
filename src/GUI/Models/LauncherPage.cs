using Mefino.Core;
using Mefino.Core.Profiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.GUI.Models
{
    public partial class LauncherPage : MetroFramework.Controls.MetroUserControl
    {
        public static LauncherPage Instance;

        public static bool ChangesSinceLastSave;

        public LauncherPage()
        {
            Instance = this;
            InitializeComponent();

            MefinoGUI.SensitiveControls.AddRange(new Control[]
            {
                this._packageList,
                this._launchOutwardButton,
                this._infoBoxUninstallButton,
                this._infoBoxUpdateButton,
                this._profileDropdown,
                this._newProfileButton,
                this._reloadProfileButton,
                this._saveProfileButton,
                this._importButton,
                this._exportButton,
                this._deleteProfileButton
            });

            RefreshProfileDropdown();
            RebuildPackageList();

            _changesSinceSaveLabel.Visible = ChangesSinceLastSave;

            _packageList.CellClick += _packageList_CellClick;
            _packageList.CellContentClick += _packageList_CellContentClick;

            LocalPackageManager.OnPackageEnabled += (_) => { RebuildPackageList(); };  //OnPackageToggled;
            LocalPackageManager.OnPackageDisabled += (_) => { RebuildPackageList(); }; //OnPackageToggled;
            LocalPackageManager.OnPackageUninstalled += (_) => { RebuildPackageList(); };
            LocalPackageManager.OnPackageInstalled += (_) => { RebuildPackageList(); };

            _profileDropdown.SelectedIndexChanged += _profileDropdown_OnSelectionChanged;

            _infoBoxTabs.SelectedIndex = 0;

            Application.DoEvents();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            _packageInfoBox.Visible = false;
            CurrentInspectedPackage = null;
        }

        #region LAUNCH OUTWARD BUTTON

        private void _launchOutwardButton_Click(object sender, EventArgs e)
        {
            OutwardHelper.TryLaunchOutward();
        }

        #endregion

        #region PROFILES

        internal static void OnChangesSinceLastSaveChanged(bool changes)
        {
            if (Instance == null)
                ChangesSinceLastSave = changes;
            else
                Instance?.Invoke(new Action<bool>(Instance.SetChangesLabelVisible), changes);

        }

        public void SetChangesLabelVisible(bool visible)
        {
            Instance._changesSinceSaveLabel.Visible = visible;
        }

        public void RefreshProfileDropdown()
        {
            //if (ProfileManager.ActiveProfile == null)
            //    ProfileManager.LoadProfileOrSetDefault();

            _profileDropdown.Items.Clear();
            _profileDropdown.Items.AddRange(ProfileManager.AllProfiles.Select(it => it.Key).ToArray());
            _profileDropdown.SelectedIndex = ProfileManager.AllProfiles.Keys.ToList().IndexOf(ProfileManager.ActiveProfile.name);
            prevProfileIndex = _profileDropdown.SelectedIndex;
        }

        private static int prevProfileIndex = -1;

        private void _profileDropdown_OnSelectionChanged(object sender, EventArgs e)
        {
            if (OutwardHelper.IsOutwardRunning())
            {
                if (prevProfileIndex != -1)
                    _profileDropdown.SelectedIndex = prevProfileIndex;
                return;
            }

            var value = _profileDropdown.SelectedIndex;
            prevProfileIndex = value;

            if (value < 0 || value >= ProfileManager.AllProfiles.Count)
            {
                Console.WriteLine("index out of range: " + value);
                return;
            }

            var profile = ProfileManager.AllProfiles.ElementAt(value);

            if (ProfileManager.s_activeProfile == profile.Key)
                return;

            ProfileManager.SavePrompt(true, true);

            ProfileManager.SetActiveProfile(profile.Value, true);
        }

        private void _newProfileButton_Click(object sender, EventArgs e)
        {
            if (OutwardHelper.IsOutwardRunning())
            {
                MessageBox.Show("You need to close Outward to do that.");
                return;
            }

            using (var creation = new ProfileCreationForm())
            {
                if (creation.ShowDialog(this) == DialogResult.OK)
                {
                    var input = creation._enterNameField.Text;

                    ProfileManager.CreateProfile(input);

                    RefreshProfileDropdown();
                }
            }
        }

        private void _reloadProfileButton_Click(object sender, EventArgs e)
        {
            if (OutwardHelper.IsOutwardRunning())
            {
                MessageBox.Show("You need to close Outward to do that.");
                return;
            }
            //if (ProfileManager.s_changesSinceLastSave)
            //{
            //    var result = MessageBox.Show("You have unsaved changes, really revert them and load the last save?", "Warning", MessageBoxButtons.OKCancel);
            //}

            ProfileManager.LoadProfiles(false);
        }

        private void _saveProfileButton_Click(object sender, EventArgs e)
        {
            ProfileManager.SaveProfiles();
        }

        private void _importButton_Click(object sender, EventArgs e)
        {
            ProfileManager.SavePrompt();

            using (var import = new ProfileImportForm())
            {
                import.ShowDialog();
            }
        }

        private void _exportButton_Click(object sender, EventArgs e)
        {
            using (var export = new ProfileExportForm())
            {
                export._exportDescText.Text = $"Exporting profile: '{ProfileManager.s_activeProfile}'";
                export.ShowDialog();
            }
        }

        private void _deleteProfileButton_Click(object sender, EventArgs e)
        {
            if (OutwardHelper.IsOutwardRunning())
            {
                MessageBox.Show("You need to close Outward to do that.");
                return;
            }

            var profile = ProfileManager.s_activeProfile;
            if (string.IsNullOrEmpty(profile))
                return;

            var msg = MessageBox.Show($"Really delete profile '{profile}'?", "Delete profile?", MessageBoxButtons.OKCancel);

            if (msg == DialogResult.OK)
            {
                ProfileManager.DeleteProfile(profile);
                RefreshProfileDropdown();
            }
        }

        #endregion

        #region PACKAGE LIST

        public static void SendRebuildPackageList()
        {
            if (Instance == null)
                return;

            Instance.Invoke(new MethodInvoker(Instance.RebuildPackageList));
        }

        internal int TryGetIndexOfPackage(PackageManifest package)
        {
            int index = -1;
            foreach (DataGridViewRow row in _packageList.Rows)
            {
                index++;

                if (row.Cells?.Count < 3)
                    continue;

                var guid = $"{row.Cells[2].Value} {row.Cells[0].Value}";

                if (guid == package.GUID)
                    return index;
            }

            // Console.WriteLine("error: couldn't find package '" + package.GUID + "' in package list rows!");
            return -1;
        }

        internal PackageManifest GetPackageFromRow(DataGridViewCellEventArgs e)
        {
            if (e == null || e.ColumnIndex < 0 || e.RowIndex < 0)
                return null;

            var row = _packageList.Rows[e.RowIndex];

            var guid = $"{row.Cells[2].Value} {row.Cells[0].Value}";
            var package = LocalPackageManager.TryGetInstalledPackage(guid);

            if (package == null)
            {
                Console.WriteLine($"ERROR! could not get package: '{package}'");
                return null;
            }

            return package;
        }

        // ======== UI CALLBACKS ========

        // ANY part of a row clicked
        private void _packageList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var package = GetPackageFromRow(e);

            if (package == null)
                return;

            SetInfoboxPackage(package);
        }

        // Cell CONTENT clicked (not just cell, the actual cell content)
        // This is used for the checkbox enabled toggles.
        private void _packageList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e?.ColumnIndex != 4)
                return;

            var package = GetPackageFromRow(e);

            if (package == null)
                return;

            //var cell = _packageList.Rows[e.RowIndex].Cells[4];

            if (!package.IsDisabled)
                LocalPackageManager.TryDisablePackage(package.GUID);
            else
                LocalPackageManager.TryEnablePackage(package.GUID);

            //RefreshRow(e.RowIndex, package);
        }

        // ====== Update and set display ======

        public void RebuildPackageList()
        {
            _packageList.Rows.Clear();

            foreach (var pkg in LocalPackageManager.s_enabledPackages)
                AddPackageRow(pkg.Value);

            foreach (var pkg in LocalPackageManager.s_disabledPackages)
                AddPackageRow(pkg.Value);

            void AddPackageRow(PackageManifest package)
            {
                _packageList.Rows.Add(new string[]
                {
                    package.name,
                    package.version,
                    package.author,
                    package.InstallStateToString(),
                    (!package.IsDisabled).ToString(),
                });

                RefreshRow(_packageList.Rows.Count - 1, package);
            }

            _packageList.Sort(_listColName, ListSortDirection.Ascending);

            if (s_currentPackage != null)
                SetInfoboxPackage(s_currentPackage);
        }

        internal void RefreshRow(int index, PackageManifest package)
        {
            if (index < 0 || index >= _packageList.Rows.Count)
                return;

            var row = _packageList.Rows[index];

            var textCells = new DataGridViewCell[] { row.Cells[0], row.Cells[1], row.Cells[2] };

            var enabledBtn = row.Cells[4] as DataGridViewButtonCell;
            enabledBtn.Value = package.IsDisabled ? "No" : "Yes";
            enabledBtn.FlatStyle = FlatStyle.Popup;
            var enableColor = package.IsDisabled ? Color.Tomato : Color.LightGreen;
            enabledBtn.Style.ForeColor = enableColor;
            enabledBtn.Style.SelectionForeColor = enableColor;

            // set the base text colors
            Color textColor;
            if (package.IsDisabled)
                textColor = Color.FromArgb(125, 125, 125);
            else
                textColor = Color.White;
            foreach (var cell in textCells)
            {
                cell.Style.ForeColor = textColor;
                cell.Style.SelectionForeColor = textColor;
            }

            // set the status color
            var statusCell = row.Cells[3];
            var style = statusCell.Style;

            if (package.IsDisabled)
            {
                // set the status message
                statusCell.Value = "Disabled";

                style.ForeColor = textColor;
                style.SelectionForeColor = textColor;
            }
            else
            {
                // set the status message
                statusCell.Value = package.InstallStateToString();
                Color warningCol = Color.FromArgb(230, 141, 46);
                switch (package.m_installState)
                {
                    case InstallState.HasConflict:
                    case InstallState.MissingDependency:
                    case InstallState.Outdated:
                        style.ForeColor = warningCol;
                        style.SelectionForeColor = warningCol;
                        break;

                    case InstallState.OptionalUpdate:
                        style.ForeColor = Color.YellowGreen;
                        style.SelectionForeColor = Color.YellowGreen;
                        break;

                    case InstallState.Installed:
                        style.ForeColor = Color.LightGreen;
                        style.SelectionForeColor = Color.LightGreen;
                        break;
                }
            }

            _packageList.Sort(_listColName, ListSortDirection.Ascending);

            if (CurrentInspectedPackage == package)
                SetInfoboxPackage(package);
        }

        #endregion

        #region INFOBOX

        internal static PackageManifest CurrentInspectedPackage
        { 
            get => s_currentPackage;
            set
            {
                if (s_currentPackage != null)
                    Instance?.SetRowHighlight(s_currentPackage, false);
                s_currentPackage = value;
                if (value != null)
                    Instance?.SetRowHighlight(value, true);
            }
        }

        private static PackageManifest s_currentPackage;

        private void SetRowHighlight(PackageManifest package, bool enable)
        {
            var row = TryGetIndexOfPackage(package);
            if (row != -1)
            {
                foreach (DataGridViewCell cell in _packageList.Rows[row].Cells)
                {
                    Color color;
                    if (enable)
                        color = Color.FromArgb(55, 55, 55);
                    else
                        color = Color.FromArgb(40, 40, 40);

                    cell.Style.BackColor = color;
                    cell.Style.SelectionBackColor = color;
                }
            }
        }

        public void SetInfoboxPackage(PackageManifest package)
        {
            CurrentInspectedPackage = package;
            _packageInfoBox.Visible = true;

            _infoBoxTitle.Text = package.name;

            _infoBoxVersionAuthor.Text = $"v{package.version} | {package.author}";

            _infoBoxDescription.Text = package.description;

            _infoBoxUpdateButton.Visible = package.m_installState == InstallState.Outdated || package.m_installState == InstallState.OptionalUpdate;

            _infoBoxUninstallButton.Enabled = package.IsDisabled;
            _infoBoxUninstallButton.Text = package.IsDisabled
                                            ? "Uninstall"
                                            : "Disable to uninstall";

            _infoboxListView.Rows.Clear();

            _infoboxListView.Rows.Add("Requires Sync:", package.require_sync.ToString());

            string deps = "";
            if (package.dependencies != null)
            {
                foreach (var dependency in package.dependencies)
                {
                    if (deps != "") deps += "\n";
                    deps += dependency;
                }
            }
            _infoboxListView.Rows.Add("Dependencies:", deps);

            string conflicts = "";
            if (package.conflicts_with != null)
            {
                foreach (var conflict in package.conflicts_with)
                {
                    if (conflicts != "") conflicts += "\n";
                    conflicts += conflict;
                }
            }
            _infoboxListView.Rows.Add("Conflicts with:", conflicts);
        }

        private void _infoBoxCloseButton_Click(object sender, EventArgs e)
        {
            _packageInfoBox.Visible = false;
            CurrentInspectedPackage = null;
        }

        private void _infoBoxUpdateButton_Click(object sender, EventArgs e)
        {
            if (!LocalPackageManager.TryUpdatePackage(CurrentInspectedPackage.GUID))
            {
                MessageBox.Show("Unable to update package!");
            }
            else
                _infoBoxUpdateButton.Visible = false;
        }

        private void _infoBoxWebsiteButton_Click(object sender, EventArgs e)
        {
            var url = CurrentInspectedPackage.GithubURL;

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                Process.Start(url);
            }
        }

        private void _infoBoxUninstallButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"Really uninstall '{CurrentInspectedPackage.name}'?", "Confirm uninstall", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                if (!LocalPackageManager.TryUninstallPackage(CurrentInspectedPackage))
                {
                    MessageBox.Show("Unable to uninstall package!");
                }
                else
                    _packageInfoBox.Visible = false;
            }
        }

        #endregion
    }
}

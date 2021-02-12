using Mefino.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class BrowseModsPage : MetroFramework.Controls.MetroUserControl
    {
        public static BrowseModsPage Instance;

        public BrowseModsPage()
        {
            Instance = this;
            InitializeComponent();
            _packageList.ColumnHeaderMouseClick += _packageList_ColumnHeaderMouseClick;

            _showInstalledCheck.Checked = ShowInstalledPackages;
            _showOnlyTrustedCheck.Checked = OnlyShowTrusted;
            _libraryToggle.Checked = ShowLibraries;

            MefinoGUI.SensitiveControls.AddRange(new Control[] { this._infoBoxInstallButton, this._refreshButton });

            // init some states
            _packageInfoBox.Visible = false;
            _infoBoxTabs.SelectedIndex = 0;

            // Register callbacks
            _packageList.CellClick += _packageList_CellClick;

            LocalPackageManager.OnPackageInstalled += RefreshRow;
            LocalPackageManager.OnPackageUninstalled += RefreshRow;

            // Force an update of web manifests on launch
            WebManifestManager.UpdateWebManifests();

            // Refresh tags after manifest update
            RefreshTags();

            RefreshPackageList();
        }

        public string SelectedTag = "All";
        private readonly HashSet<string> s_implementedTags = new HashSet<string>();

        public static bool ShowInstalledPackages = true;
        public static bool ShowLibraries;
        public static bool OnlyShowTrusted;

        //internal static readonly ObservableCollection<WebPackageDisplay> s_packageList = new ObservableCollection<WebPackageDisplay>();

        #region MISC BUTTONS

        // Button to refresh the package list
        private void _refreshButton_Click(object sender, EventArgs e)
        {
            _refreshButton.Enabled = false;
            _refreshButton.Text = "Refreshing...";
            Application.DoEvents();

            MefinoApp.RefreshAllPackages(true);

            RefreshTags();

            RefreshPackageList();

            _refreshButton.Enabled = true;
            _refreshButton.Text = "Refresh";
        }

        private void _showInstalledCheck_CheckedChanged(object sender, EventArgs e)
        {
            ShowInstalledPackages = _showInstalledCheck.Checked;

            RefreshTags();
            RefreshPackageList();
        }

        private void _libraryToggle_CheckedChanged(object sender, EventArgs e)
        {
            ShowLibraries = _libraryToggle.Checked;

            RefreshTags();

            if (SelectedTag == "All" || SelectedTag == "Library")
            {
                RefreshPackageList();
            }
        }

        private void _showOnlyTrustedCheck_CheckedChanged(object sender, EventArgs e)
        {
            OnlyShowTrusted = _showOnlyTrustedCheck.Checked;

            RefreshTags();
            RefreshPackageList();
        }

        #endregion

        #region TAGS DROPDOWN

        // When user selects a tag from dropdown
        private void _tagDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idx = _tagDropDown.SelectedIndex;
            SelectedTag = _tagDropDown.Items[idx].ToString();

            RefreshPackageList();
        }

        public void RefreshTags()
        {
            s_implementedTags.Clear();

            s_implementedTags.Add("All");

            foreach (var package in WebManifestManager.s_webManifests.Values)
            {
                if (package.tags == null || !package.tags.Any())
                    continue;

                foreach (var tag in package.tags)
                {
                    if (string.IsNullOrEmpty(tag) || s_implementedTags.Contains(tag))
                        continue;

                    if (PackageTags.IsValidTag(tag, ShowLibraries))
                        s_implementedTags.Add(tag);
                }
            }

            _tagDropDown.Items.Clear();

            foreach (var tag in s_implementedTags)
            {
                if (!ShowLibraries && string.Equals(tag, "Library", StringComparison.OrdinalIgnoreCase))
                    continue;

                _tagDropDown.Items.Add(tag);
            }

            if (s_implementedTags.Contains(SelectedTag))
                _tagDropDown.SelectedIndex = _tagDropDown.Items.IndexOf(SelectedTag);
            else
                _tagDropDown.SelectedIndex = 0;
        }

        #endregion

        #region MAIN PACKAGE LIST

        internal static SortOrder s_lastDateSortDir;

        private void _packageList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Custom sort if user clicks the "Details Updated" row.
            // Use hidden column 'hiddenColDateUpdated' which is UTC date time strings.
            if (e.ColumnIndex == 4)
            {
                ListSortDirection dir;
                switch (s_lastDateSortDir)
                {
                    case SortOrder.None:
                    case SortOrder.Ascending:
                        dir = ListSortDirection.Descending; break;
                    case SortOrder.Descending:
                        dir = ListSortDirection.Ascending; break;
                    default:
                        throw new NotImplementedException();
                }

                s_lastDateSortDir = (SortOrder)((int)dir + 1);
                _packageList.Sort(_hiddenColDateUpdated, dir);
            }
            else
                s_lastDateSortDir = SortOrder.None;
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

            //Console.WriteLine("error: couldn't find package '" + package.GUID + "' in package list rows!");
            return -1;
        }

        internal PackageManifest GetPackageFromRow(DataGridViewCellEventArgs e)
        {
            if (e == null || e.ColumnIndex < 0 || e.RowIndex < 0)
                return null;

            var row = _packageList.Rows[e.RowIndex];

            var guid = $"{row.Cells[2].Value} {row.Cells[0].Value}";
            WebManifestManager.s_webManifests.TryGetValue(guid, out PackageManifest package);

            if (package == null)
            {
                Console.WriteLine($"ERROR! could not get package: '{package}'");
                return null;
            }

            return package;
        }

        // ANY part of a row clicked
        private void _packageList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var package = GetPackageFromRow(e);

            if (package == null)
                return;

            SetInfoboxPackage(package);
        }

        public void RefreshPackageList()
        {
            _packageList.Rows.Clear();
            //s_packageList.Clear();

            foreach (var pkg in WebManifestManager.s_webManifests)
                AddPackageRow(pkg.Value);

            void AddPackageRow(PackageManifest package)
            {
                if (!ShowInstalledPackages && package.IsInstalled)
                    return;

                if (!ShowLibraries && package.HasTag("Library"))
                    return;

                if (SelectedTag != "All" && !string.IsNullOrEmpty(SelectedTag))
                {
                    if (package.tags == null || !package.HasTag(SelectedTag))
                        return;
                }

                var state = WebManifestManager.GetStateForGuid(package.GUID);
                if (OnlyShowTrusted && state != WebManifestManager.GuidFilterState.Whitelist)
                    return;

                if (state == WebManifestManager.GuidFilterState.Blacklist)
                    return;

                _packageList.Rows.Add(new string[]
                {
                    package.name,
                    package.version,
                    package.author,
                    package.RepoLastUpdatedTime.ToString()
                });

                RefreshRow(_packageList.Rows.Count - 1, package);
            }

            //_packageList.Sort(_listColName, ListSortDirection.Ascending);

            if (s_currentPackage != null)
                SetInfoboxPackage(s_currentPackage);
        }

        internal void RefreshRow(PackageManifest package)
        {
            RefreshRow(TryGetIndexOfPackage(package), package);
        }

        internal void RefreshRow(int index, PackageManifest package)
        {
            if (index < 0 || index >= _packageList.Rows.Count)
                return;

            var row = _packageList.Rows[index];

            var state = WebManifestManager.GetStateForGuid(package.GUID);
            switch (state)
            {
                case WebManifestManager.GuidFilterState.BrokenList:
                    Console.WriteLine("TODO");
                    break;
                case WebManifestManager.GuidFilterState.Whitelist:
                    row.DefaultCellStyle.ForeColor = Color.Goldenrod;
                    row.DefaultCellStyle.SelectionForeColor = Color.Goldenrod;
                    break;
            }

            row.Cells[4].Value = package.TimeSinceRepoUpdatedPretty;

            if (CurrentInspectedPackage == package)
                SetInfoboxPackage(package);
        }

        internal static PackageManifest CurrentInspectedPackage
        {
            get => s_currentPackage;
            set
            {
                if (s_currentPackage != null)
                    Instance?.SetRowHighlight(s_currentPackage, false);

                s_currentPackage = value;

                if (Instance != null)
                {
                    if (value != null)
                    {
                        Instance.SetRowHighlight(value, true);
                        Instance._packageInfoBox.Visible = true;
                    }
                    else
                        Instance._packageInfoBox.Visible = false;
                }
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

        #endregion

        #region INFOBOX

        public void SetInfoboxPackage(PackageManifest package)
        {
            CurrentInspectedPackage = package;

            _infoBoxTitle.Text = package.name;

            _infoBoxVersionAuthor.Text = $"v{package.version} | {package.author}";

            var state = WebManifestManager.GetStateForGuid(package.GUID);
            switch (state)
            {
                case WebManifestManager.GuidFilterState.Blacklist:
                    break;
                case WebManifestManager.GuidFilterState.BrokenList:
                    Console.WriteLine("TODO");
                    break;
                case WebManifestManager.GuidFilterState.Whitelist:
                    _infoBoxVersionAuthor.Text += " [Trusted]";
                    break;
            }

            _infoBoxDescription.Text = package.description;

            if (!package.IsInstalled)
            {
                _infoBoxInstallButton.Text = "Install";
                _infoBoxInstallButton.ForeColor = Color.LightGreen;
            }
            else
            {
                _infoBoxInstallButton.Text = "Uninstall";
                _infoBoxInstallButton.ForeColor = Color.IndianRed;
            }

            _infoboxListView.Rows.Clear();

            _infoboxListView.Rows.Add("Requires Sync:", package.require_sync ? "Yes" : "No");

            string tags = "";
            if (package.tags != null)
            {
                foreach (var tag in package.tags)
                {
                    if (!PackageTags.IsValidTag(tag, true))
                        continue;

                    if (tags != "") tags += "\n";
                    tags += tag;
                }
            }
            _infoboxListView.Rows.Add("Tags", tags);

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
            CurrentInspectedPackage = null;
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
            var package = s_currentPackage;

            if (!package.IsInstalled)
            {
                LocalPackageManager.TryInstallWebPackage(package.GUID, true);
                RefreshRow(s_currentPackage);
                SetInfoboxPackage(s_currentPackage);
            }
            else
            {
                var confirm = MessageBox.Show($"Really uninstall {s_currentPackage.name}?", "Confirm", MessageBoxButtons.OKCancel);
                if (confirm == DialogResult.OK)
                {
                    LocalPackageManager.TryUninstallPackage(s_currentPackage);
                }
                RefreshRow(s_currentPackage);
                SetInfoboxPackage(s_currentPackage);
            }
        }

        #endregion
    }
}

using Mefino.Core;
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
    public partial class BrowseModsPage : MetroFramework.Controls.MetroUserControl
    {
        public static BrowseModsPage Instance;

        public BrowseModsPage()
        {
            Instance = this;
            InitializeComponent();

            MefinoGUI.SensitiveControls.AddRange(new Control[] { this._infoBoxInstallButton, this._updateButton });

            _packageInfoBox.Visible = false;
            _infoBoxTabs.SelectedIndex = 0;

            _packageList.CellClick += _packageList_CellClick;

            LocalPackageManager.OnPackageInstalled += RefreshRow;
            LocalPackageManager.OnPackageUninstalled += RefreshRow;

            // Force an update of web manifests on launch
            WebManifestManager.UpdateWebManifests();

            RefreshPackageList();
        }

        private void _updateButton_Click(object sender, EventArgs e)
        {
            MefinoApp.RefreshAllPackages(true);

            RefreshPackageList();
        }

        // ======= helpers ==========

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
            WebManifestManager.s_cachedWebManifests.TryGetValue(guid, out PackageManifest package);

            if (package == null)
            {
                Console.WriteLine($"ERROR! could not get package: '{package}'");
                return null;
            }

            return package;
        }

        // ==========================

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

            foreach (var pkg in WebManifestManager.s_cachedWebManifests)
                AddPackageRow(pkg.Value);

            void AddPackageRow(PackageManifest package)
            {
                _packageList.Rows.Add(new string[]
                {
                    package.name,
                    package.version,
                    package.author,
                    LocalPackageManager.TryGetInstalledPackage(package.GUID) == null
                        ? "No" 
                        : "Yes",
                });

                RefreshRow(_packageList.Rows.Count - 1, package);
            }

            _packageList.Sort(_listColName, ListSortDirection.Ascending);

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

            row.Cells[3].Value = !package.IsInstalled
                                    ? "No"
                                    : "Yes";

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
            CurrentInspectedPackage = null;
            _packageInfoBox.Visible = false;
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
    }
}

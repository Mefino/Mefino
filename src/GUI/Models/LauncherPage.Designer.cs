
namespace Mefino.GUI.Models
{
    partial class LauncherPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherPage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            this._launchOutwardButton = new MetroFramework.Controls.MetroButton();
            this._profileTitle = new MetroFramework.Controls.MetroLabel();
            this._profileDropdown = new MetroFramework.Controls.MetroComboBox();
            this._newProfileButton = new MetroFramework.Controls.MetroButton();
            this._reloadProfileButton = new MetroFramework.Controls.MetroButton();
            this._saveProfileButton = new MetroFramework.Controls.MetroButton();
            this._deleteProfileButton = new MetroFramework.Controls.MetroButton();
            this._exportButton = new MetroFramework.Controls.MetroButton();
            this._importButton = new MetroFramework.Controls.MetroButton();
            this._packageInfoBox = new MetroFramework.Controls.MetroPanel();
            this._infoBoxDescription = new MetroFramework.Controls.MetroLabel();
            this._infoBoxCloseButton = new MetroFramework.Controls.MetroButton();
            this._infoBoxUninstallButton = new MetroFramework.Controls.MetroButton();
            this._infoBoxUpdateButton = new MetroFramework.Controls.MetroButton();
            this._infoBoxWebsiteButton = new MetroFramework.Controls.MetroButton();
            this._infoBoxTitle = new MetroFramework.Controls.MetroLabel();
            this._infoBoxVersionAuthor = new MetroFramework.Controls.MetroLabel();
            this._packageList = new System.Windows.Forms.DataGridView();
            this._changesSinceSaveLabel = new MetroFramework.Controls.MetroLabel();
            this._infoBoxTabs = new MetroFramework.Controls.MetroTabControl();
            this._descTab = new MetroFramework.Controls.MetroTabPage();
            this._packageInfoTab = new MetroFramework.Controls.MetroTabPage();
            this._infoboxListView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._listColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._listColVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._listColAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._listColStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._listColChecked = new System.Windows.Forms.DataGridViewButtonColumn();
            this._packageInfoBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._packageList)).BeginInit();
            this._infoBoxTabs.SuspendLayout();
            this._descTab.SuspendLayout();
            this._packageInfoTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._infoboxListView)).BeginInit();
            this.SuspendLayout();
            // 
            // _launchOutwardButton
            // 
            this._launchOutwardButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this._launchOutwardButton.FontSize = MetroFramework.MetroButtonSize.Medium;
            this._launchOutwardButton.ForeColor = System.Drawing.Color.LightGreen;
            this._launchOutwardButton.Location = new System.Drawing.Point(615, 16);
            this._launchOutwardButton.Name = "_launchOutwardButton";
            this._launchOutwardButton.Size = new System.Drawing.Size(290, 46);
            this._launchOutwardButton.Style = MetroFramework.MetroColorStyle.Orange;
            this._launchOutwardButton.TabIndex = 1;
            this._launchOutwardButton.Text = "Launch Outward";
            this._launchOutwardButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._launchOutwardButton.UseCustomForeColor = true;
            this._launchOutwardButton.UseSelectable = true;
            this._launchOutwardButton.Click += new System.EventHandler(this._launchOutwardButton_Click);
            // 
            // _profileTitle
            // 
            this._profileTitle.AutoSize = true;
            this._profileTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this._profileTitle.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this._profileTitle.Location = new System.Drawing.Point(16, 16);
            this._profileTitle.Name = "_profileTitle";
            this._profileTitle.Size = new System.Drawing.Size(73, 25);
            this._profileTitle.TabIndex = 2;
            this._profileTitle.Text = "Profile:";
            this._profileTitle.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // _profileDropdown
            // 
            this._profileDropdown.FormattingEnabled = true;
            this._profileDropdown.ItemHeight = 23;
            this._profileDropdown.Items.AddRange(new object[] {
            "default"});
            this._profileDropdown.Location = new System.Drawing.Point(96, 16);
            this._profileDropdown.Name = "_profileDropdown";
            this._profileDropdown.Size = new System.Drawing.Size(388, 29);
            this._profileDropdown.Style = MetroFramework.MetroColorStyle.Orange;
            this._profileDropdown.TabIndex = 3;
            this._profileDropdown.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._profileDropdown.UseSelectable = true;
            // 
            // _newProfileButton
            // 
            this._newProfileButton.Location = new System.Drawing.Point(16, 51);
            this._newProfileButton.Name = "_newProfileButton";
            this._newProfileButton.Size = new System.Drawing.Size(73, 23);
            this._newProfileButton.TabIndex = 4;
            this._newProfileButton.Text = "New...";
            this._newProfileButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._newProfileButton.UseSelectable = true;
            this._newProfileButton.Click += new System.EventHandler(this._newProfileButton_Click);
            // 
            // _reloadProfileButton
            // 
            this._reloadProfileButton.Location = new System.Drawing.Point(96, 51);
            this._reloadProfileButton.Name = "_reloadProfileButton";
            this._reloadProfileButton.Size = new System.Drawing.Size(73, 23);
            this._reloadProfileButton.TabIndex = 5;
            this._reloadProfileButton.Text = "Reload";
            this._reloadProfileButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._reloadProfileButton.UseSelectable = true;
            this._reloadProfileButton.Click += new System.EventHandler(this._loadProfileButton_Click);
            // 
            // _saveProfileButton
            // 
            this._saveProfileButton.Location = new System.Drawing.Point(175, 51);
            this._saveProfileButton.Name = "_saveProfileButton";
            this._saveProfileButton.Size = new System.Drawing.Size(73, 23);
            this._saveProfileButton.TabIndex = 6;
            this._saveProfileButton.Text = "Save";
            this._saveProfileButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._saveProfileButton.UseSelectable = true;
            this._saveProfileButton.Click += new System.EventHandler(this._saveProfileButton_Click);
            // 
            // _deleteProfileButton
            // 
            this._deleteProfileButton.ForeColor = System.Drawing.Color.IndianRed;
            this._deleteProfileButton.Location = new System.Drawing.Point(411, 51);
            this._deleteProfileButton.Name = "_deleteProfileButton";
            this._deleteProfileButton.Size = new System.Drawing.Size(73, 23);
            this._deleteProfileButton.TabIndex = 7;
            this._deleteProfileButton.Text = "Delete";
            this._deleteProfileButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._deleteProfileButton.UseCustomForeColor = true;
            this._deleteProfileButton.UseSelectable = true;
            this._deleteProfileButton.Click += new System.EventHandler(this._deleteProfileButton_Click);
            // 
            // _exportButton
            // 
            this._exportButton.Enabled = false;
            this._exportButton.Location = new System.Drawing.Point(333, 51);
            this._exportButton.Name = "_exportButton";
            this._exportButton.Size = new System.Drawing.Size(73, 23);
            this._exportButton.TabIndex = 8;
            this._exportButton.Text = "Export";
            this._exportButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._exportButton.UseSelectable = true;
            // 
            // _importButton
            // 
            this._importButton.Enabled = false;
            this._importButton.Location = new System.Drawing.Point(254, 51);
            this._importButton.Name = "_importButton";
            this._importButton.Size = new System.Drawing.Size(73, 23);
            this._importButton.TabIndex = 9;
            this._importButton.Text = "Import";
            this._importButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._importButton.UseSelectable = true;
            // 
            // _packageInfoBox
            // 
            this._packageInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._packageInfoBox.Controls.Add(this._infoBoxTabs);
            this._packageInfoBox.Controls.Add(this._infoBoxCloseButton);
            this._packageInfoBox.Controls.Add(this._infoBoxUninstallButton);
            this._packageInfoBox.Controls.Add(this._infoBoxUpdateButton);
            this._packageInfoBox.Controls.Add(this._infoBoxWebsiteButton);
            this._packageInfoBox.Controls.Add(this._infoBoxTitle);
            this._packageInfoBox.Controls.Add(this._infoBoxVersionAuthor);
            this._packageInfoBox.HorizontalScrollbarBarColor = true;
            this._packageInfoBox.HorizontalScrollbarHighlightOnWheel = false;
            this._packageInfoBox.HorizontalScrollbarSize = 10;
            this._packageInfoBox.Location = new System.Drawing.Point(615, 81);
            this._packageInfoBox.Name = "_packageInfoBox";
            this._packageInfoBox.Size = new System.Drawing.Size(290, 361);
            this._packageInfoBox.Style = MetroFramework.MetroColorStyle.Orange;
            this._packageInfoBox.TabIndex = 11;
            this._packageInfoBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._packageInfoBox.VerticalScrollbarBarColor = true;
            this._packageInfoBox.VerticalScrollbarHighlightOnWheel = false;
            this._packageInfoBox.VerticalScrollbarSize = 10;
            // 
            // _infoBoxDescription
            // 
            this._infoBoxDescription.Location = new System.Drawing.Point(-4, 9);
            this._infoBoxDescription.Name = "_infoBoxDescription";
            this._infoBoxDescription.Size = new System.Drawing.Size(266, 123);
            this._infoBoxDescription.TabIndex = 9;
            this._infoBoxDescription.Text = resources.GetString("_infoBoxDescription.Text");
            this._infoBoxDescription.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._infoBoxDescription.WrapToLine = true;
            // 
            // _infoBoxCloseButton
            // 
            this._infoBoxCloseButton.ForeColor = System.Drawing.Color.Gray;
            this._infoBoxCloseButton.Location = new System.Drawing.Point(257, 4);
            this._infoBoxCloseButton.Name = "_infoBoxCloseButton";
            this._infoBoxCloseButton.Size = new System.Drawing.Size(28, 23);
            this._infoBoxCloseButton.TabIndex = 8;
            this._infoBoxCloseButton.Text = "X";
            this._infoBoxCloseButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._infoBoxCloseButton.UseCustomForeColor = true;
            this._infoBoxCloseButton.UseSelectable = true;
            this._infoBoxCloseButton.Click += new System.EventHandler(this._infoBoxCloseButton_Click);
            // 
            // _infoBoxUninstallButton
            // 
            this._infoBoxUninstallButton.ForeColor = System.Drawing.Color.IndianRed;
            this._infoBoxUninstallButton.Location = new System.Drawing.Point(10, 314);
            this._infoBoxUninstallButton.Name = "_infoBoxUninstallButton";
            this._infoBoxUninstallButton.Size = new System.Drawing.Size(266, 29);
            this._infoBoxUninstallButton.TabIndex = 7;
            this._infoBoxUninstallButton.Text = "Uninstall";
            this._infoBoxUninstallButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._infoBoxUninstallButton.UseCustomForeColor = true;
            this._infoBoxUninstallButton.UseSelectable = true;
            this._infoBoxUninstallButton.Click += new System.EventHandler(this._infoBoxUninstallButton_Click);
            // 
            // _infoBoxUpdateButton
            // 
            this._infoBoxUpdateButton.FontSize = MetroFramework.MetroButtonSize.Medium;
            this._infoBoxUpdateButton.ForeColor = System.Drawing.Color.LightGreen;
            this._infoBoxUpdateButton.Location = new System.Drawing.Point(10, 244);
            this._infoBoxUpdateButton.Name = "_infoBoxUpdateButton";
            this._infoBoxUpdateButton.Size = new System.Drawing.Size(266, 29);
            this._infoBoxUpdateButton.TabIndex = 6;
            this._infoBoxUpdateButton.Text = "Update";
            this._infoBoxUpdateButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._infoBoxUpdateButton.UseCustomForeColor = true;
            this._infoBoxUpdateButton.UseSelectable = true;
            this._infoBoxUpdateButton.Click += new System.EventHandler(this._infoBoxUpdateButton_Click);
            // 
            // _infoBoxWebsiteButton
            // 
            this._infoBoxWebsiteButton.Location = new System.Drawing.Point(10, 279);
            this._infoBoxWebsiteButton.Name = "_infoBoxWebsiteButton";
            this._infoBoxWebsiteButton.Size = new System.Drawing.Size(266, 29);
            this._infoBoxWebsiteButton.TabIndex = 5;
            this._infoBoxWebsiteButton.Text = "View GitHub Page";
            this._infoBoxWebsiteButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._infoBoxWebsiteButton.UseSelectable = true;
            this._infoBoxWebsiteButton.Click += new System.EventHandler(this._infoBoxWebsiteButton_Click);
            // 
            // _infoBoxTitle
            // 
            this._infoBoxTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this._infoBoxTitle.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._infoBoxTitle.Location = new System.Drawing.Point(10, 9);
            this._infoBoxTitle.Name = "_infoBoxTitle";
            this._infoBoxTitle.Size = new System.Drawing.Size(241, 29);
            this._infoBoxTitle.TabIndex = 2;
            this._infoBoxTitle.Text = "{name}";
            this._infoBoxTitle.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // _infoBoxVersionAuthor
            // 
            this._infoBoxVersionAuthor.Location = new System.Drawing.Point(10, 38);
            this._infoBoxVersionAuthor.Name = "_infoBoxVersionAuthor";
            this._infoBoxVersionAuthor.Size = new System.Drawing.Size(266, 22);
            this._infoBoxVersionAuthor.TabIndex = 3;
            this._infoBoxVersionAuthor.Text = "v{version} | {author}";
            this._infoBoxVersionAuthor.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // _packageList
            // 
            this._packageList.AllowUserToAddRows = false;
            this._packageList.AllowUserToDeleteRows = false;
            this._packageList.AllowUserToResizeColumns = false;
            this._packageList.AllowUserToResizeRows = false;
            this._packageList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this._packageList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this._packageList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._packageList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this._packageList.ColumnHeadersHeight = 25;
            this._packageList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._packageList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._listColName,
            this._listColVersion,
            this._listColAuthor,
            this._listColStatus,
            this._listColChecked});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._packageList.DefaultCellStyle = dataGridViewCellStyle20;
            this._packageList.EnableHeadersVisualStyles = false;
            this._packageList.GridColor = System.Drawing.Color.Black;
            this._packageList.Location = new System.Drawing.Point(16, 81);
            this._packageList.Name = "_packageList";
            this._packageList.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this._packageList.RowHeadersVisible = false;
            this._packageList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.Black;
            this._packageList.RowsDefaultCellStyle = dataGridViewCellStyle22;
            this._packageList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._packageList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._packageList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this._packageList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._packageList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this._packageList.RowTemplate.Height = 25;
            this._packageList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._packageList.Size = new System.Drawing.Size(593, 361);
            this._packageList.TabIndex = 12;
            // 
            // _changesSinceSaveLabel
            // 
            this._changesSinceSaveLabel.AutoSize = true;
            this._changesSinceSaveLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this._changesSinceSaveLabel.ForeColor = System.Drawing.Color.Tomato;
            this._changesSinceSaveLabel.Location = new System.Drawing.Point(490, 22);
            this._changesSinceSaveLabel.Name = "_changesSinceSaveLabel";
            this._changesSinceSaveLabel.Size = new System.Drawing.Size(15, 19);
            this._changesSinceSaveLabel.TabIndex = 13;
            this._changesSinceSaveLabel.Text = "*";
            this._changesSinceSaveLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._changesSinceSaveLabel.UseCustomForeColor = true;
            // 
            // _infoBoxTabs
            // 
            this._infoBoxTabs.Controls.Add(this._descTab);
            this._infoBoxTabs.Controls.Add(this._packageInfoTab);
            this._infoBoxTabs.ItemSize = new System.Drawing.Size(129, 25);
            this._infoBoxTabs.Location = new System.Drawing.Point(10, 64);
            this._infoBoxTabs.Name = "_infoBoxTabs";
            this._infoBoxTabs.SelectedIndex = 0;
            this._infoBoxTabs.Size = new System.Drawing.Size(266, 174);
            this._infoBoxTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this._infoBoxTabs.Style = MetroFramework.MetroColorStyle.Orange;
            this._infoBoxTabs.TabIndex = 10;
            this._infoBoxTabs.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._infoBoxTabs.UseSelectable = true;
            // 
            // _descTab
            // 
            this._descTab.Controls.Add(this._infoBoxDescription);
            this._descTab.HorizontalScrollbarBarColor = true;
            this._descTab.HorizontalScrollbarHighlightOnWheel = false;
            this._descTab.HorizontalScrollbarSize = 10;
            this._descTab.Location = new System.Drawing.Point(4, 29);
            this._descTab.Name = "_descTab";
            this._descTab.Size = new System.Drawing.Size(258, 141);
            this._descTab.TabIndex = 0;
            this._descTab.Text = "Description";
            this._descTab.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._descTab.VerticalScrollbarBarColor = true;
            this._descTab.VerticalScrollbarHighlightOnWheel = false;
            this._descTab.VerticalScrollbarSize = 10;
            // 
            // _packageInfoTab
            // 
            this._packageInfoTab.Controls.Add(this._infoboxListView);
            this._packageInfoTab.HorizontalScrollbarBarColor = true;
            this._packageInfoTab.HorizontalScrollbarHighlightOnWheel = false;
            this._packageInfoTab.HorizontalScrollbarSize = 10;
            this._packageInfoTab.Location = new System.Drawing.Point(4, 29);
            this._packageInfoTab.Name = "_packageInfoTab";
            this._packageInfoTab.Size = new System.Drawing.Size(258, 141);
            this._packageInfoTab.TabIndex = 1;
            this._packageInfoTab.Text = "Package Info";
            this._packageInfoTab.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._packageInfoTab.VerticalScrollbarBarColor = true;
            this._packageInfoTab.VerticalScrollbarHighlightOnWheel = false;
            this._packageInfoTab.VerticalScrollbarSize = 10;
            // 
            // _infoboxListView
            // 
            this._infoboxListView.AllowUserToAddRows = false;
            this._infoboxListView.AllowUserToDeleteRows = false;
            this._infoboxListView.AllowUserToResizeColumns = false;
            this._infoboxListView.AllowUserToResizeRows = false;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.White;
            this._infoboxListView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle23;
            this._infoboxListView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this._infoboxListView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this._infoboxListView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this._infoboxListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._infoboxListView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._infoboxListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this._infoboxListView.ColumnHeadersHeight = 25;
            this._infoboxListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._infoboxListView.ColumnHeadersVisible = false;
            this._infoboxListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._infoboxListView.DefaultCellStyle = dataGridViewCellStyle25;
            this._infoboxListView.EnableHeadersVisualStyles = false;
            this._infoboxListView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this._infoboxListView.Location = new System.Drawing.Point(0, 13);
            this._infoboxListView.Name = "_infoboxListView";
            this._infoboxListView.ReadOnly = true;
            this._infoboxListView.RowHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this._infoboxListView.RowHeadersVisible = false;
            this._infoboxListView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.Color.White;
            this._infoboxListView.RowsDefaultCellStyle = dataGridViewCellStyle27;
            this._infoboxListView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this._infoboxListView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._infoboxListView.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this._infoboxListView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._infoboxListView.RowTemplate.DividerHeight = 1;
            this._infoboxListView.RowTemplate.Height = 25;
            this._infoboxListView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._infoboxListView.Size = new System.Drawing.Size(258, 128);
            this._infoboxListView.TabIndex = 14;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Property";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 5;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 5;
            // 
            // _listColName
            // 
            this._listColName.HeaderText = "Name";
            this._listColName.Name = "_listColName";
            this._listColName.ReadOnly = true;
            this._listColName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._listColName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._listColName.Width = 180;
            // 
            // _listColVersion
            // 
            this._listColVersion.HeaderText = "Version";
            this._listColVersion.Name = "_listColVersion";
            this._listColVersion.ReadOnly = true;
            this._listColVersion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._listColVersion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._listColVersion.Width = 80;
            // 
            // _listColAuthor
            // 
            this._listColAuthor.HeaderText = "Author";
            this._listColAuthor.Name = "_listColAuthor";
            this._listColAuthor.ReadOnly = true;
            this._listColAuthor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._listColAuthor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._listColAuthor.Width = 149;
            // 
            // _listColStatus
            // 
            this._listColStatus.HeaderText = "Status";
            this._listColStatus.Name = "_listColStatus";
            this._listColStatus.ReadOnly = true;
            this._listColStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._listColStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._listColStatus.Width = 127;
            // 
            // _listColChecked
            // 
            this._listColChecked.HeaderText = "Enabled";
            this._listColChecked.Name = "_listColChecked";
            this._listColChecked.ReadOnly = true;
            this._listColChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._listColChecked.Width = 55;
            // 
            // LauncherPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._changesSinceSaveLabel);
            this.Controls.Add(this._packageList);
            this.Controls.Add(this._packageInfoBox);
            this.Controls.Add(this._importButton);
            this.Controls.Add(this._exportButton);
            this.Controls.Add(this._deleteProfileButton);
            this.Controls.Add(this._saveProfileButton);
            this.Controls.Add(this._reloadProfileButton);
            this.Controls.Add(this._newProfileButton);
            this.Controls.Add(this._profileDropdown);
            this.Controls.Add(this._profileTitle);
            this.Controls.Add(this._launchOutwardButton);
            this.Name = "LauncherPage";
            this.Size = new System.Drawing.Size(925, 460);
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._packageInfoBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._packageList)).EndInit();
            this._infoBoxTabs.ResumeLayout(false);
            this._descTab.ResumeLayout(false);
            this._packageInfoTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._infoboxListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroButton _launchOutwardButton;
        private MetroFramework.Controls.MetroLabel _profileTitle;
        private MetroFramework.Controls.MetroComboBox _profileDropdown;
        private MetroFramework.Controls.MetroButton _newProfileButton;
        private MetroFramework.Controls.MetroButton _reloadProfileButton;
        private MetroFramework.Controls.MetroButton _saveProfileButton;
        private MetroFramework.Controls.MetroButton _deleteProfileButton;
        private MetroFramework.Controls.MetroButton _exportButton;
        private MetroFramework.Controls.MetroButton _importButton;
        private MetroFramework.Controls.MetroPanel _packageInfoBox;
        private System.Windows.Forms.DataGridView _packageList;
        private MetroFramework.Controls.MetroLabel _infoBoxTitle;
        private MetroFramework.Controls.MetroLabel _changesSinceSaveLabel;
        private MetroFramework.Controls.MetroButton _infoBoxWebsiteButton;
        private MetroFramework.Controls.MetroLabel _infoBoxVersionAuthor;
        private MetroFramework.Controls.MetroButton _infoBoxUninstallButton;
        private MetroFramework.Controls.MetroButton _infoBoxUpdateButton;
        private MetroFramework.Controls.MetroButton _infoBoxCloseButton;
        private MetroFramework.Controls.MetroLabel _infoBoxDescription;
        private MetroFramework.Controls.MetroTabControl _infoBoxTabs;
        private MetroFramework.Controls.MetroTabPage _descTab;
        private MetroFramework.Controls.MetroTabPage _packageInfoTab;
        private System.Windows.Forms.DataGridView _infoboxListView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn _listColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _listColVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn _listColAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn _listColStatus;
        private System.Windows.Forms.DataGridViewButtonColumn _listColChecked;
    }
}

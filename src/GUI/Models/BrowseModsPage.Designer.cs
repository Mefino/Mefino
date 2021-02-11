
namespace Mefino.GUI.Models
{
    partial class BrowseModsPage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowseModsPage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this._packageList = new System.Windows.Forms.DataGridView();
            this._listColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._listColVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._listColAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._listColStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._updateButton = new MetroFramework.Controls.MetroButton();
            this._packageInfoBox = new MetroFramework.Controls.MetroPanel();
            this._infoBoxTabs = new MetroFramework.Controls.MetroTabControl();
            this._descTab = new MetroFramework.Controls.MetroTabPage();
            this._infoBoxDescription = new MetroFramework.Controls.MetroLabel();
            this._packageInfoTab = new MetroFramework.Controls.MetroTabPage();
            this._infoboxListView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._infoBoxCloseButton = new MetroFramework.Controls.MetroButton();
            this._infoBoxInstallButton = new MetroFramework.Controls.MetroButton();
            this._infoBoxWebsiteButton = new MetroFramework.Controls.MetroButton();
            this._infoBoxTitle = new MetroFramework.Controls.MetroLabel();
            this._infoBoxVersionAuthor = new MetroFramework.Controls.MetroLabel();
            this._packagesTitle = new MetroFramework.Controls.MetroLabel();
            this._tagDropDown = new MetroFramework.Controls.MetroComboBox();
            this._libraryToggle = new MetroFramework.Controls.MetroCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._packageList)).BeginInit();
            this._packageInfoBox.SuspendLayout();
            this._infoBoxTabs.SuspendLayout();
            this._descTab.SuspendLayout();
            this._packageInfoTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._infoboxListView)).BeginInit();
            this.SuspendLayout();
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._packageList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this._packageList.ColumnHeadersHeight = 25;
            this._packageList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._packageList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._listColName,
            this._listColVersion,
            this._listColAuthor,
            this._listColStatus});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._packageList.DefaultCellStyle = dataGridViewCellStyle11;
            this._packageList.EnableHeadersVisualStyles = false;
            this._packageList.GridColor = System.Drawing.Color.Black;
            this._packageList.Location = new System.Drawing.Point(16, 82);
            this._packageList.Name = "_packageList";
            this._packageList.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this._packageList.RowHeadersVisible = false;
            this._packageList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this._packageList.RowsDefaultCellStyle = dataGridViewCellStyle13;
            this._packageList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._packageList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._packageList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this._packageList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._packageList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this._packageList.RowTemplate.Height = 25;
            this._packageList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._packageList.Size = new System.Drawing.Size(540, 360);
            this._packageList.TabIndex = 13;
            // 
            // _listColName
            // 
            this._listColName.HeaderText = "Name";
            this._listColName.Name = "_listColName";
            this._listColName.ReadOnly = true;
            this._listColName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._listColName.Width = 180;
            // 
            // _listColVersion
            // 
            this._listColVersion.HeaderText = "Version";
            this._listColVersion.Name = "_listColVersion";
            this._listColVersion.ReadOnly = true;
            this._listColVersion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._listColVersion.Width = 80;
            // 
            // _listColAuthor
            // 
            this._listColAuthor.HeaderText = "Author";
            this._listColAuthor.Name = "_listColAuthor";
            this._listColAuthor.ReadOnly = true;
            this._listColAuthor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._listColAuthor.Width = 149;
            // 
            // _listColStatus
            // 
            this._listColStatus.HeaderText = "Installed";
            this._listColStatus.Name = "_listColStatus";
            this._listColStatus.ReadOnly = true;
            this._listColStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._listColStatus.Width = 127;
            // 
            // _updateButton
            // 
            this._updateButton.ForeColor = System.Drawing.Color.LightGreen;
            this._updateButton.Location = new System.Drawing.Point(417, 18);
            this._updateButton.Name = "_updateButton";
            this._updateButton.Size = new System.Drawing.Size(139, 29);
            this._updateButton.TabIndex = 14;
            this._updateButton.Text = "Refresh";
            this._updateButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._updateButton.UseCustomForeColor = true;
            this._updateButton.UseSelectable = true;
            this._updateButton.Click += new System.EventHandler(this._refreshButton_Click);
            // 
            // _packageInfoBox
            // 
            this._packageInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._packageInfoBox.Controls.Add(this._infoBoxTabs);
            this._packageInfoBox.Controls.Add(this._infoBoxCloseButton);
            this._packageInfoBox.Controls.Add(this._infoBoxInstallButton);
            this._packageInfoBox.Controls.Add(this._infoBoxWebsiteButton);
            this._packageInfoBox.Controls.Add(this._infoBoxTitle);
            this._packageInfoBox.Controls.Add(this._infoBoxVersionAuthor);
            this._packageInfoBox.HorizontalScrollbarBarColor = true;
            this._packageInfoBox.HorizontalScrollbarHighlightOnWheel = false;
            this._packageInfoBox.HorizontalScrollbarSize = 10;
            this._packageInfoBox.Location = new System.Drawing.Point(562, 56);
            this._packageInfoBox.Name = "_packageInfoBox";
            this._packageInfoBox.Size = new System.Drawing.Size(345, 386);
            this._packageInfoBox.Style = MetroFramework.MetroColorStyle.Orange;
            this._packageInfoBox.TabIndex = 15;
            this._packageInfoBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._packageInfoBox.VerticalScrollbarBarColor = true;
            this._packageInfoBox.VerticalScrollbarHighlightOnWheel = false;
            this._packageInfoBox.VerticalScrollbarSize = 10;
            // 
            // _infoBoxTabs
            // 
            this._infoBoxTabs.Controls.Add(this._descTab);
            this._infoBoxTabs.Controls.Add(this._packageInfoTab);
            this._infoBoxTabs.ItemSize = new System.Drawing.Size(155, 25);
            this._infoBoxTabs.Location = new System.Drawing.Point(10, 64);
            this._infoBoxTabs.Name = "_infoBoxTabs";
            this._infoBoxTabs.SelectedIndex = 1;
            this._infoBoxTabs.Size = new System.Drawing.Size(318, 231);
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
            this._descTab.Size = new System.Drawing.Size(310, 198);
            this._descTab.TabIndex = 0;
            this._descTab.Text = "Description";
            this._descTab.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._descTab.VerticalScrollbarBarColor = true;
            this._descTab.VerticalScrollbarHighlightOnWheel = false;
            this._descTab.VerticalScrollbarSize = 10;
            // 
            // _infoBoxDescription
            // 
            this._infoBoxDescription.Location = new System.Drawing.Point(-4, 9);
            this._infoBoxDescription.Name = "_infoBoxDescription";
            this._infoBoxDescription.Size = new System.Drawing.Size(314, 193);
            this._infoBoxDescription.TabIndex = 9;
            this._infoBoxDescription.Text = resources.GetString("_infoBoxDescription.Text");
            this._infoBoxDescription.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._infoBoxDescription.WrapToLine = true;
            // 
            // _packageInfoTab
            // 
            this._packageInfoTab.Controls.Add(this._infoboxListView);
            this._packageInfoTab.HorizontalScrollbarBarColor = true;
            this._packageInfoTab.HorizontalScrollbarHighlightOnWheel = false;
            this._packageInfoTab.HorizontalScrollbarSize = 10;
            this._packageInfoTab.Location = new System.Drawing.Point(4, 29);
            this._packageInfoTab.Name = "_packageInfoTab";
            this._packageInfoTab.Size = new System.Drawing.Size(310, 198);
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
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            this._infoboxListView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle14;
            this._infoboxListView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this._infoboxListView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this._infoboxListView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this._infoboxListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._infoboxListView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._infoboxListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this._infoboxListView.ColumnHeadersHeight = 25;
            this._infoboxListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._infoboxListView.ColumnHeadersVisible = false;
            this._infoboxListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._infoboxListView.DefaultCellStyle = dataGridViewCellStyle16;
            this._infoboxListView.EnableHeadersVisualStyles = false;
            this._infoboxListView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this._infoboxListView.Location = new System.Drawing.Point(0, 13);
            this._infoboxListView.Name = "_infoboxListView";
            this._infoboxListView.ReadOnly = true;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._infoboxListView.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this._infoboxListView.RowHeadersVisible = false;
            this._infoboxListView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.White;
            this._infoboxListView.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this._infoboxListView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this._infoboxListView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._infoboxListView.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this._infoboxListView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._infoboxListView.RowTemplate.DividerHeight = 1;
            this._infoboxListView.RowTemplate.Height = 25;
            this._infoboxListView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._infoboxListView.Size = new System.Drawing.Size(307, 189);
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
            // _infoBoxCloseButton
            // 
            this._infoBoxCloseButton.ForeColor = System.Drawing.Color.Gray;
            this._infoBoxCloseButton.Location = new System.Drawing.Point(312, 3);
            this._infoBoxCloseButton.Name = "_infoBoxCloseButton";
            this._infoBoxCloseButton.Size = new System.Drawing.Size(28, 23);
            this._infoBoxCloseButton.TabIndex = 8;
            this._infoBoxCloseButton.Text = "X";
            this._infoBoxCloseButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._infoBoxCloseButton.UseCustomForeColor = true;
            this._infoBoxCloseButton.UseSelectable = true;
            this._infoBoxCloseButton.Click += new System.EventHandler(this._infoBoxCloseButton_Click);
            // 
            // _infoBoxInstallButton
            // 
            this._infoBoxInstallButton.ForeColor = System.Drawing.Color.IndianRed;
            this._infoBoxInstallButton.Location = new System.Drawing.Point(13, 345);
            this._infoBoxInstallButton.Name = "_infoBoxInstallButton";
            this._infoBoxInstallButton.Size = new System.Drawing.Size(315, 29);
            this._infoBoxInstallButton.TabIndex = 7;
            this._infoBoxInstallButton.Text = "Uninstall";
            this._infoBoxInstallButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._infoBoxInstallButton.UseCustomForeColor = true;
            this._infoBoxInstallButton.UseSelectable = true;
            this._infoBoxInstallButton.Click += new System.EventHandler(this._infoBoxUninstallButton_Click);
            // 
            // _infoBoxWebsiteButton
            // 
            this._infoBoxWebsiteButton.Location = new System.Drawing.Point(13, 310);
            this._infoBoxWebsiteButton.Name = "_infoBoxWebsiteButton";
            this._infoBoxWebsiteButton.Size = new System.Drawing.Size(315, 29);
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
            // _packagesTitle
            // 
            this._packagesTitle.AutoSize = true;
            this._packagesTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this._packagesTitle.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this._packagesTitle.Location = new System.Drawing.Point(16, 18);
            this._packagesTitle.Name = "_packagesTitle";
            this._packagesTitle.Size = new System.Drawing.Size(162, 25);
            this._packagesTitle.TabIndex = 16;
            this._packagesTitle.Text = "GitHub Packages:";
            this._packagesTitle.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // _tagDropDown
            // 
            this._tagDropDown.FormattingEnabled = true;
            this._tagDropDown.ItemHeight = 23;
            this._tagDropDown.Location = new System.Drawing.Point(185, 18);
            this._tagDropDown.Name = "_tagDropDown";
            this._tagDropDown.Size = new System.Drawing.Size(226, 29);
            this._tagDropDown.Style = MetroFramework.MetroColorStyle.Orange;
            this._tagDropDown.TabIndex = 17;
            this._tagDropDown.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._tagDropDown.UseSelectable = true;
            this._tagDropDown.SelectedIndexChanged += new System.EventHandler(this._tagDropDown_SelectedIndexChanged);
            // 
            // _libraryToggle
            // 
            this._libraryToggle.AutoSize = true;
            this._libraryToggle.Location = new System.Drawing.Point(26, 61);
            this._libraryToggle.Name = "_libraryToggle";
            this._libraryToggle.Size = new System.Drawing.Size(99, 15);
            this._libraryToggle.TabIndex = 18;
            this._libraryToggle.Text = "Show Libraries";
            this._libraryToggle.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._libraryToggle.UseSelectable = true;
            this._libraryToggle.CheckedChanged += new System.EventHandler(this._libraryToggle_CheckedChanged);
            // 
            // BrowseModsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._libraryToggle);
            this.Controls.Add(this._tagDropDown);
            this.Controls.Add(this._packagesTitle);
            this.Controls.Add(this._packageInfoBox);
            this.Controls.Add(this._updateButton);
            this.Controls.Add(this._packageList);
            this.Name = "BrowseModsPage";
            this.Size = new System.Drawing.Size(925, 460);
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this._packageList)).EndInit();
            this._packageInfoBox.ResumeLayout(false);
            this._infoBoxTabs.ResumeLayout(false);
            this._descTab.ResumeLayout(false);
            this._packageInfoTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._infoboxListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _packageList;
        private MetroFramework.Controls.MetroButton _updateButton;
        private MetroFramework.Controls.MetroPanel _packageInfoBox;
        private MetroFramework.Controls.MetroTabControl _infoBoxTabs;
        private MetroFramework.Controls.MetroTabPage _descTab;
        private MetroFramework.Controls.MetroLabel _infoBoxDescription;
        private MetroFramework.Controls.MetroTabPage _packageInfoTab;
        private System.Windows.Forms.DataGridView _infoboxListView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private MetroFramework.Controls.MetroButton _infoBoxCloseButton;
        private MetroFramework.Controls.MetroButton _infoBoxInstallButton;
        private MetroFramework.Controls.MetroButton _infoBoxWebsiteButton;
        private MetroFramework.Controls.MetroLabel _infoBoxTitle;
        private MetroFramework.Controls.MetroLabel _infoBoxVersionAuthor;
        private MetroFramework.Controls.MetroLabel _packagesTitle;
        private MetroFramework.Controls.MetroComboBox _tagDropDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn _listColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _listColVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn _listColAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn _listColStatus;
        private MetroFramework.Controls.MetroCheckBox _libraryToggle;
    }
}

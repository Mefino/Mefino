
namespace Mefino.GUI
{
    partial class MefinoGUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MefinoGUI));
            this._mainHeader = new System.Windows.Forms.Label();
            this._setupTabPage = new MetroFramework.Controls.MetroTabPage();
            this._tabView = new MetroFramework.Controls.MetroTabControl();
            this._installedTabPage = new MetroFramework.Controls.MetroTabPage();
            this._browseTabPage = new MetroFramework.Controls.MetroTabPage();
            this._profileTabPage = new MetroFramework.Controls.MetroTabPage();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this._mainLogo = new System.Windows.Forms.PictureBox();
            this._versionLabel = new System.Windows.Forms.Label();
            this._tabView.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._mainLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // _mainHeader
            // 
            this._mainHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mainHeader.Font = new System.Drawing.Font("Ebrima", 24F);
            this._mainHeader.ForeColor = System.Drawing.SystemColors.Control;
            this._mainHeader.Location = new System.Drawing.Point(70, 10);
            this._mainHeader.Name = "_mainHeader";
            this._mainHeader.Size = new System.Drawing.Size(122, 44);
            this._mainHeader.TabIndex = 1;
            this._mainHeader.Text = "Mefino 0.2.0.0";
            this._mainHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _setupTabPage
            // 
            this._setupTabPage.HorizontalScrollbarBarColor = true;
            this._setupTabPage.HorizontalScrollbarHighlightOnWheel = false;
            this._setupTabPage.HorizontalScrollbarSize = 10;
            this._setupTabPage.Location = new System.Drawing.Point(4, 44);
            this._setupTabPage.Name = "_setupTabPage";
            this._setupTabPage.Size = new System.Drawing.Size(934, 515);
            this._setupTabPage.TabIndex = 0;
            this._setupTabPage.Text = "Setup";
            this._setupTabPage.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._setupTabPage.VerticalScrollbarBarColor = true;
            this._setupTabPage.VerticalScrollbarHighlightOnWheel = false;
            this._setupTabPage.VerticalScrollbarSize = 10;
            // 
            // _tabView
            // 
            this._tabView.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this._tabView.Controls.Add(this._setupTabPage);
            this._tabView.Controls.Add(this._installedTabPage);
            this._tabView.Controls.Add(this._browseTabPage);
            this._tabView.Controls.Add(this._profileTabPage);
            this._tabView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._tabView.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            this._tabView.HotTrack = true;
            this._tabView.ItemSize = new System.Drawing.Size(200, 40);
            this._tabView.Location = new System.Drawing.Point(20, 102);
            this._tabView.Name = "_tabView";
            this._tabView.SelectedIndex = 0;
            this._tabView.Size = new System.Drawing.Size(942, 563);
            this._tabView.Style = MetroFramework.MetroColorStyle.Orange;
            this._tabView.TabIndex = 2;
            this._tabView.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._tabView.UseSelectable = true;
            // 
            // _installedTabPage
            // 
            this._installedTabPage.HorizontalScrollbarBarColor = true;
            this._installedTabPage.HorizontalScrollbarHighlightOnWheel = false;
            this._installedTabPage.HorizontalScrollbarSize = 10;
            this._installedTabPage.Location = new System.Drawing.Point(4, 44);
            this._installedTabPage.Name = "_installedTabPage";
            this._installedTabPage.Size = new System.Drawing.Size(934, 515);
            this._installedTabPage.TabIndex = 1;
            this._installedTabPage.Text = "Manage Packages";
            this._installedTabPage.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._installedTabPage.VerticalScrollbarBarColor = true;
            this._installedTabPage.VerticalScrollbarHighlightOnWheel = false;
            this._installedTabPage.VerticalScrollbarSize = 10;
            // 
            // _browseTabPage
            // 
            this._browseTabPage.HorizontalScrollbarBarColor = true;
            this._browseTabPage.HorizontalScrollbarHighlightOnWheel = false;
            this._browseTabPage.HorizontalScrollbarSize = 10;
            this._browseTabPage.Location = new System.Drawing.Point(4, 44);
            this._browseTabPage.Name = "_browseTabPage";
            this._browseTabPage.Size = new System.Drawing.Size(934, 515);
            this._browseTabPage.TabIndex = 2;
            this._browseTabPage.Text = "Browse Packages";
            this._browseTabPage.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._browseTabPage.VerticalScrollbarBarColor = true;
            this._browseTabPage.VerticalScrollbarHighlightOnWheel = false;
            this._browseTabPage.VerticalScrollbarSize = 10;
            // 
            // _profileTabPage
            // 
            this._profileTabPage.HorizontalScrollbarBarColor = true;
            this._profileTabPage.HorizontalScrollbarHighlightOnWheel = false;
            this._profileTabPage.HorizontalScrollbarSize = 10;
            this._profileTabPage.Location = new System.Drawing.Point(4, 44);
            this._profileTabPage.Name = "_profileTabPage";
            this._profileTabPage.Size = new System.Drawing.Size(934, 515);
            this._profileTabPage.TabIndex = 3;
            this._profileTabPage.Text = "Profiles";
            this._profileTabPage.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._profileTabPage.VerticalScrollbarBarColor = true;
            this._profileTabPage.VerticalScrollbarHighlightOnWheel = false;
            this._profileTabPage.VerticalScrollbarSize = 10;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this._versionLabel);
            this.metroPanel1.Controls.Add(this._mainHeader);
            this.metroPanel1.Controls.Add(this._mainLogo);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 30);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(942, 66);
            this.metroPanel1.TabIndex = 3;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // _mainLogo
            // 
            this._mainLogo.Image = global::Mefino.Properties.Resources.logo_64x64;
            this._mainLogo.Location = new System.Drawing.Point(0, 0);
            this._mainLogo.Name = "_mainLogo";
            this._mainLogo.Size = new System.Drawing.Size(64, 64);
            this._mainLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._mainLogo.TabIndex = 0;
            this._mainLogo.TabStop = false;
            // 
            // _versionLabel
            // 
            this._versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._versionLabel.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._versionLabel.ForeColor = System.Drawing.SystemColors.Control;
            this._versionLabel.Location = new System.Drawing.Point(184, 22);
            this._versionLabel.Margin = new System.Windows.Forms.Padding(0);
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(122, 29);
            this._versionLabel.TabIndex = 2;
            this._versionLabel.Text = "v0.2.0.0";
            this._versionLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // MefinoGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(982, 685);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this._tabView);
            this.DisplayHeader = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MefinoGUI";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Mefino {VERSION}";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._tabView.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._mainLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _mainLogo;
        private System.Windows.Forms.Label _mainHeader;
        private MetroFramework.Controls.MetroTabControl _tabView;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroTabPage _setupTabPage;
        private MetroFramework.Controls.MetroTabPage _installedTabPage;
        private MetroFramework.Controls.MetroTabPage _browseTabPage;
        private MetroFramework.Controls.MetroTabPage _profileTabPage;
        private System.Windows.Forms.Label _versionLabel;
    }
}
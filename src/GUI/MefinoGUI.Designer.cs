
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MefinoGUI));
            this._mainHeader = new System.Windows.Forms.Label();
            this._setupTabPage = new MetroFramework.Controls.MetroTabPage();
            this._tabView = new MetroFramework.Controls.MetroTabControl();
            this._installedTabPage = new MetroFramework.Controls.MetroTabPage();
            this._browseTabPage = new MetroFramework.Controls.MetroTabPage();
            this._profileTabPage = new MetroFramework.Controls.MetroTabPage();
            this._topTitlePanel = new MetroFramework.Controls.MetroPanel();
            this._mainLogo = new System.Windows.Forms.PictureBox();
            this._versionLabel = new System.Windows.Forms.Label();
            this._btmProgressPanel = new MetroFramework.Controls.MetroPanel();
            this._globalStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this._progressBar = new MetroFramework.Controls.MetroProgressBar();
            this._progressText = new System.Windows.Forms.Label();
            this._tabView.SuspendLayout();
            this._topTitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._mainLogo)).BeginInit();
            this._btmProgressPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._globalStyleManager)).BeginInit();
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
            this._setupTabPage.Size = new System.Drawing.Size(934, 473);
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
            this._tabView.Location = new System.Drawing.Point(20, 106);
            this._tabView.Name = "_tabView";
            this._tabView.SelectedIndex = 0;
            this._tabView.Size = new System.Drawing.Size(942, 521);
            this._tabView.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
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
            this._installedTabPage.Size = new System.Drawing.Size(934, 473);
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
            this._browseTabPage.Size = new System.Drawing.Size(934, 473);
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
            this._profileTabPage.Size = new System.Drawing.Size(934, 473);
            this._profileTabPage.TabIndex = 3;
            this._profileTabPage.Text = "Profiles";
            this._profileTabPage.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._profileTabPage.VerticalScrollbarBarColor = true;
            this._profileTabPage.VerticalScrollbarHighlightOnWheel = false;
            this._profileTabPage.VerticalScrollbarSize = 10;
            // 
            // _topTitlePanel
            // 
            this._topTitlePanel.Controls.Add(this._versionLabel);
            this._topTitlePanel.Controls.Add(this._mainHeader);
            this._topTitlePanel.Controls.Add(this._mainLogo);
            this._topTitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._topTitlePanel.HorizontalScrollbarBarColor = true;
            this._topTitlePanel.HorizontalScrollbarHighlightOnWheel = false;
            this._topTitlePanel.HorizontalScrollbarSize = 10;
            this._topTitlePanel.Location = new System.Drawing.Point(20, 30);
            this._topTitlePanel.Name = "_topTitlePanel";
            this._topTitlePanel.Size = new System.Drawing.Size(942, 66);
            this._topTitlePanel.TabIndex = 3;
            this._topTitlePanel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._topTitlePanel.VerticalScrollbarBarColor = true;
            this._topTitlePanel.VerticalScrollbarHighlightOnWheel = false;
            this._topTitlePanel.VerticalScrollbarSize = 10;
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
            // _btmProgressPanel
            // 
            this._btmProgressPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._btmProgressPanel.Controls.Add(this._progressText);
            this._btmProgressPanel.Controls.Add(this._progressBar);
            this._btmProgressPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._btmProgressPanel.HorizontalScrollbarBarColor = true;
            this._btmProgressPanel.HorizontalScrollbarHighlightOnWheel = false;
            this._btmProgressPanel.HorizontalScrollbarSize = 10;
            this._btmProgressPanel.Location = new System.Drawing.Point(20, 627);
            this._btmProgressPanel.Name = "_btmProgressPanel";
            this._btmProgressPanel.Size = new System.Drawing.Size(942, 38);
            this._btmProgressPanel.Style = MetroFramework.MetroColorStyle.Orange;
            this._btmProgressPanel.TabIndex = 4;
            this._btmProgressPanel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._btmProgressPanel.VerticalScrollbarBarColor = true;
            this._btmProgressPanel.VerticalScrollbarHighlightOnWheel = false;
            this._btmProgressPanel.VerticalScrollbarSize = 10;
            // 
            // _globalStyleManager
            // 
            this._globalStyleManager.Owner = null;
            this._globalStyleManager.Style = MetroFramework.MetroColorStyle.Orange;
            this._globalStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // _progressBar
            // 
            this._progressBar.Dock = System.Windows.Forms.DockStyle.Left;
            this._progressBar.HideProgressText = false;
            this._progressBar.Location = new System.Drawing.Point(0, 0);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(332, 38);
            this._progressBar.Style = MetroFramework.MetroColorStyle.Orange;
            this._progressBar.TabIndex = 3;
            this._progressBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._progressBar.UseCustomBackColor = true;
            this._progressBar.Value = 10;
            // 
            // _progressText
            // 
            this._progressText.AutoSize = true;
            this._progressText.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._progressText.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this._progressText.Location = new System.Drawing.Point(338, 3);
            this._progressText.MinimumSize = new System.Drawing.Size(500, 30);
            this._progressText.Name = "_progressText";
            this._progressText.Size = new System.Drawing.Size(500, 30);
            this._progressText.TabIndex = 4;
            this._progressText.Text = "This is an example of some text";
            this._progressText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MefinoGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(982, 685);
            this.Controls.Add(this._topTitlePanel);
            this.Controls.Add(this._tabView);
            this.Controls.Add(this._btmProgressPanel);
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
            this._topTitlePanel.ResumeLayout(false);
            this._topTitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._mainLogo)).EndInit();
            this._btmProgressPanel.ResumeLayout(false);
            this._btmProgressPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._globalStyleManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _mainLogo;
        private System.Windows.Forms.Label _mainHeader;
        private MetroFramework.Controls.MetroTabControl _tabView;
        private MetroFramework.Controls.MetroPanel _topTitlePanel;
        private MetroFramework.Controls.MetroTabPage _setupTabPage;
        private MetroFramework.Controls.MetroTabPage _installedTabPage;
        private MetroFramework.Controls.MetroTabPage _browseTabPage;
        private MetroFramework.Controls.MetroTabPage _profileTabPage;
        private System.Windows.Forms.Label _versionLabel;
        private MetroFramework.Controls.MetroPanel _btmProgressPanel;
        private MetroFramework.Components.MetroStyleManager _globalStyleManager;
        private MetroFramework.Controls.MetroProgressBar _progressBar;
        private System.Windows.Forms.Label _progressText;
    }
}
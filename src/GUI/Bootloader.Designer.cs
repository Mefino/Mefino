
namespace Mefino.GUI
{
    partial class Bootloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bootloader));
            this._styleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.mainLogoImage = new System.Windows.Forms.PictureBox();
            this._otwPathLabelTop = new System.Windows.Forms.Label();
            this._selectOutwardButton = new MetroFramework.Controls.MetroButton();
            this._otwPathInput = new MetroFramework.Controls.MetroTextBox();
            this._otwPathLabelBottom = new System.Windows.Forms.Label();
            this._bepStatusTitle = new System.Windows.Forms.Label();
            this._bepStatusLabel = new System.Windows.Forms.Label();
            this._bepInstallBtn = new MetroFramework.Controls.MetroButton();
            this._bepProgressBar = new MetroFramework.Controls.MetroProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this._styleManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLogoImage)).BeginInit();
            this.SuspendLayout();
            // 
            // _styleManager
            // 
            this._styleManager.Owner = null;
            this._styleManager.Style = MetroFramework.MetroColorStyle.White;
            this._styleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // mainLogoImage
            // 
            this.mainLogoImage.Image = global::Mefino.Properties.Resources.banner_350x128;
            this.mainLogoImage.Location = new System.Drawing.Point(4, 29);
            this.mainLogoImage.Name = "mainLogoImage";
            this.mainLogoImage.Size = new System.Drawing.Size(773, 152);
            this.mainLogoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.mainLogoImage.TabIndex = 0;
            this.mainLogoImage.TabStop = false;
            // 
            // _otwPathLabelTop
            // 
            this._otwPathLabelTop.AutoSize = true;
            this._otwPathLabelTop.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._otwPathLabelTop.ForeColor = System.Drawing.SystemColors.Control;
            this._otwPathLabelTop.Location = new System.Drawing.Point(239, 226);
            this._otwPathLabelTop.Name = "_otwPathLabelTop";
            this._otwPathLabelTop.Size = new System.Drawing.Size(166, 20);
            this._otwPathLabelTop.TabIndex = 1;
            this._otwPathLabelTop.Text = "Outward Mono status:";
            // 
            // _selectOutwardButton
            // 
            this._selectOutwardButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this._selectOutwardButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this._selectOutwardButton.Location = new System.Drawing.Point(213, 267);
            this._selectOutwardButton.Name = "_selectOutwardButton";
            this._selectOutwardButton.Size = new System.Drawing.Size(115, 28);
            this._selectOutwardButton.TabIndex = 2;
            this._selectOutwardButton.Text = "Select Outward.exe";
            this._selectOutwardButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._selectOutwardButton.UseCustomBackColor = true;
            this._selectOutwardButton.UseCustomForeColor = true;
            this._selectOutwardButton.UseSelectable = true;
            this._selectOutwardButton.Click += new System.EventHandler(this.SelectOutwardButton_Click);
            // 
            // _otwPathInput
            // 
            // 
            // 
            // 
            this._otwPathInput.CustomButton.Image = null;
            this._otwPathInput.CustomButton.Location = new System.Drawing.Point(235, 1);
            this._otwPathInput.CustomButton.Name = "";
            this._otwPathInput.CustomButton.Size = new System.Drawing.Size(21, 21);
            this._otwPathInput.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this._otwPathInput.CustomButton.TabIndex = 1;
            this._otwPathInput.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this._otwPathInput.CustomButton.UseSelectable = true;
            this._otwPathInput.CustomButton.Visible = false;
            this._otwPathInput.Lines = new string[] {
        "..."};
            this._otwPathInput.Location = new System.Drawing.Point(334, 270);
            this._otwPathInput.MaxLength = 32767;
            this._otwPathInput.Name = "_otwPathInput";
            this._otwPathInput.PasswordChar = '\0';
            this._otwPathInput.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this._otwPathInput.SelectedText = "";
            this._otwPathInput.SelectionLength = 0;
            this._otwPathInput.SelectionStart = 0;
            this._otwPathInput.ShortcutsEnabled = true;
            this._otwPathInput.Size = new System.Drawing.Size(257, 23);
            this._otwPathInput.TabIndex = 3;
            this._otwPathInput.Text = "...";
            this._otwPathInput.UseSelectable = true;
            this._otwPathInput.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this._otwPathInput.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // _otwPathLabelBottom
            // 
            this._otwPathLabelBottom.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._otwPathLabelBottom.ForeColor = System.Drawing.SystemColors.Control;
            this._otwPathLabelBottom.Location = new System.Drawing.Point(411, 226);
            this._otwPathLabelBottom.Name = "_otwPathLabelBottom";
            this._otwPathLabelBottom.Size = new System.Drawing.Size(244, 20);
            this._otwPathLabelBottom.TabIndex = 4;
            this._otwPathLabelBottom.Text = "Outward path not set!";
            this._otwPathLabelBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _bepStatusTitle
            // 
            this._bepStatusTitle.AutoSize = true;
            this._bepStatusTitle.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._bepStatusTitle.ForeColor = System.Drawing.SystemColors.Control;
            this._bepStatusTitle.Location = new System.Drawing.Point(288, 319);
            this._bepStatusTitle.Name = "_bepStatusTitle";
            this._bepStatusTitle.Size = new System.Drawing.Size(117, 20);
            this._bepStatusTitle.TabIndex = 5;
            this._bepStatusTitle.Text = "BepInEx status:";
            // 
            // _bepStatusLabel
            // 
            this._bepStatusLabel.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._bepStatusLabel.ForeColor = System.Drawing.SystemColors.Control;
            this._bepStatusLabel.Location = new System.Drawing.Point(411, 319);
            this._bepStatusLabel.Name = "_bepStatusLabel";
            this._bepStatusLabel.Size = new System.Drawing.Size(244, 20);
            this._bepStatusLabel.TabIndex = 6;
            this._bepStatusLabel.Text = "Not installed";
            this._bepStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _bepInstallBtn
            // 
            this._bepInstallBtn.Location = new System.Drawing.Point(292, 355);
            this._bepInstallBtn.Name = "_bepInstallBtn";
            this._bepInstallBtn.Size = new System.Drawing.Size(200, 27);
            this._bepInstallBtn.TabIndex = 7;
            this._bepInstallBtn.Text = "Install BepInEx";
            this._bepInstallBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._bepInstallBtn.UseSelectable = true;
            this._bepInstallBtn.Click += new System.EventHandler(this._bepInstallBtn_Click);
            // 
            // _bepProgressBar
            // 
            this._bepProgressBar.Location = new System.Drawing.Point(292, 389);
            this._bepProgressBar.Name = "_bepProgressBar";
            this._bepProgressBar.Size = new System.Drawing.Size(200, 23);
            this._bepProgressBar.TabIndex = 8;
            // 
            // Bootloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._bepProgressBar);
            this.Controls.Add(this._bepInstallBtn);
            this.Controls.Add(this._bepStatusLabel);
            this.Controls.Add(this._bepStatusTitle);
            this.Controls.Add(this._otwPathLabelBottom);
            this.Controls.Add(this._otwPathInput);
            this.Controls.Add(this._selectOutwardButton);
            this.Controls.Add(this._otwPathLabelTop);
            this.Controls.Add(this.mainLogoImage);
            this.DisplayHeader = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Bootloader";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Mefino";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.Bootloader_Load);
            ((System.ComponentModel.ISupportInitialize)(this._styleManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLogoImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager _styleManager;
        private System.Windows.Forms.PictureBox mainLogoImage;
        private System.Windows.Forms.Label _otwPathLabelTop;
        private MetroFramework.Controls.MetroButton _selectOutwardButton;
        private MetroFramework.Controls.MetroTextBox _otwPathInput;
        private System.Windows.Forms.Label _otwPathLabelBottom;
        private System.Windows.Forms.Label _bepStatusTitle;
        private System.Windows.Forms.Label _bepStatusLabel;
        private MetroFramework.Controls.MetroButton _bepInstallBtn;
        private MetroFramework.Controls.MetroProgressBar _bepProgressBar;
    }
}
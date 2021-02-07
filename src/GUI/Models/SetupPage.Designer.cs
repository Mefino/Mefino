
namespace Mefino.GUI.Models
{
    partial class SetupPage
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
            this._otwPathTitle = new MetroFramework.Controls.MetroLabel();
            this._otwPathSelectBtn = new MetroFramework.Controls.MetroButton();
            this._otwPathInputField = new MetroFramework.Controls.MetroTextBox();
            this._bepTitle = new MetroFramework.Controls.MetroLabel();
            this._otwPathStatusTitle = new MetroFramework.Controls.MetroLabel();
            this._otwPathStatus = new MetroFramework.Controls.MetroLabel();
            this._bepStatusTitle = new MetroFramework.Controls.MetroLabel();
            this._bepStatus = new MetroFramework.Controls.MetroLabel();
            this._bepInstallButton = new MetroFramework.Controls.MetroButton();
            this._bepPanel = new MetroFramework.Controls.MetroPanel();
            this._uninstallButton = new MetroFramework.Controls.MetroButton();
            this._bepPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _otwPathTitle
            // 
            this._otwPathTitle.AutoSize = true;
            this._otwPathTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this._otwPathTitle.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this._otwPathTitle.Location = new System.Drawing.Point(18, 15);
            this._otwPathTitle.Name = "_otwPathTitle";
            this._otwPathTitle.Size = new System.Drawing.Size(191, 25);
            this._otwPathTitle.TabIndex = 0;
            this._otwPathTitle.Text = "Outward install path:";
            this._otwPathTitle.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // _otwPathSelectBtn
            // 
            this._otwPathSelectBtn.Location = new System.Drawing.Point(19, 44);
            this._otwPathSelectBtn.Name = "_otwPathSelectBtn";
            this._otwPathSelectBtn.Size = new System.Drawing.Size(126, 30);
            this._otwPathSelectBtn.TabIndex = 1;
            this._otwPathSelectBtn.Text = "Select Outward.exe...";
            this._otwPathSelectBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._otwPathSelectBtn.UseSelectable = true;
            this._otwPathSelectBtn.Click += new System.EventHandler(this._otwPathSelectBtn_Click);
            // 
            // _otwPathInputField
            // 
            // 
            // 
            // 
            this._otwPathInputField.CustomButton.Image = null;
            this._otwPathInputField.CustomButton.Location = new System.Drawing.Point(315, 2);
            this._otwPathInputField.CustomButton.Name = "";
            this._otwPathInputField.CustomButton.Size = new System.Drawing.Size(25, 25);
            this._otwPathInputField.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this._otwPathInputField.CustomButton.TabIndex = 1;
            this._otwPathInputField.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this._otwPathInputField.CustomButton.UseSelectable = true;
            this._otwPathInputField.CustomButton.Visible = false;
            this._otwPathInputField.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this._otwPathInputField.Lines = new string[0];
            this._otwPathInputField.Location = new System.Drawing.Point(151, 44);
            this._otwPathInputField.MaxLength = 32767;
            this._otwPathInputField.Name = "_otwPathInputField";
            this._otwPathInputField.PasswordChar = '\0';
            this._otwPathInputField.PromptText = "...";
            this._otwPathInputField.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this._otwPathInputField.SelectedText = "";
            this._otwPathInputField.SelectionLength = 0;
            this._otwPathInputField.SelectionStart = 0;
            this._otwPathInputField.ShortcutsEnabled = true;
            this._otwPathInputField.Size = new System.Drawing.Size(343, 30);
            this._otwPathInputField.TabIndex = 2;
            this._otwPathInputField.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._otwPathInputField.UseSelectable = true;
            this._otwPathInputField.WaterMark = "...";
            this._otwPathInputField.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this._otwPathInputField.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // _bepTitle
            // 
            this._bepTitle.AutoSize = true;
            this._bepTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this._bepTitle.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this._bepTitle.Location = new System.Drawing.Point(3, 0);
            this._bepTitle.Name = "_bepTitle";
            this._bepTitle.Size = new System.Drawing.Size(199, 25);
            this._bepTitle.TabIndex = 3;
            this._bepTitle.Text = "BepInEx install status:";
            this._bepTitle.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // _otwPathStatusTitle
            // 
            this._otwPathStatusTitle.AutoSize = true;
            this._otwPathStatusTitle.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._otwPathStatusTitle.Location = new System.Drawing.Point(22, 91);
            this._otwPathStatusTitle.Name = "_otwPathStatusTitle";
            this._otwPathStatusTitle.Size = new System.Drawing.Size(50, 19);
            this._otwPathStatusTitle.TabIndex = 4;
            this._otwPathStatusTitle.Text = "Status:";
            this._otwPathStatusTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._otwPathStatusTitle.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // _otwPathStatus
            // 
            this._otwPathStatus.AutoSize = true;
            this._otwPathStatus.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._otwPathStatus.ForeColor = System.Drawing.Color.LightGreen;
            this._otwPathStatus.Location = new System.Drawing.Point(78, 91);
            this._otwPathStatus.Name = "_otwPathStatus";
            this._otwPathStatus.Size = new System.Drawing.Size(58, 19);
            this._otwPathStatus.TabIndex = 5;
            this._otwPathStatus.Text = "<todo>";
            this._otwPathStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._otwPathStatus.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._otwPathStatus.UseCustomForeColor = true;
            // 
            // _bepStatusTitle
            // 
            this._bepStatusTitle.AutoSize = true;
            this._bepStatusTitle.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._bepStatusTitle.Location = new System.Drawing.Point(3, 37);
            this._bepStatusTitle.Name = "_bepStatusTitle";
            this._bepStatusTitle.Size = new System.Drawing.Size(50, 19);
            this._bepStatusTitle.TabIndex = 6;
            this._bepStatusTitle.Text = "Status:";
            this._bepStatusTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._bepStatusTitle.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // _bepStatus
            // 
            this._bepStatus.AutoSize = true;
            this._bepStatus.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this._bepStatus.ForeColor = System.Drawing.SystemColors.Control;
            this._bepStatus.Location = new System.Drawing.Point(59, 37);
            this._bepStatus.Name = "_bepStatus";
            this._bepStatus.Size = new System.Drawing.Size(58, 19);
            this._bepStatus.TabIndex = 7;
            this._bepStatus.Text = "<todo>";
            this._bepStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._bepStatus.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._bepStatus.UseCustomForeColor = true;
            // 
            // _bepInstallButton
            // 
            this._bepInstallButton.Location = new System.Drawing.Point(3, 73);
            this._bepInstallButton.Name = "_bepInstallButton";
            this._bepInstallButton.Size = new System.Drawing.Size(190, 30);
            this._bepInstallButton.TabIndex = 8;
            this._bepInstallButton.Text = "<todo>";
            this._bepInstallButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._bepInstallButton.UseSelectable = true;
            this._bepInstallButton.Click += new System.EventHandler(this._bepInstallButton_Click);
            // 
            // _bepPanel
            // 
            this._bepPanel.Controls.Add(this._bepStatus);
            this._bepPanel.Controls.Add(this._bepInstallButton);
            this._bepPanel.Controls.Add(this._bepTitle);
            this._bepPanel.Controls.Add(this._bepStatusTitle);
            this._bepPanel.HorizontalScrollbarBarColor = true;
            this._bepPanel.HorizontalScrollbarHighlightOnWheel = false;
            this._bepPanel.HorizontalScrollbarSize = 10;
            this._bepPanel.Location = new System.Drawing.Point(19, 139);
            this._bepPanel.Name = "_bepPanel";
            this._bepPanel.Size = new System.Drawing.Size(475, 163);
            this._bepPanel.Style = MetroFramework.MetroColorStyle.Orange;
            this._bepPanel.TabIndex = 9;
            this._bepPanel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._bepPanel.VerticalScrollbarBarColor = true;
            this._bepPanel.VerticalScrollbarHighlightOnWheel = false;
            this._bepPanel.VerticalScrollbarSize = 10;
            // 
            // _uninstallButton
            // 
            this._uninstallButton.BackColor = System.Drawing.Color.DarkRed;
            this._uninstallButton.Location = new System.Drawing.Point(22, 410);
            this._uninstallButton.Name = "_uninstallButton";
            this._uninstallButton.Size = new System.Drawing.Size(190, 30);
            this._uninstallButton.TabIndex = 10;
            this._uninstallButton.Text = "Remove Mefino";
            this._uninstallButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._uninstallButton.UseCustomBackColor = true;
            this._uninstallButton.UseSelectable = true;
            this._uninstallButton.Click += new System.EventHandler(this._uninstallButton_Click);
            // 
            // SetupPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._uninstallButton);
            this.Controls.Add(this._otwPathStatus);
            this.Controls.Add(this._otwPathStatusTitle);
            this.Controls.Add(this._otwPathInputField);
            this.Controls.Add(this._otwPathSelectBtn);
            this.Controls.Add(this._otwPathTitle);
            this.Controls.Add(this._bepPanel);
            this.Margin = new System.Windows.Forms.Padding(15);
            this.Name = "SetupPage";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Size = new System.Drawing.Size(786, 458);
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._bepPanel.ResumeLayout(false);
            this._bepPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel _otwPathTitle;
        private MetroFramework.Controls.MetroButton _otwPathSelectBtn;
        private MetroFramework.Controls.MetroTextBox _otwPathInputField;
        private MetroFramework.Controls.MetroLabel _bepTitle;
        private MetroFramework.Controls.MetroLabel _otwPathStatusTitle;
        private MetroFramework.Controls.MetroLabel _otwPathStatus;
        private MetroFramework.Controls.MetroLabel _bepStatusTitle;
        private MetroFramework.Controls.MetroLabel _bepStatus;
        private MetroFramework.Controls.MetroButton _bepInstallButton;
        private MetroFramework.Controls.MetroPanel _bepPanel;
        private MetroFramework.Controls.MetroButton _uninstallButton;
    }
}

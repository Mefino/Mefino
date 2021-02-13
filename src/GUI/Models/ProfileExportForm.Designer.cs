
namespace Mefino.GUI.Models
{
    partial class ProfileExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileExportForm));
            this._exportButton = new MetroFramework.Controls.MetroButton();
            this._cancelButton = new MetroFramework.Controls.MetroButton();
            this._selectPathButton = new MetroFramework.Controls.MetroButton();
            this._pathTextBox = new MetroFramework.Controls.MetroTextBox();
            this._exportDescText = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // _exportButton
            // 
            this._exportButton.ForeColor = System.Drawing.Color.LightGreen;
            this._exportButton.Location = new System.Drawing.Point(23, 156);
            this._exportButton.Name = "_exportButton";
            this._exportButton.Size = new System.Drawing.Size(238, 27);
            this._exportButton.TabIndex = 0;
            this._exportButton.Text = "Export";
            this._exportButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._exportButton.UseCustomForeColor = true;
            this._exportButton.UseSelectable = true;
            this._exportButton.Click += new System.EventHandler(this._exportButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.ForeColor = System.Drawing.Color.IndianRed;
            this._cancelButton.Location = new System.Drawing.Point(267, 156);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(140, 27);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._cancelButton.UseCustomForeColor = true;
            this._cancelButton.UseSelectable = true;
            // 
            // _selectPathButton
            // 
            this._selectPathButton.Location = new System.Drawing.Point(23, 103);
            this._selectPathButton.Name = "_selectPathButton";
            this._selectPathButton.Size = new System.Drawing.Size(102, 26);
            this._selectPathButton.TabIndex = 2;
            this._selectPathButton.Text = "Select path...";
            this._selectPathButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._selectPathButton.UseSelectable = true;
            this._selectPathButton.Click += new System.EventHandler(this._selectPathButton_Click);
            // 
            // _pathTextBox
            // 
            // 
            // 
            // 
            this._pathTextBox.CustomButton.Image = null;
            this._pathTextBox.CustomButton.Location = new System.Drawing.Point(251, 2);
            this._pathTextBox.CustomButton.Name = "";
            this._pathTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this._pathTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this._pathTextBox.CustomButton.TabIndex = 1;
            this._pathTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this._pathTextBox.CustomButton.UseSelectable = true;
            this._pathTextBox.CustomButton.Visible = false;
            this._pathTextBox.Lines = new string[0];
            this._pathTextBox.Location = new System.Drawing.Point(132, 103);
            this._pathTextBox.MaxLength = 32767;
            this._pathTextBox.Name = "_pathTextBox";
            this._pathTextBox.PasswordChar = '\0';
            this._pathTextBox.PromptText = "...";
            this._pathTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this._pathTextBox.SelectedText = "";
            this._pathTextBox.SelectionLength = 0;
            this._pathTextBox.SelectionStart = 0;
            this._pathTextBox.ShortcutsEnabled = true;
            this._pathTextBox.Size = new System.Drawing.Size(275, 26);
            this._pathTextBox.TabIndex = 3;
            this._pathTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._pathTextBox.UseSelectable = true;
            this._pathTextBox.WaterMark = "...";
            this._pathTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this._pathTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // _exportDescText
            // 
            this._exportDescText.Location = new System.Drawing.Point(24, 64);
            this._exportDescText.Name = "_exportDescText";
            this._exportDescText.Size = new System.Drawing.Size(383, 29);
            this._exportDescText.TabIndex = 4;
            this._exportDescText.Text = "Exporting profile: {profile}";
            this._exportDescText.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // ProfileExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(430, 206);
            this.Controls.Add(this._exportDescText);
            this.Controls.Add(this._pathTextBox);
            this.Controls.Add(this._selectPathButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._exportButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileExportForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Export Profile";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton _exportButton;
        private MetroFramework.Controls.MetroButton _cancelButton;
        private MetroFramework.Controls.MetroButton _selectPathButton;
        private MetroFramework.Controls.MetroTextBox _pathTextBox;
        internal MetroFramework.Controls.MetroLabel _exportDescText;
    }
}

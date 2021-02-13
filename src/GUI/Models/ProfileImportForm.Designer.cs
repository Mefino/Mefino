
namespace Mefino.GUI.Models
{
    partial class ProfileImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileImportForm));
            this._importButton = new MetroFramework.Controls.MetroButton();
            this._cancelButton = new MetroFramework.Controls.MetroButton();
            this._selectPathButton = new MetroFramework.Controls.MetroButton();
            this._pathTextBox = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // _importButton
            // 
            this._importButton.ForeColor = System.Drawing.Color.LightGreen;
            this._importButton.Location = new System.Drawing.Point(23, 125);
            this._importButton.Name = "_importButton";
            this._importButton.Size = new System.Drawing.Size(238, 27);
            this._importButton.TabIndex = 0;
            this._importButton.Text = "Import";
            this._importButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._importButton.UseCustomForeColor = true;
            this._importButton.UseSelectable = true;
            this._importButton.Click += new System.EventHandler(this._importButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.ForeColor = System.Drawing.Color.IndianRed;
            this._cancelButton.Location = new System.Drawing.Point(267, 125);
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
            this._selectPathButton.Location = new System.Drawing.Point(23, 74);
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
            this._pathTextBox.Location = new System.Drawing.Point(132, 74);
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
            // ProfileImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(430, 178);
            this.Controls.Add(this._pathTextBox);
            this.Controls.Add(this._selectPathButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._importButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileImportForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Import Profile";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton _importButton;
        private MetroFramework.Controls.MetroButton _cancelButton;
        private MetroFramework.Controls.MetroButton _selectPathButton;
        private MetroFramework.Controls.MetroTextBox _pathTextBox;
    }
}

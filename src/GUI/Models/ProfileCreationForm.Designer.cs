
namespace Mefino.GUI.Models
{
    partial class ProfileCreationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileCreationForm));
            this._enterNameField = new MetroFramework.Controls.MetroTextBox();
            this._cancelButton = new MetroFramework.Controls.MetroButton();
            this._createButton = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // _enterNameField
            // 
            // 
            // 
            // 
            this._enterNameField.CustomButton.Image = null;
            this._enterNameField.CustomButton.Location = new System.Drawing.Point(217, 1);
            this._enterNameField.CustomButton.Name = "";
            this._enterNameField.CustomButton.Size = new System.Drawing.Size(21, 21);
            this._enterNameField.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this._enterNameField.CustomButton.TabIndex = 1;
            this._enterNameField.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this._enterNameField.CustomButton.UseSelectable = true;
            this._enterNameField.CustomButton.Visible = false;
            this._enterNameField.Lines = new string[0];
            this._enterNameField.Location = new System.Drawing.Point(23, 63);
            this._enterNameField.MaxLength = 32767;
            this._enterNameField.Name = "_enterNameField";
            this._enterNameField.PasswordChar = '\0';
            this._enterNameField.PromptText = "Enter a name...";
            this._enterNameField.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this._enterNameField.SelectedText = "";
            this._enterNameField.SelectionLength = 0;
            this._enterNameField.SelectionStart = 0;
            this._enterNameField.ShortcutsEnabled = true;
            this._enterNameField.Size = new System.Drawing.Size(239, 23);
            this._enterNameField.TabIndex = 0;
            this._enterNameField.UseSelectable = true;
            this._enterNameField.WaterMark = "Enter a name...";
            this._enterNameField.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this._enterNameField.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.ForeColor = System.Drawing.Color.IndianRed;
            this._cancelButton.Location = new System.Drawing.Point(171, 107);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(91, 23);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._cancelButton.UseCustomForeColor = true;
            this._cancelButton.UseSelectable = true;
            // 
            // _createButton
            // 
            this._createButton.ForeColor = System.Drawing.Color.LightGreen;
            this._createButton.Location = new System.Drawing.Point(23, 107);
            this._createButton.Name = "_createButton";
            this._createButton.Size = new System.Drawing.Size(142, 23);
            this._createButton.TabIndex = 2;
            this._createButton.Text = "Create";
            this._createButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._createButton.UseCustomForeColor = true;
            this._createButton.UseSelectable = true;
            this._createButton.Click += new System.EventHandler(this._createButton_Click);
            // 
            // ProfileCreationForm
            // 
            this.AcceptButton = this._createButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(285, 153);
            this.Controls.Add(this._createButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._enterNameField);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileCreationForm";
            this.Resizable = false;
            this.ShowInTaskbar = false;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "New Profile";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);

        }

        #endregion

        public MetroFramework.Controls.MetroTextBox _enterNameField;
        private MetroFramework.Controls.MetroButton _cancelButton;
        private MetroFramework.Controls.MetroButton _createButton;
    }
}
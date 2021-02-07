
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
            this._launchOutwardButton = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // _launchOutwardButton
            // 
            this._launchOutwardButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this._launchOutwardButton.FontSize = MetroFramework.MetroButtonSize.Medium;
            this._launchOutwardButton.Location = new System.Drawing.Point(719, 16);
            this._launchOutwardButton.Name = "_launchOutwardButton";
            this._launchOutwardButton.Size = new System.Drawing.Size(186, 39);
            this._launchOutwardButton.Style = MetroFramework.MetroColorStyle.Orange;
            this._launchOutwardButton.TabIndex = 1;
            this._launchOutwardButton.Text = "Launch Outward";
            this._launchOutwardButton.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._launchOutwardButton.UseSelectable = true;
            this._launchOutwardButton.Click += new System.EventHandler(this._launchOutwardButton_Click);
            // 
            // LauncherPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._launchOutwardButton);
            this.Name = "LauncherPage";
            this.Size = new System.Drawing.Size(925, 460);
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton _launchOutwardButton;
    }
}

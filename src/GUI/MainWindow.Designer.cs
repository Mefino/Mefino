
namespace Mefino.GUI
{
    partial class Mefino
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mefino));
            this._mainMefinoTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _mainMefinoTitle
            // 
            this._mainMefinoTitle.Font = new System.Drawing.Font("Ebrima", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._mainMefinoTitle.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this._mainMefinoTitle.Location = new System.Drawing.Point(23, 30);
            this._mainMefinoTitle.Name = "_mainMefinoTitle";
            this._mainMefinoTitle.Size = new System.Drawing.Size(273, 41);
            this._mainMefinoTitle.TabIndex = 1;
            this._mainMefinoTitle.Text = "Mefino";
            // 
            // Mefino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 689);
            this.Controls.Add(this._mainMefinoTitle);
            this.DisplayHeader = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Mefino";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Mefino";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label _mainMefinoTitle;
    }
}
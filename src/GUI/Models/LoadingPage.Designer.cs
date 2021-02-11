
namespace Mefino.GUI.Models
{
    partial class LoadingPage
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
            this._title = new MetroFramework.Controls.MetroLabel();
            this._spinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.SuspendLayout();
            // 
            // _title
            // 
            this._title.AutoSize = true;
            this._title.FontSize = MetroFramework.MetroLabelSize.Tall;
            this._title.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this._title.Location = new System.Drawing.Point(417, 91);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(95, 25);
            this._title.TabIndex = 1;
            this._title.Text = "Loading...";
            this._title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._title.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // _spinner
            // 
            this._spinner.Location = new System.Drawing.Point(417, 154);
            this._spinner.Maximum = 100;
            this._spinner.Name = "_spinner";
            this._spinner.Size = new System.Drawing.Size(95, 81);
            this._spinner.Style = MetroFramework.MetroColorStyle.Orange;
            this._spinner.TabIndex = 0;
            this._spinner.Theme = MetroFramework.MetroThemeStyle.Dark;
            this._spinner.UseSelectable = true;
            this._spinner.Value = 10;
            this._spinner.Visible = false;
            // 
            // LoadingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._title);
            this.Controls.Add(this._spinner);
            this.Name = "LoadingPage";
            this.Size = new System.Drawing.Size(925, 460);
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel _title;
        private MetroFramework.Controls.MetroProgressSpinner _spinner;
    }
}

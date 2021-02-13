using Mefino.Core.Profiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.GUI.Models
{
    public partial class ProfileExportForm : MetroFramework.Forms.MetroForm
    {
        public ProfileExportForm()
        {
            InitializeComponent();
        }

        private void _selectPathButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Mefino Profile (*.json)|*.json";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var path = Path.GetFullPath(dialog.FileName);
                    _pathTextBox.Text = path;
                }
            }
        }

        private void _exportButton_Click(object sender, EventArgs e)
        {
            var profile = ProfileManager.ActiveProfile;

            if (profile == null)
            {
                MessageBox.Show("Error: Current profile not set!", "Error");
                this.Close();
            }

            try
            {
                if (string.IsNullOrEmpty(_pathTextBox.Text))
                {
                    MessageBox.Show($"Please set a save path above.", "Error");
                    return;
                }

                var path = new Uri(_pathTextBox.Text);

                if (!Uri.IsWellFormedUriString(path.ToString(), UriKind.Absolute))
                {
                    MessageBox.Show($"Invalid save path! Please set a valid save path above.", "Error");
                    return;
                }

                if (File.Exists(path.AbsolutePath))
                    File.Delete(path.AbsolutePath);

                var json = profile.ToJson().ToString(true);

                File.WriteAllText(path.AbsolutePath, json);

                MessageBox.Show($"Saved '{ProfileManager.s_activeProfile}' to '{path.AbsolutePath}'.", "Success!");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not export the profile:\n\n{ex.Message}", "Error");
            }
        }
    }
}

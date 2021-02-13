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
    public partial class ProfileImportForm : MetroFramework.Forms.MetroForm
    {
        public ProfileImportForm()
        {
            InitializeComponent();
        }

        private void _selectPathButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
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

        private void _importButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_pathTextBox.Text))
                {
                    MessageBox.Show($"Please set a import path above.", "Error");
                    return;
                }

                var path = new Uri(_pathTextBox.Text);

                if (!Uri.IsWellFormedUriString(path.ToString(), UriKind.Absolute))
                {
                    MessageBox.Show($"Invalid import path! Please set a valid path above.", "Error");
                    return;
                }

                if (!File.Exists(path.AbsolutePath))
                {
                    MessageBox.Show($"No file exists at '{path.AbsolutePath}'!", "Error");
                    return;
                }

                var rawText = File.ReadAllText(path.AbsolutePath);
                if (string.IsNullOrEmpty(rawText))
                {
                    MessageBox.Show($"Could not process the file!", "Error");
                    return;
                }

                if (MefinoProfile.FromJson(rawText) is MefinoProfile profile)
                {
                    if (ProfileManager.AllProfiles.ContainsKey(profile.name))
                    {
                        var result = MessageBox.Show($"A profile already exists with the name '{profile.name}'!\n\nOverwrite?",
                            "Overwrite profile?",
                            MessageBoxButtons.YesNo);

                        if (result == DialogResult.No)
                            return;

                        ProfileManager.AllProfiles[profile.name] = profile;
                    }
                    else
                        ProfileManager.AllProfiles.Add(profile.name, profile);

                    ProfileManager.SetActiveProfile(profile, true);
                    ProfileManager.SaveProfiles();

                    LauncherPage.Instance.RefreshProfileDropdown();

                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Could not process the file!", "Error");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not export the profile:\n\n{ex.Message}", "Error");
            }
        }
    }
}

using Mefino.Core.Profiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.GUI.Models
{
    public partial class ProfileCreationForm : MetroFramework.Forms.MetroForm
    {
        public ProfileCreationForm()
        {
            InitializeComponent();
        }

        private void _createButton_Click(object sender, EventArgs e)
        {
            var name = this._enterNameField.Text;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name for your profile first", "Invalid name");
                return;
            }

            if (ProfileManager.AllProfiles.ContainsKey(name))
            {
                var result = MessageBox.Show($"There is already a profile called {name}! Do you want to overwrite it?", "Warning", MessageBoxButtons.OKCancel);
                if (result != DialogResult.OK)
                    return;

                ProfileManager.AllProfiles.Remove(name);
            }

            this.Close();
            this.DialogResult = DialogResult.OK;
        }
    }
}

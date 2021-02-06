using Mefino.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.GUI.Models
{
    public partial class SetupPage : MetroFramework.Controls.MetroUserControl
    {
        public static SetupPage Instance;

        public SetupPage()
        {
            Instance = this;

            InitializeComponent();

            _otwPathInputField.Text = Folders.OUTWARD_FOLDER;

            if (!Folders.IsCurrentOutwardPathValid(out InstallState state))
                this._bepPanel.Visible = false;

            SetOtwPathResult(state);
        }

        internal void SetOtwPathResult(InstallState state)
        {
            switch (state)
            {
                // this value is used to indicate its il2cpp in this context.
                case InstallState.Outdated:
                    this._otwPathStatus.Text = $"IL2CPP install detected!";
                    this._otwPathStatus.ForeColor = Color.IndianRed;

                    var result = MessageBox.Show($"This is an IL2CPP (main branch) install! Do you want to open the guide for swapping to Mono?", 
                        "IL2CPP Install detected", 
                        MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        Process.Start($"https://outward.gamepedia.com/Installing_Mods#Modding_Branch");
                    }

                    break;

                case InstallState.NotInstalled:
                    this._otwPathStatus.Text = $"Not found / invalid path";
                    this._otwPathStatus.ForeColor = Color.DarkKhaki;
                    break;

                case InstallState.Installed:
                    this._otwPathStatus.Text = $"Found Mono install";
                    this._otwPathStatus.ForeColor = Color.LightGreen;

                    RefreshBepInExPanel();

                    break;
            }
        }

        internal void RefreshBepInExPanel()
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                this._bepPanel.Visible = false;
                return;
            }

            this._bepPanel.Visible = true;

            if (Web.BepInExHandler.IsBepInExUpdated())
            {
                this._bepStatus.Text = "Up to date";
                this._bepStatus.ForeColor = Color.LightGreen;
                this._bepInstallButton.Visible = false;

                MefinoGUI.InitFeatures();
            }
            else
            {
                switch (Web.BepInExHandler.s_lastInstallStateResult)
                {
                    case InstallState.Outdated:
                        this._bepStatus.Text = $"Outdated ({Web.BepInExHandler.s_latestBepInExVersion} available)";
                        this._bepStatus.ForeColor = Color.DarkKhaki;
                        this._bepInstallButton.Visible = true;
                        this._bepInstallButton.Text = "Update";
                        break;

                    case InstallState.NotInstalled:
                        this._bepStatus.Text = "Not installed";
                        this._bepStatus.ForeColor = Color.IndianRed;
                        this._bepInstallButton.Visible = true;
                        this._bepInstallButton.Text = "Install";
                        break;
                }
            }
        }

        private void _otwPathSelectBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Outward Game (*.exe)|Outward.exe";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = Path.GetDirectoryName(openFileDialog.FileName);

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        this._otwPathInputField.Text = filePath;
                        Folders.SetOutwardFolderPath(filePath, out InstallState state);

                        SetOtwPathResult(state);
                    }
                }
            }
        }

        private void _bepInstallButton_Click(object sender, EventArgs e)
        {
            this._bepInstallButton.Enabled = false;

            MefinoGUI.SetProgressBarActive(true);

            Web.BepInExHandler.UpdateBepInEx();

            MefinoGUI.SetProgressBarActive(false);

            this._bepInstallButton.Enabled = true;
            RefreshBepInExPanel();
        }
    }
}

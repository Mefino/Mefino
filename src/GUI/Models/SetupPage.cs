﻿using Mefino.Core;
using Mefino.Core.Profiles;
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
using Mefino.Core.Web;

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
                // 'Outdated' is used to indicate its il2cpp in this context.
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

                    this._uninstallButton.Visible = false;
                    MefinoGUI.DisableFeaturePages();
                    break;

                case InstallState.NotInstalled:
                    this._otwPathStatus.Text = $"Not found / invalid path";
                    this._otwPathStatus.ForeColor = Color.DarkKhaki;
                    this._uninstallButton.Visible = false;
                    MefinoGUI.DisableFeaturePages();
                    break;

                case InstallState.Installed:
                    this._otwPathStatus.Text = $"Found Mono install";
                    this._otwPathStatus.ForeColor = Color.LightGreen;

                    Application.DoEvents();

                    RefreshBepInExPanel();

                    break;
            }

        }

        internal void RefreshBepInExPanel()
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                this._bepPanel.Visible = false;
                this._uninstallButton.Visible = false;
                MefinoGUI.DisableFeaturePages();
                return;
            }

            this._bepPanel.Visible = true;

            if (BepInExHandler.IsBepInExUpdated())
            {
                this._bepStatus.Text = "Installed";
                this._bepStatus.ForeColor = Color.LightGreen;
                this._bepInstallButton.Visible = false;

                LocalPackageManager.RefreshInstalledPackages();

                MefinoGUI.EnabledFeaturePages();
                Folders.CheckOutwardMefinoInstall();

                this._uninstallButton.Visible = true;
            }
            else
            {
                this._uninstallButton.Visible = false;
                MefinoGUI.DisableFeaturePages();

                switch (BepInExHandler.s_lastInstallStateResult)
                {
                    case InstallState.Outdated:
                        this._bepStatus.Text = $"Outdated";
                        this._bepStatus.ForeColor = Color.DarkKhaki;
                        this._bepInstallButton.Visible = true;
                        this._bepInstallButton.Text = $"Update to {BepInExHandler.s_latestBepInExVersion}";
                        break;

                    case InstallState.NotInstalled:
                        this._bepStatus.Text = "Not installed";
                        this._bepStatus.ForeColor = Color.IndianRed;
                        this._bepInstallButton.Visible = true;
                        this._bepInstallButton.Text = $"Install BepInEx {BepInExHandler.s_latestBepInExVersion}";
                        break;
                }
            }
        }

        private void _otwPathSelectBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Outward Game (Outward.exe)|Outward.exe";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = Path.GetDirectoryName(openFileDialog.FileName);

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        this._otwPathInputField.Text = filePath;
                        Folders.SetOutwardFolderPath(filePath, out InstallState state);

                        SetOtwPathResult(state);

                        AppDataManager.SaveConfig();
                    }
                }
            }
        }

        private void _bepInstallButton_Click(object sender, EventArgs e)
        {
            this._bepInstallButton.Enabled = false;
            this._uninstallButton.Enabled = false;
            MefinoGUI.SetProgressBarActive(true);

            BepInExHandler.UpdateBepInEx();

            // update GUI before moving on
            this._bepInstallButton.Enabled = true;
            this._uninstallButton.Enabled = true;
            MefinoGUI.SetProgressBarActive(false);

            RefreshBepInExPanel();

            Application.DoEvents();

            if (BepInExHandler.s_lastInstallStateResult != InstallState.Installed)
            {
                MessageBox.Show($"Failed to update BepInEx! Make sure Outward isn't running and you're online.", "Warning", MessageBoxButtons.OK);
            }
            else
            {
                ProfileManager.LoadProfiles(false);
            }
        }

        private void _uninstallButton_Click(object sender, EventArgs e)
        {
            ProfileManager.SavePrompt(false);

            var result = OutwardHelper.UninstallFromOutward();

            if (result == DialogResult.Cancel)
                return;

            Folders.IsCurrentOutwardPathValid(out InstallState state);
            SetOtwPathResult(state);

            LocalPackageManager.RefreshInstalledPackages();
            LauncherPage.Instance.RebuildPackageList();
        }

        private void _appDataFolderButton_Click(object sender, EventArgs e)
        {
            var dir = Folders.MEFINO_APPDATA_FOLDER;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            Process.Start(dir);
        }
    }
}

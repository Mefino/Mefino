using Mefino.Core;
using Mefino.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.GUI
{
    public partial class Bootloader : MetroFramework.Forms.MetroForm
    {
        public static Bootloader Instance;

        internal static InstallState s_bootloaderCloseResult;

        internal static MetroFramework.Controls.MetroProgressBar BepProgressBar => Instance?._bepProgressBar;

        public Bootloader()
        {
            Instance = this;

            InitializeComponent();

            this.Text = $"Mefino {(global::Mefino.Mefino.VERSION)}";

            this._bepProgressBar.Visible = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Instance = null;
        }


        private void Bootloader_Load(object sender, EventArgs e)
        {
            UpdateOutwardPathState();
            UpdateBepInExState();
        }

        internal void UpdateOutwardPathState()
        {
            if (global::Mefino.Mefino.IsCurrentOutwardPathValid())
            {
                this._otwPathInput.Text = global::Mefino.Mefino.OUTWARD_FOLDER;
                this._otwPathLabelBottom.Text = "Outward Mono found.";
                this._otwPathLabelBottom.ForeColor = Color.Green;

                this._bepInstallBtn.Visible = true;
                this._bepStatusLabel.Visible = true;
                this._bepStatusTitle.Visible = true;
            }
            else
            {
                this._otwPathInput.Text = global::Mefino.Mefino.OUTWARD_FOLDER;
                this._otwPathLabelBottom.Text = "No valid path set...";
                this._otwPathLabelBottom.ForeColor = Color.Orange;

                this._bepInstallBtn.Visible = false;
                this._bepStatusLabel.Visible = false;
                this._bepStatusTitle.Visible = false;
            }
        }

        internal void UpdateBepInExState()
        {
            if (BepInExHandler.IsBepInExUpdated())
            {
                s_bootloaderCloseResult = InstallState.Installed;

                BeginInvoke(new MethodInvoker(Close));
            }
            else
            {
                if (BepInExHandler.s_lastInstallStateResult == InstallState.Outdated)
                {
                    this._bepStatusLabel.Text = "Outdated (new: " + BepInExHandler.s_latestBepInExVersion + ")";
                    this._bepInstallBtn.Text = "Update";
                    this._bepStatusLabel.ForeColor = Color.Yellow;
                }
                else
                {
                    this._bepStatusLabel.Text = "Not installed";
                    this._bepInstallBtn.Text = "Install";
                    this._bepStatusLabel.ForeColor = Color.Orange;
                }
            }
        }

        private void SelectOutwardButton_Click(object sender, EventArgs e)
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
                        this._otwPathInput.Text = filePath;
                        global::Mefino.Mefino.SetOutwardFolderPath(filePath);
                        UpdateOutwardPathState();
                        UpdateBepInExState();
                    }
                }
            }
        }

        private void _bepInstallBtn_Click(object sender, EventArgs e)
        {

            this._bepProgressBar.Visible = true;

            BepInExHandler.CheckAndUpdateBepInEx();

            this._bepProgressBar.Visible = false;

            UpdateBepInExState();
        }
    }
}

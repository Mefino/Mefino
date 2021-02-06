using Mefino.GUI.Models;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino.GUI
{
    public partial class MefinoGUI : MetroFramework.Forms.MetroForm
    {
        public static MefinoGUI Instance;

        internal static MetroTabPage[] s_featureTabPages;

        public MefinoGUI()
        {
            Instance = this;

            InitializeComponent();

            SetProgressBarActive(false);

            s_featureTabPages = new MetroTabPage[]
            {
                Instance._installedTabPage,
                Instance._browseTabPage,
                Instance._profileTabPage
            };

            this.Text = $"Mefino {MefinoApp.VERSION}";
            this._versionLabel.Text = $"v{MefinoApp.VERSION}";

            // if setup is all good, init features, otherwise SetupPage must do it.
            if (Folders.IsCurrentOutwardPathValid() && Web.BepInExHandler.IsBepInExUpdated())
            {
                InitFeatures();
            }
            else
            {
                foreach (var tab in s_featureTabPages)
                    Instance._tabView.DisableTab(tab);
            }

            // create setup page control
            this._setupTabPage.Controls.Add(new SetupPage());
        }

        private static bool s_doneInitFeatures;

        public static void InitFeatures()
        {
            foreach (var tab in s_featureTabPages)
                Instance._tabView.EnableTab(tab);

            if (s_doneInitFeatures)
                return;

            s_doneInitFeatures = true;

            // add control modules for other pages
            Instance._installedTabPage.Controls.Add(new ManagerPage());
            Instance._browseTabPage.Controls.Add(new BrowsePage());
            Instance._profileTabPage.Controls.Add(new ProfilePage());

            // go to first feature page
            Instance._tabView.SelectedIndex = 1;

            // Refresh 
            Application.DoEvents();

            // refresh web manifests after the menu inits (just looks smoother in this order)
            Core.WebManifestManager.UpdateWebManifests();
        }

        public static void SetProgressBarActive(bool active)
        {
            Instance._progressBar.Visible = active;
            Instance._progressText.Visible = active;
        }

        public static void SetProgressPercent(int value)
        {
            if (Instance == null)
                return;

            Instance._progressBar.Value = value;
        }

        public static void SetProgressMessage(string message)
        {
            if (Instance == null)
                return;

            Instance._progressText.Text = message;
        }
    }
}

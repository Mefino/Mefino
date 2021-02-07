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
                Instance._manageTabPage,
                Instance._browseTabPage,
            };

            this.Text = $"Mefino {MefinoApp.VERSION}";
            this._versionLabel.Text = $"v{MefinoApp.VERSION}";

            var bepinex = Web.BepInExHandler.IsBepInExUpdated();

            // create setup page control
            this._setupTabPage.Controls.Add(new SetupPage());

            // if setup is all good, init features, otherwise SetupPage must do it.
            if (Folders.IsCurrentOutwardPathValid() && bepinex)
                EnabledFeaturePages();
            else
                DisableFeaturePages();
        }

        public static void DisableFeaturePages()
        {
            if (Instance == null)
                return;

            foreach (var tab in s_featureTabPages)
                Instance._tabView.DisableTab(tab); 
            
            Instance._tabView.SelectedIndex = 0;
        }

        private static bool s_doneInitFeatures;

        public static void EnabledFeaturePages()
        {
            if (Instance == null)
                return;

            foreach (var tab in s_featureTabPages)
                Instance._tabView.EnableTab(tab);

            // go to launch page by default
            Instance._tabView.SelectedIndex = 2;

            if (s_doneInitFeatures)
                return;

            s_doneInitFeatures = true;

            // add control modules for other pages
            Instance._manageTabPage.Controls.Add(new LauncherPage());
            Instance._browseTabPage.Controls.Add(new BrowseModsPage());

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

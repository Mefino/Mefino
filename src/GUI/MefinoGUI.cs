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

        public static MetroTabPage[] FeatureTabPages;

        public MefinoGUI()
        {
            Instance = this;

            InitializeComponent();

            FeatureTabPages = new MetroTabPage[]
            {
                Instance._installedTabPage,
                Instance._browseTabPage,
                Instance._profileTabPage
            };

            this.Text = $"Mefino {Mefino.VERSION}";
            this._versionLabel.Text = $"v{Mefino.VERSION}";

            bool bepinexUpdated = Web.BepInExHandler.IsBepInExUpdated();

            // create setup page control
            this._setupTabPage.Controls.Add(new SetupPage());

            // if setup is all good, init features, otherwise SetupPage must do it.
            if (Folders.IsCurrentOutwardPathValid() && bepinexUpdated)
            {
                InitFeatures();
            }
            else
            {
                foreach (var tab in FeatureTabPages)
                    Instance._tabView.DisableTab(tab);
            }
        }

        public static void InitFeatures()
        {
            foreach (var tab in FeatureTabPages)
                Instance._tabView.EnableTab(tab);

            Core.WebManifestManager.UpdateWebManifests();

            // todo add control modules for other pages once made


            // go to first feature page
            Instance._tabView.SelectedIndex = 1;
        }
    }
}

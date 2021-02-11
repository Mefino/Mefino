using Mefino.Core;
using Mefino.Core.Profiles;
using Mefino.GUI.Models;
using Mefino.Core.IO;
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

        internal static readonly List<Control> SensitiveControls = new List<Control>();

        internal static MetroTabPage[] s_featureTabPages;

        public MefinoGUI()
        {
            Instance = this;

            this.FormClosed += MefinoGUI_FormClosed;

            InitializeComponent();
        }

        public static void SetLoadingSplashVisible(bool visible)
        {
            if (Instance == null)
                return;

            Instance.Invoke(new MethodInvoker(() => 
            {
                Instance._loadingSplash.Visible = visible;
                Instance._tabView.Enabled = !visible;
            }));
            Application.DoEvents();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            SetProgressBarActive(false);
            SetLoadingSplashVisible(false);

            s_featureTabPages = new MetroTabPage[]
            {
                Instance._browseTabPage,
                Instance._manageTabPage,
            };

            this.Text = $"Mefino {MefinoApp.VERSION}";
            this._versionLabel.Text = $"v{MefinoApp.VERSION}";

            bool bepUpdated = Core.Web.BepInExHandler.IsBepInExUpdated();

            // create setup page control
            this._setupTabPage.Controls.Add(new SetupPage());

            // if setup is all good, init features, otherwise SetupPage must do it.
            if (Folders.IsCurrentOutwardPathValid() && bepUpdated)
            {
                EnabledFeaturePages();
            }
            else
            {
                DisableFeaturePages();
                Instance._tabView.SelectedIndex = 0;
            }
        }

        private void MefinoGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            TemporaryFile.CleanupAllFiles();

            AppDataManager.SaveConfig();
            ProfileManager.SavePrompt();
        }

        private static bool[] s_lastEnabledSensitiveStates;

        public static void DisableSensitiveControls()
        {
            try
            {
                for (int i = 0; i < Instance._tabView.TabPages.Count; i++)
                {
                    if (i == Instance._tabView.SelectedIndex)
                        continue;

                    var page = (MetroTabPage)Instance._tabView.TabPages[i];
                    Instance.Invoke(new MethodInvoker(() => { Instance._tabView.DisableTab(page); }));
                }

                s_lastEnabledSensitiveStates = new bool[SensitiveControls.Count];
                for (int i = 0; i < SensitiveControls.Count; i++)
                {
                    s_lastEnabledSensitiveStates[i] = SensitiveControls[i].Enabled;
                    SensitiveControls[i].Invoke(new MethodInvoker(() => { SensitiveControls[i].Enabled = false; }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception on DisableSensitiveControls: " + ex);
            }
        }

        public static void ReEnableSensitiveControls()
        {
            try
            {
                int origSelected = Instance._tabView.SelectedIndex;
                for (int i = 0; i < Instance._tabView.TabPages.Count; i++)
                {
                    var page = (MetroTabPage)Instance._tabView.TabPages[i];
                    Instance.Invoke(new MethodInvoker(() => { Instance._tabView.EnableTab(page); }));
                }
                Instance.Invoke(new MethodInvoker(() => { Instance._tabView.SelectedIndex = origSelected; }));

                for (int i = 0; i < SensitiveControls.Count; i++)
                {
                    SensitiveControls[i].Invoke(new MethodInvoker(() => { SensitiveControls[i].Enabled = s_lastEnabledSensitiveStates[i]; }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception on ReEnableSensitiveControls: " + ex);
            }
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

            SetLoadingSplashVisible(true);

            foreach (var tab in s_featureTabPages)
                Instance._tabView.EnableTab(tab);

            Instance._tabView.SelectedIndex = 2;

            if (ProfileManager.ActiveProfile == null)
                ProfileManager.LoadProfileOrSetDefault();

            if (!s_doneInitFeatures)
            {
                s_doneInitFeatures = true;

                // add control modules for other pages
                Instance._browseTabPage.Controls.Add(new BrowseModsPage());
                Instance._manageTabPage.Controls.Add(new LauncherPage());
            }

            Application.DoEvents();

            SetLoadingSplashVisible(false);
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

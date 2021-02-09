using Mefino.Core;
using Mefino.Core.IO;
using Mefino.Core.Web;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mefino.GUI;
using Mefino.CLI;

namespace Mefino
{
    /// <summary>
    /// Mefino's current context. If there were command line arguments passed to Mefino it will run them in CLI mode and close, otherwise it will default to GUI.
    /// </summary>
    public enum MefinoContext
    {
        CLI,
        GUI
    }

    public class MefinoApp
    {
        public const string VERSION = "0.2.1.0";

        public static MefinoContext CurrentContext;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            try
            {
                // If the user passed any arguments, run them in CLI mode.
                if (args.Any())
                {
                    CurrentContext = MefinoContext.CLI;
                    CoreInit();

                    CLIHandler.Execute(args);
                }
                else // Otherwise launch the GUI.
                {
#if RELEASE
                    CLIHandler.HideConsole();
#endif
                    CurrentContext = MefinoContext.GUI;
                    CoreInit();

                    FolderWatcher.Init();

                    if (SelfUpdater.CheckUpdatedWanted())
                        return;

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(new MefinoGUI());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fatal unhandled exception in Mefino.exe:" +
                    $"\n\n" +
                    $"{ex}",
                    "Error!",
                    MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Core initialization that must happen for Mefino's base features to operate.
        /// </summary>
        internal static void CoreInit()
        {
            AppDataManager.LoadConfig();

            WebClientManager.Initialize();

            // Local refresh (doesn't download new manifests)
            RefreshAllPackages();
        }

        /// <summary>
        /// Refresh all packages, online and local. Optionally re-download new manifests as well, if <paramref name="refreshOnline"/> is <see langword="true"/>
        /// </summary>
        public static void RefreshAllPackages(bool refreshOnline = false)
        {
            if (refreshOnline)
                WebManifestManager.UpdateWebManifests();
            else
                WebManifestManager.LoadWebManifestCache();

            // refresh installed packages
            LocalPackageManager.RefreshInstalledPackages();
        }

        /// <summary>
        /// Send generic Async progress percentage for MefinoGUI's progress bar.
        /// </summary>
        internal static void SendAsyncProgress(int progress)
        {
            MefinoGUI.SetProgressPercent(progress);
            // let UI actually refresh for the progress.
            Application.DoEvents();
        }
    }
}

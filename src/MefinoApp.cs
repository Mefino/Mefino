using Mefino.CLI;
using Mefino.Core;
using Mefino.Core.Web;
using Mefino.GUI;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
        public const string VERSION = "0.2.8.0";

        // Use a Mutex to limit the number of app instances to 1.
        internal static Mutex appMutex = new Mutex(true, MUTEX_STRING);
        internal const string MUTEX_STRING = "{42FA6537-1C93-48EF-88CB-0B6ADF1220A2}"; // our project GUID

        public static MefinoContext CurrentContext;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            try
            {
                if (appMutex.WaitOne(TimeSpan.Zero, true))
                {
                    // Application was able to enter (no instance running).
                    ProcessInitialize(args);
                }
                else
                {
                    // Application is already running

                    // If arguments were passed, we need to send them to the running instance.
                    // My solution is: save them to disk, then post a message to the other app to read them.
                    if (args.Any())
                    {
                        SetExternalArguments(args);
                        NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_READEXTARGS, IntPtr.Zero, IntPtr.Zero);
                    }

                    // Bring the running instance window to front.
                    NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
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

        internal static void ProcessInitialize(params string[] args)
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
                // Show a console if DEBUG build.
                CLIHandler.HideConsole();
#endif
                CurrentContext = MefinoContext.GUI;
                CoreInit();

                if (SelfUpdater.CheckUpdatedWanted())
                    return;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new MefinoGUI());
            }
        }

        private static string ExternalArgumentPath => $@"{Folders.MEFINO_APPDATA_FOLDER}\tempargs.txt";

        internal static void SetExternalArguments(string[] args)
        {
            string argsToString = "";
            foreach (var arg in args)
                argsToString += arg + "\n";

            if (File.Exists(ExternalArgumentPath))
                File.Delete(ExternalArgumentPath);

            File.WriteAllText(ExternalArgumentPath, argsToString);
        }

        internal static void LoadExternalArguments()
        {
            if (!File.Exists(ExternalArgumentPath))
            {
                Console.WriteLine("Received 'LoadExternalArguments' message but no args found!");
                return;
            }

            string[] args = File.ReadAllLines(ExternalArgumentPath);

            // This will be used by, for example, the in-game plugin sending a message to Mefino to install a profile or something.

            // These will probably go through the CLI handler.

            // All CLI arguments should check if context is CLI or GUI before executing?

            // ...

            // Delete the tempargs.txt file after reading it.
            if (File.Exists(ExternalArgumentPath))
                File.Delete(ExternalArgumentPath);
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
        /// Send generic Async progress percentage.
        /// </summary>
        internal static void SendAsyncProgress(int progress)
        {
            MefinoGUI.SetProgressPercent(progress);
            // let UI actually refresh for the progress.
            Application.DoEvents();
        }
    }
}

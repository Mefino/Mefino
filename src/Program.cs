using Mefino.Core;
using Mefino.IO;
using Mefino.Web;
using System;
using System.Net;
using System.Windows.Forms;
using Mefino.CLI;
using System.Threading;
using Mefino.GUI;

namespace Mefino
{
    internal class Program
    {
        internal enum MefinoContext
        {
            CLI,
            GUI
        }

        internal static MefinoContext s_context = MefinoContext.CLI;
        
        [STAThread]
        static void Main(string[] args)
        {
#if DEBUG
#else
            Mefino.CheckSelfUpdate();
#endif

            try
            {
                WebClientManager.Initialize();

                Mefino.LoadConfig();

                ManifestManager.LoadManifestCache();
                MefinoPackageManager.RefreshInstalledMods();

                if (args == null || args.Length < 1 || string.IsNullOrEmpty(args[0]))
                {
#if DEBUG
#else
                    CLIManager.HideConsole();
#endif

                    s_context = MefinoContext.GUI;

                    Application.Run(new GUI.Bootloader());

                    if (Bootloader.s_bootloaderCloseResult == InstallState.Installed)
                    {
                        Application.Run(new GUI.Mefino());
                    }
                }
                else
                {
                    s_context = MefinoContext.CLI;
                    CLIManager.Execute(args);
                }

            }
            catch (Exception ex)
            {
                TemporaryFile.CleanupAllFiles();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fatal unhandled exception in Mefino!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex);
                Console.WriteLine("");
                Console.WriteLine("Press any button to exit.");
                Console.ReadLine();
            }
        }
    }
}

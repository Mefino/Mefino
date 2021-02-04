using Mefino.Loader.Core;
using Mefino.Loader.IO;
using Mefino.Loader.Web;
using System;
using System.Net;
using System.Windows.Forms;
using Mefino.Loader.CLI;

namespace Mefino.Loader
{
    internal class Program
    {
        internal enum MefinoContext
        {
            CLI,
            GUI
        }

        internal static MefinoContext s_context = MefinoContext.CLI;

        static void Main(string[] args)
        {
            try
            {
                WebClientManager.Initialize();

                MefinoLoader.LoadConfig();

                ManifestManager.LoadManifestCache();
                MefinoPackageManager.RefreshInstalledMods();

                if (args == null || args.Length < 1 || string.IsNullOrEmpty(args[0]))
                {
                    s_context = MefinoContext.GUI;
                    CLIManager.HideConsole();
                    Application.Run(new GUI.Bootloader());
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

using Mefino.CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mefino
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            try
            {
                Mefino.CoreInit();

                if (args.Any())
                {
                    CLIHandler.Execute(args);
                }
                else
                {
#if RELEASE
                    CLIHandler.HideConsole();
#endif

                    if (Mefino.CheckUpdatedWanted())
                        return;

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new GUI.MefinoGUI());
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
    }
}

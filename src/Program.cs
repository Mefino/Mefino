using Mefino.Loader.IO;
using Mefino.Loader.Web;
using System;
using System.Net;

namespace Mefino.Loader
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                WebClientManager.Initialize();

                Console.WriteLine("Mefino " + MefinoLoader.VERSION + " starting...");
                Console.WriteLine("");

                MefinoLoader.Execute(args);
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

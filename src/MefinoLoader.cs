using Mefino.Loader.Manifests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Loader
{
    public static class MefinoLoader
    {
        public const string VERSION = "0.1.0.0";

        // todo support this properly
        public static string OutwardFolderPath => @"E:\Steam\steamapps\common\Outward";

        public static void Execute(params string[] args)
        {
            Console.WriteLine("");

            if (args == null || args.Length < 1 || string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Please enter a command:");
                ListCommands();

                Execute(Console.ReadLine().Split(' '));
            }
            else
            {
                if (s_commandDict.TryGetValue(args[0], out Action<string[]> action))
                {
                    string[] subArgs = null;
                    if (args.Length > 1)
                    {
                        subArgs = new string[args.Length - 1];
                        for (int i = 1; i < args.Length; i++)
                            subArgs[i - 1] = args[i];
                    }
                    action.Invoke(subArgs);
                }
                else
                    InvalidCommand(args[0]);
            }
        }

        internal static readonly Dictionary<string, Action<string[]>> s_commandDict = new Dictionary<string, Action<string[]>>
        {
            { "-quit",           Cmd_Quit },
            { "-bepinex",        Cmd_BepInEx },
            { "-list",           Cmd_RefreshModList },
            { "-install",        Cmd_Install },
            { "-uninstall",      Cmd_Uninstall },
            { "-uninstallall",   Cmd_UninstallAll }
        };

        internal static void ListCommands()
        {
            Console.WriteLine("");
            Console.WriteLine(" -quit : Quit the application");
            Console.WriteLine(" -bepinex : Check that BepInEx is installed and updated");
            Console.WriteLine(" -list : Refresh mod lists (GitHub and installed mods)");
            Console.WriteLine(" -install [author].[repo] : Install the mod from given author/repo, eg '-install sinai-dev.SideLoader'");
            Console.WriteLine(" -uninstall [author].[repo] : Uninstall the mod from given author/repo, eg '-uninstall sinai-dev.SideLoader'");
            Console.WriteLine(" -uninstallall : Uninstalls all mods (which are supported by a manifest)");
            Console.WriteLine("");
        }

        internal static void InvalidCommand(string input)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Invalid command '" + input + "'");
            Console.ForegroundColor = ConsoleColor.White;
            Execute();
        }

        internal static void Cmd_Quit(params string[] args)
        {
            // do nothing and quit
        }

        internal static void Cmd_BepInEx(params string[] args)
        {
            Console.WriteLine("TODO");

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_RefreshModList(params string[] args)
        {
            ManifestManager.RefreshModList();

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_Install(params string[] args)
        {
            if (!ManifestManager.s_cachedWebManifests.Any())
                ManifestManager.RefreshModList(true);

            foreach (var guid in args)
            {
                if (ManifestManager.s_cachedWebManifests.TryGetValue(guid, out PackageManifest manifest))
                    ManifestManager.TryInstallPackage(manifest);
                else
                    Console.WriteLine($"Could not find package by name '{guid}', maybe need to refresh the list?");
            }

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_Uninstall(params string[] args)
        {
            Console.WriteLine("TODO");

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_UninstallAll(params string[] args)
        {
            Console.WriteLine("TODO");

            // Return to arg input..
            Execute();
        }
    }
}

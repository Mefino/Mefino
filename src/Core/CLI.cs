﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Mefino.Loader.Core
{
    public static class CLI
    {
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public static void ShowConsole()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_SHOW);
        }

        public static void HideConsole()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // ========================================================= //

        public static void Execute(params string[] args)
        {
            Console.WriteLine("");

            if (args == null || args.Length < 1 || string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Please enter a command:");
                //ListCommands();

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
            { "-help",          Cmd_Help },
            { "-quit",          Cmd_Quit },
            { "-bepinex",       Cmd_BepInEx },
            { "-outward",       Cmd_SetOutwardPath },
            { "-list",          Cmd_RefreshModList },
            { "-install",       Cmd_Install },
            { "-uninstall",     Cmd_Uninstall },
            { "-uninstallall",  Cmd_UninstallAll },
            { "-enable",        Cmd_Enable },
            { "-disable",       Cmd_Disable },
        };

        internal static void ListCommands()
        {
            Console.WriteLine("");
            Console.WriteLine(" -help : List available commands");
            Console.WriteLine(" -quit : Quit the application");
            Console.WriteLine(" -outward [path] : Set your Outward installation path, eg. '-outward C:\\Program Files\\Outward'");
            Console.WriteLine(" -bepinex : Check that BepInEx is installed and updated");
            Console.WriteLine(" -list : Refresh mod lists (GitHub and installed mods)");
            Console.WriteLine(" -install [author].[repo] : Install the mod from given author/repo, eg '-install sinai-dev.SideLoader', or enable if it is disabled.");
            Console.WriteLine(" -uninstall [author].[repo] : Uninstall the mod from given author/repo, eg '-uninstall sinai-dev.SideLoader'");
            Console.WriteLine(" -uninstallall : Uninstalls all mods (which are supported by a manifest)");
            Console.WriteLine(" -enable [author].[repo] : Try to enable the specified package");
            Console.WriteLine(" -disable [author].[repo] : Try to disable the specified package");
            Console.WriteLine("");
        }

        internal static void InvalidCommand(string input)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Invalid command '" + input + "'");
            Console.ForegroundColor = ConsoleColor.White;
            ListCommands();
            Execute();
        }

        internal static void Cmd_Help(params string[] args)
        {
            ListCommands();
            Execute();
        }

        internal static void Cmd_Quit(params string[] args)
        {
            // do nothing and quit
        }

        internal static void Cmd_SetOutwardPath(params string[] args)
        {
            if (args == null || args.Length < 1 || string.IsNullOrEmpty(args[0]))
                Console.WriteLine("Invalid outward path!");
            else
            {
                if (MefinoLoader.SetOutwardFolderPath(args[0]))
                    Console.WriteLine($"Set Outward folder to '{MefinoLoader.OUTWARD_FOLDER}'");
                else
                    Console.WriteLine($"Invalid Outward path '{args[0]}'");
            }

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_BepInEx(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                Execute();
                return;
            }

            BepInExHandler.CheckAndUpdateBepInEx();

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_RefreshModList(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                Execute();
                return;
            }

            MefinoPackageManager.RefreshInstalledMods();

            ManifestManager.RefreshManifestCache();

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_Install(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                Execute();
                return;
            }

            foreach (var guid in args)
            {
                if (ManifestManager.s_cachedWebManifests.ContainsKey(guid))
                    MefinoPackageManager.TryInstallPackage(guid);
                else
                    Console.WriteLine($"Could not find package by name '{guid}', maybe need to refresh the list?");
            }

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_Uninstall(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                Execute();
                return;
            }

            foreach (var guid in args)
            {
                MefinoPackageManager.TryDeleteDirectory(guid);
            }

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_UninstallAll(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                Execute();
                return;
            }

            for (int i = MefinoPackageManager.s_installedManifests.Count - 1; i >= 0; i--)
            {
                var pkg = MefinoPackageManager.s_installedManifests.ElementAt(i).Value;

                MefinoPackageManager.TryRemovePackage(pkg.GUID);
            }

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_Enable(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                Execute();
                return;
            }

            foreach (var guid in args)
            {
                MefinoPackageManager.TryEnablePackage(guid);
            }

            // Return to arg input..
            Execute();
        }

        internal static void Cmd_Disable(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                Execute();
                return;
            }

            foreach (var guid in args)
            {
                MefinoPackageManager.TryDisablePackage(guid);
            }

            // Return to arg input..
            Execute();
        }
    }
}
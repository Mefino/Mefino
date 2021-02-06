using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Mefino.Core;
using Mefino.Web;

namespace Mefino.CLI
{
    public static class CLIHandler
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
            //var joined = string.Join(" ", args);
            //var parsed = CreateArgs(joined);

            foreach (var entry in s_commands)
            {
                if (entry.IsMatch(args[0]))
                {
                    string[] subArgs = null;
                    if (args.Length > 1)
                    {
                        subArgs = new string[args.Length - 1];
                        for (int i = 1; i < args.Length; i++)
                            subArgs[i - 1] = args[i];
                    }

                    entry.Invoke(subArgs);

                    return;
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Invalid input! The following commands are available:");
            Console.ForegroundColor = ConsoleColor.White;
            ListCommands();
        }

        internal static readonly HashSet<ConsoleCommand> s_commands = new HashSet<ConsoleCommand>
        {
            new ConsoleCommand("help",
                "h",
                "List all supported commands",
                Cmd_Help),

            new ConsoleCommand("bepinex",
                "bie",
                "Check that BepInEx is up to date, and update it if not.",
                Cmd_BepInEx),

            new ConsoleCommand("outward",
                "otw",
                @"Set the saved Outward path to the specified string, eg. 'otw ""C:\Program Files (x86)\..\Outward""'",
                Cmd_SetOutwardPath),

            new ConsoleCommand("refresh",
                "r",
                "Refresh installed mods and the manifests from GitHub",
                Cmd_RefreshModList),

            new ConsoleCommand("install",
                "i",
                "Install and/or enable the specific package GUID, eg. 'install sinai-dev.Outward-SideLoader'",
                Cmd_Install),

            new ConsoleCommand("enable",
                "e",
                "Enable the specific package GUID without checking for updates, if it is installed and disabled. eg. 'enable sinai-dev.Outward-SideLoader'",
                Cmd_Enable),

            new ConsoleCommand("disable",
                "d",
                "Disable the specific package GUID, if it is installed and enabled. eg. 'disable sinai-dev.Outward-SideLoader'",
                Cmd_Disable),

            new ConsoleCommand("disableall",
                "da",
                "Disable all installed packages.",
                Cmd_DisableAll),

            new ConsoleCommand("uninstall",
                "u",
                "Uninstall (delete) the specified package GUID, eg. 'uninstall sinai-dev.Outward-SideLoader'",
                Cmd_Uninstall),

            new ConsoleCommand("uninstallall",
                "",
                "Uninstall (delete) ALL packages",
                Cmd_UninstallAll),
        };

        internal static void ListCommands()
        {
            foreach (var entry in s_commands)
                Console.WriteLine($" - {entry}");
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
        }


        internal static void Cmd_SetOutwardPath(params string[] args)
        {
            if (args == null || args.Length < 1 || string.IsNullOrEmpty(args[0]))
                Console.WriteLine("Invalid outward path!");
            else
            {
                if (!Folders.SetOutwardFolderPath(args[0]))
                    Console.WriteLine($"Invalid Outward path '{args[0]}'");
            }
        }

        internal static void Cmd_BepInEx(params string[] args)
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            BepInExHandler.UpdateBepInExIfNeeded();
        }

        internal static void Cmd_RefreshModList(params string[] args)
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            LocalPackageManager.RefreshInstalledPackages();

            WebManifestManager.UpdateWebManifests();
        }

        internal static void Cmd_Install(params string[] args)
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            foreach (var guid in args)
            {
                if (WebManifestManager.s_cachedWebManifests.ContainsKey(guid))
                    LocalPackageManager.TryInstallWebPackage(guid);
                else
                    Console.WriteLine($"Could not find package by name '{guid}', maybe need to refresh the list?");
            }
        }

        internal static void Cmd_Uninstall(params string[] args)
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            foreach (var guid in args)
            {
                LocalPackageManager.TryDeleteDirectory(guid);
            }
        }

        internal static void Cmd_UninstallAll(params string[] args)
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            for (int i = LocalPackageManager.s_enabledPackages.Count - 1; i >= 0; i--)
            {
                var pkg = LocalPackageManager.s_enabledPackages.ElementAt(i).Value;
                LocalPackageManager.TryRemovePackage(pkg.GUID);
            }
        }

        internal static void Cmd_Enable(params string[] args)
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            foreach (var guid in args)
            {
                LocalPackageManager.TryEnablePackage(guid);
            }
        }

        internal static void Cmd_Disable(params string[] args)
        {
            if (!Folders.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                Execute();
                return;
            }

            foreach (var guid in args)
            {
                LocalPackageManager.TryDisablePackage(guid);
            }
        }

        private static void Cmd_DisableAll(string[] obj)
        {
            if (!LocalPackageManager.s_enabledPackages.Any())
                return;

            LocalPackageManager.TryDisableAllPackages();
        }
    }
}

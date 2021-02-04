using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Mefino.Loader.Core;
using Mefino.Loader.Web;

namespace Mefino.Loader.CLI
{
    public static class CLIManager
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
            var joined = string.Join("", args);
            var parsed = CreateArgs(joined);

            foreach (var entry in s_commands)
            {
                if (entry.IsMatch(parsed[0]))
                {
                    string[] subArgs = null;
                    if (parsed.Length > 1)
                    {
                        subArgs = new string[parsed.Length - 1];
                        for (int i = 1; i < parsed.Length; i++)
                            subArgs[i - 1] = parsed[i];
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
                "Install the specific package GUID, eg. 'install sinai-dev.Outward-SideLoader'", 
                Cmd_Install),

            new ConsoleCommand("uninstall", 
                "u", 
                "Uninstall the specified package GUID, eg. 'uninstall sinai-dev.Outward-SideLoader'", 
                Cmd_Uninstall),

            new ConsoleCommand("uninstallall", 
                "", 
                "Uninstall all packages (not disabling, actually uninstalling).", 
                Cmd_UninstallAll),

            new ConsoleCommand("enable", 
                "e", 
                "Enable the specific package GUID, if it is installed and disabled. eg. 'enable sinai-dev.Outward-SideLoader'", 
                Cmd_Enable),

            new ConsoleCommand("disable", 
                "d",
                "Disable the specific package GUID, if it is installed and enabled. eg. 'disable sinai-dev.Outward-SideLoader'",
                Cmd_Disable),

            new ConsoleCommand("disableall", 
                "da", 
                "Disable all enabled mods.", 
                Cmd_DisableAll),
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
                if (MefinoLoader.SetOutwardFolderPath(args[0]))
                    Console.WriteLine($"Set Outward folder to '{MefinoLoader.OUTWARD_FOLDER}'");
                else
                    Console.WriteLine($"Invalid Outward path '{args[0]}'");
            }
        }

        internal static void Cmd_BepInEx(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            BepInExHandler.CheckAndUpdateBepInEx();
        }

        internal static void Cmd_RefreshModList(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            MefinoPackageManager.RefreshInstalledMods();

            ManifestManager.RefreshManifestCache();
        }

        internal static void Cmd_Install(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            foreach (var guid in args)
            {
                if (ManifestManager.s_cachedWebManifests.ContainsKey(guid))
                    MefinoPackageManager.TryInstallPackage(guid);
                else
                    Console.WriteLine($"Could not find package by name '{guid}', maybe need to refresh the list?");
            }
        }

        internal static void Cmd_Uninstall(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            foreach (var guid in args)
            {
                MefinoPackageManager.TryDeleteDirectory(guid);
            }
        }

        internal static void Cmd_UninstallAll(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            for (int i = MefinoPackageManager.s_installedManifests.Count - 1; i >= 0; i--)
            {
                var pkg = MefinoPackageManager.s_installedManifests.ElementAt(i).Value;
                MefinoPackageManager.TryRemovePackage(pkg.GUID);
            }
        }

        internal static void Cmd_Enable(params string[] args)
        {
            if (!MefinoLoader.IsCurrentOutwardPathValid())
            {
                Console.WriteLine("You need to set the Outward path first!");
                return;
            }

            foreach (var guid in args)
            {
                MefinoPackageManager.TryEnablePackage(guid);
            }
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
        }

        private static void Cmd_DisableAll(string[] obj)
        {
            throw new NotImplementedException();
        }

        // =========== ARGUMENT PARSER HELPER ============

        /// <summary>
        /// C-like argument parser
        /// </summary>
        /// <param name="commandLine">Command line string with arguments. Use Environment.CommandLine</param>
        /// <returns>The args[] array (argv)</returns>
        public static string[] CreateArgs(string commandLine)
        {
            StringBuilder argsBuilder = new StringBuilder(commandLine);
            bool inQuote = false;

            // Convert the spaces to a newline sign so we can split at newline later on
            // Only convert spaces which are outside the boundries of quoted text
            for (int i = 0; i < argsBuilder.Length; i++)
            {
                if (argsBuilder[i].Equals('"'))
                {
                    inQuote = !inQuote;
                }

                if (argsBuilder[i].Equals(' ') && !inQuote)
                {
                    argsBuilder[i] = '\n';
                }
            }

            // Split to args array
            string[] args = argsBuilder.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Clean the '"' signs from the args as needed.
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = ClearQuotes(args[i]);
            }

            return args;
        }

        /// <summary>
        /// Cleans quotes from the arguments.<br/>
        /// All signle quotes (") will be removed.<br/>
        /// Every pair of quotes ("") will transform to a single quote.<br/>
        /// </summary>
        /// <param name="stringWithQuotes">A string with quotes.</param>
        /// <returns>The same string if its without quotes, or a clean string if its with quotes.</returns>
        private static string ClearQuotes(string stringWithQuotes)
        {
            int quoteIndex;
            if ((quoteIndex = stringWithQuotes.IndexOf('"')) == -1)
            {
                // String is without quotes..
                return stringWithQuotes;
            }

            // Linear sb scan is faster than string assignemnt if quote count is 2 or more (=always)
            StringBuilder sb = new StringBuilder(stringWithQuotes);
            for (int i = quoteIndex; i < sb.Length; i++)
            {
                if (sb[i].Equals('"'))
                {
                    // If we are not at the last index and the next one is '"', we need to jump one to preserve one
                    if (i != sb.Length - 1 && sb[i + 1].Equals('"'))
                    {
                        i++;
                    }

                    // We remove and then set index one backwards.
                    // This is because the remove itself is going to shift everything left by 1.
                    sb.Remove(i--, 1);
                }
            }

            return sb.ToString();
        }
    }
}

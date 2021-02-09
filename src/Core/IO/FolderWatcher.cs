using Mefino.Core;
using Mefino.Core.Profiles;
using Mefino.GUI.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace Mefino.Core.IO
{
    /// <summary>
    /// Handles the <see cref="FileSystemWatcher"/>s for Mefino, to automatically update our state on manual user changes.
    /// </summary>
    public static class FolderWatcher
    {
        internal static FileSystemWatcher PluginsFolderWatcher;
        internal static FileSystemWatcher DisabledFolderWatcher;

        public static void Init()
        {
            PluginsFolderWatcher = new FileSystemWatcher(Folders.OUTWARD_PLUGINS);
            PluginsFolderWatcher.Created += OnFileSystemChanged;
            PluginsFolderWatcher.Deleted += OnFileSystemChanged;
            PluginsFolderWatcher.Renamed += OnFileSystemChanged;
            PluginsFolderWatcher.IncludeSubdirectories = true;
            PluginsFolderWatcher.EnableRaisingEvents = true;
            PluginsFolderWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;

            DisabledFolderWatcher = new FileSystemWatcher(Folders.MEFINO_DISABLED_FOLDER);
            DisabledFolderWatcher.Created += OnFileSystemChanged;
            DisabledFolderWatcher.Deleted += OnFileSystemChanged;
            PluginsFolderWatcher.Renamed += OnFileSystemChanged;
            PluginsFolderWatcher.IncludeSubdirectories = true;
            PluginsFolderWatcher.EnableRaisingEvents = true;
            PluginsFolderWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
        }

        private static void OnFileSystemChanged(object sender, FileSystemEventArgs e)
        {
            if (IOHelper.MefinoDoingIO)
                return;

            // Console.WriteLine("Manual folder IO detected");

            LocalPackageManager.RefreshInstalledPackages();

            if (ProfileManager.IsProfileDifferentToEnabledPackages())
                ProfileManager.SetChangesSinceSave(true);

            LauncherPage.Instance?.Invoke(new MethodInvoker(LauncherPage.Instance.RebuildPackageList));
        }
    }
}

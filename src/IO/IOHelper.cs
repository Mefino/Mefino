using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.IO
{
    public static class IOHelper
    {
        /// <summary>
        /// Is Mefino currently doing IO, or has done any in the last 1 second?
        /// </summary>
        public static bool MefinoDoingIO => s_doingIO || ((DateTime.Now - s_lastWriteTime).Seconds < 1);

        /// <summary>
        /// Set Mefino's "doing IO" state, and stamp this time as our last write time (used by <see cref="MefinoDoingIO"/>).
        /// </summary>
        internal static bool SetDoingIO
        {
            set
            {
                s_doingIO = value;
                s_lastWriteTime = DateTime.Now;
            }
        }
        private static bool s_doingIO;
        private static DateTime s_lastWriteTime;

        /// <summary>
        /// Try to create a directory at the given location.
        /// </summary>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static DirectoryInfo CreateDirectory(string directory)
        {
            try
            {
                SetDoingIO = true;
                var ret = Directory.CreateDirectory(directory);
                SetDoingIO = false;
                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                SetDoingIO = false;
                return null;
            }
        }

        /// <summary>
        /// Try to move the directory from one place to another.
        /// </summary>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool TryMoveDirectory(string fromDir, string toDir)
        {
            if (!Directory.Exists(fromDir))
                return false;

            try
            {
                SetDoingIO = true;

                var toDirParent = Path.GetDirectoryName(toDir);
                Directory.CreateDirectory(toDirParent);

                Directory.Move(fromDir, toDir);

                SetDoingIO = false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception moving directory: {ex}");
                SetDoingIO = false;
                return false;
            }
        }

        /// <summary>
        /// Try to delete the directory.
        /// </summary>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool TryDeleteDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Console.WriteLine("dir " + dir + " does not exist, returning true by default...");
                return true;
            }

            try
            {
                SetDoingIO = true;
                Directory.Delete(dir, true);
                SetDoingIO = false;
                return true;
            }
            catch
            {
                SetDoingIO = false;
                return false;
            }
        }
    }
}

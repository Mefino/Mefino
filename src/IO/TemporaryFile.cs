using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.IO
{
    class TemporaryFile
    {
        private static readonly HashSet<string> s_tempFiles = new HashSet<string>();

        internal static string CreateFile()
        {
            var path = Path.GetTempFileName();
            s_tempFiles.Add(path);
            return path;
        }

        internal static void CleanupFile(string tempPath)
        {
            if (string.IsNullOrEmpty(tempPath))
                return;

            if (File.Exists(tempPath))
                File.Delete(tempPath);

            if (s_tempFiles.Contains(tempPath))
                s_tempFiles.Remove(tempPath);
        }

        internal static void CleanupAllFiles()
        {
            if (!s_tempFiles.Any())
                return;

            for (int i = s_tempFiles.Count - 1; i >= 0; i--)
            {
                var file = s_tempFiles.ElementAt(i);

                if (File.Exists(file))
                    File.Delete(file);

                s_tempFiles.Remove(file);
            }
        }
    }
}

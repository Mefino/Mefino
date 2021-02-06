using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mefino.IO
{
    public static class ZipHelper
    {
        // thanks to ML installer for this snippet
        public static bool ExtractZip(string zipFilePath, string dirpath)
        {
            try
            {
                using (var stream = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read))
                {
                    if (stream == null || stream.Length == 0)
                        throw new IOException("The requested zip file was not found or was invalid!");

                    using (var zip = new ZipArchive(stream))
                    {
                        int total_entry_count = zip.Entries.Count;
                        string fullName = Directory.CreateDirectory(dirpath).FullName;

                        for (int i = 0; i < total_entry_count; i++)
                        {
                            ZipArchiveEntry entry = zip.Entries[i];
                            string fullPath = Path.GetFullPath(Path.Combine(fullName, entry.FullName));
                            
                            if (!fullPath.StartsWith(fullName))
                                throw new IOException("Extracting Zip entry would have resulted in a file outside the specified destination directory.");
                            
                            if (Path.GetFileName(fullPath).Length != 0)
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                                entry.ExtractToFile(fullPath, true);
                            }
                            else
                            {
                                if (entry.Length != 0)
                                    throw new IOException("Zip entry name ends in directory separator character but contains data.");

                                Directory.CreateDirectory(fullPath);
                            }

                            int prog = (int)((decimal)i / total_entry_count);
                            MefinoApp.SendAsyncProgress(prog);
                        }
                    }
                }

                TemporaryFile.CleanupFile(zipFilePath);

                //Mefino.SendAsyncCompletion(true);

                return true;
            }
            catch (Exception ex)
            {
                TemporaryFile.CleanupFile(zipFilePath);
                Console.WriteLine("Exception getting zip at '" + zipFilePath + "'");
                Console.WriteLine($"{ex.GetType()}: {ex.Message}");

                //Mefino.SendAsyncCompletion(false);
                return false;
            }
        }
    }
}

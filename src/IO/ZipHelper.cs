using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Loader.IO
{
    public static class ZipHelper
    {
        // thanks to ML installer for this snippet
        public static bool DownloadAndExtractZip(string zipURL, string dirpath)
        {
            var tempFile = TemporaryFile.CreateFile();

            try
            {
                var webClient = Web.WebClientManager.WebClient;
                Web.WebClientManager.Reset();

                webClient.DownloadFileAsync(new Uri(zipURL), tempFile);
                while (webClient.IsBusy) { }

                using (var stream = new FileStream(tempFile, FileMode.Open, FileAccess.Read))
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
                        }
                    }
                }

                TemporaryFile.CleanupFile(tempFile);

                return true;
            }
            catch (Exception ex)
            {
                TemporaryFile.CleanupFile(tempFile);
                Console.WriteLine("Exception getting zip from '" + zipURL + "'");
                Console.WriteLine($"{ex.GetType()}: {ex.Message}");

                return false;
            }
        }
    }
}

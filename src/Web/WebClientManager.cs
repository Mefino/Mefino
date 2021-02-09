using Mefino.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mefino.Web
{
    internal static class WebClientManager
    {
        private static readonly WebClient s_webClient = new WebClient();

        public static int LastDownloadProgress => s_lastDownloadProgressPercent;
        private static int s_lastDownloadProgressPercent;

        public static bool IsBusy => s_webClient?.IsBusy ?? false;

        /// <summary>
        /// Setup the WebClientManager's callbacks.
        /// </summary>
        internal static void Initialize()
        {
            s_webClient.DownloadProgressChanged += OnDownloadProgress;
        }

        private static void OnDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            s_lastDownloadProgressPercent = e.ProgressPercentage;
        }

        /// <summary>
        /// Reset the WebClient, ensuring protocols and headers are set properly.
        /// </summary>
        private static void Reset()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
                | SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12
                | (SecurityProtocolType)3072;

            s_webClient.Headers.Clear();
            s_webClient.Headers.Add("User-Agent", "request");
        }
        
        /// <summary>
        /// Download from the given URL as a string.
        /// </summary>
        public static string DownloadString(string url)
        {
            try
            {
                Reset();
                return s_webClient.DownloadString(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception downloading string from: " + url);
                Console.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// Download from the given URL to the provided file path, which should probably be a temporary file.
        /// </summary>
        public static void DownloadFileAsync(string fileURL, string tempFile)
        {
            try
            {
                Reset();

                new Thread(() => { s_webClient.DownloadFileAsync(new Uri(fileURL), tempFile); }).Start();

                var startTime = DateTime.Now;
                while (!s_webClient.IsBusy && (DateTime.Now - startTime).Seconds < 5)
                    Thread.Sleep(25);
                if (!s_webClient.IsBusy)
                    Console.WriteLine("ERROR! Timeout trying to download from: " + fileURL);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception downloading file from: " + fileURL);
                Console.WriteLine(ex);
            }
        }
    }
}

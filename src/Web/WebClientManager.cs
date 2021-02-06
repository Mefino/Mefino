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

        //public static bool LastDownloadSuccess => s_lastDownloadSuccess;
        //private static bool s_lastDownloadSuccess;

        public static bool IsBusy => s_webClient?.IsBusy ?? false;

        // ======== Internal =========

        internal static void Initialize()
        {
            s_webClient.DownloadProgressChanged += OnDownloadProgress;
            //s_webClient.DownloadFileCompleted += OnDownloadCompleted;
        }

        private static void OnDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            s_lastDownloadProgressPercent = e.ProgressPercentage;
            // Mefino.SendAsyncProgress(e.ProgressPercentage);
        }

        //private static void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        //{
        //    s_lastDownloadProgressPercent = 100;
        //    s_lastDownloadSuccess = !e.Cancelled && e.Error == null;
        //}

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
        
        // ========= Public ==========

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

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
        internal static WebClient WebClient => s_webClient;
        private static readonly WebClient s_webClient = new WebClient();

        internal static int s_lastDownloadProgressPercent;

        internal static void Initialize()
        {
            s_webClient.DownloadProgressChanged += OnDownloadProgress;
            s_webClient.DownloadFileCompleted += OnDownloadCompleted;

            Reset();
        }

        private static void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            s_lastDownloadProgressPercent = 100;
        }

        private static void OnDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            s_lastDownloadProgressPercent = e.ProgressPercentage;
        }

        internal static void Reset()
        {
            //s_webClient?.Dispose();

            //s_webClient = new WebClient();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
                | SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12
                | (SecurityProtocolType)3072;

            s_webClient.Headers.Clear();
            s_webClient.Headers.Add("User-Agent", "request");
        }

        internal static void DownloadFileAsync(string fileURL, string tempFile)
        {
            Reset();

            new Thread(() =>
            {
                WebClient.DownloadFileAsync(new Uri(fileURL), tempFile);

            }).Start();
        }
    }
}

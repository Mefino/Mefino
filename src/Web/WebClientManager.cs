using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mefino.Loader.Web
{
    internal static class WebClientManager
    {
        internal static WebClient WebClient => s_webClient;
        private static readonly WebClient s_webClient = new WebClient();

        internal static void Initialize()
        {
            //s_webClient.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs info)
            //    => OnWebClientDownloadProgress(info);

            Reset();
        }

        //internal static void OnWebClientDownloadProgress(DownloadProgressChangedEventArgs info)
        //{
        //    if (info.ProgressPercentage == 100)
        //        Console.WriteLine("DONE!");
        //    else if (info.ProgressPercentage % 10 == 0)
        //        Console.Write($"{info.ProgressPercentage}...");
        //}

        internal static void Reset()
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
    }
}

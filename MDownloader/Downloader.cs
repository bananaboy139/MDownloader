using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace MDownloader
{
    public class Downloader
    {
        private ProgressTask task;

        public async Task DownloadAsync(DownloadLink L, ProgressTask taskin)
        {
            //URL to URI
            Uri URI = new Uri(L.URL);
            //Get downloads folder path

            task = taskin;

            if (URI.IsFile)
            {
                string filename = Path.GetFileName(URI.LocalPath);

                WebClient Wc = new WebClient()
                {
                    Credentials = L.Credentials
                };
                Wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                //Wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                try
                {
                    await Wc.DownloadFileTaskAsync(URI, Path.Combine(L.path, filename));
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                }
            }
        }

        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            AnsiConsole.Render(
                new FigletText("Done!")
                    .LeftAligned()
                    .Color(Color.Green));
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            task.MaxValue = e.TotalBytesToReceive;
            task.Increment(e.BytesReceived);
        }
    }

    public class DownloadLink
    {
        public string URL;

        public NetworkCredential Credentials;

        public IWebProxy Proxy;

        public string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), 
            "Downloads/");
    }
}

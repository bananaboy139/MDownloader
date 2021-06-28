using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDownloader
{
    public class Downloader
    {
        public void DownloadAsync(DownloadLink L)
        {
            //URL to URI
            Uri URI = new Uri(L.URL);
            //Get downloads folder path

            if (URI.IsFile)
            {
                string filename = Path.GetFileName(URI.LocalPath);

                WebClient Wc = new WebClient()
                {
                    Credentials = L.Credentials,
                    Proxy = L.Proxy
                };
                //Wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                //Wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                Wc.DownloadFileAsync(URI, L.path + filename);
            }
        }

        public float percentage = 0f;

        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
            percentage = (float)e.ProgressPercentage;
        }
    }

    public class DownloadLink
    {
        public string URL;

        public string URI;

        public NetworkCredential Credentials;

        public IWebProxy Proxy;

        public string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), 
            "Downloads");
    }
}

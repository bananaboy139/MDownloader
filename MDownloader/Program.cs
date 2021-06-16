using System;
using System.IO;
using Spectre.Console;

namespace MDownloader
{
    class Program
    {
        static void Main(string[] args)
        {


            DownloadLink LINK = new DownloadLink();
            //get link info
            LINK.URL = AnsiConsole.Ask<string>("What is the [blue]URL[/]?");
            if (!AnsiConsole.Confirm("Download to [green]Downloads Folder?[/]"))
            {
                LINK.path = Path.GetFullPath(AnsiConsole.Ask<string>("[green]Download path?[/]"));
            }//missing credentials and proxy

            //Download file
            Downloader Dow = new Downloader();
            //fixme: not implemented yet
            AnsiConsole.Progress()
                .Start(ctx =>
                {
                    // Define tasks
                    var task1 = ctx.AddTask("[green]Downloading[/]");

                    while (!ctx.IsFinished)
                    {
                        task1.Increment(1.5);
                    }
                });
            Dow.DownloadAsync(LINK);
        }
    }
}

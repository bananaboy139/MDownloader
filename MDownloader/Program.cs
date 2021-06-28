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
            string URL = AnsiConsole.Ask<string>("What is the [blue]URL[/]?");
            LINK.URL = URL;
            string filename = Path.GetFileName(new Uri(URL).LocalPath);

            if (!AnsiConsole.Confirm("Download to [green]Downloads Folder?[/]"))
            {
                LINK.path = Path.GetFullPath(AnsiConsole.Ask<string>("[green]Download path?[/]"));
            }


            if (AnsiConsole.Confirm("[green]Credentials?[/]"))
            {
                string user = AnsiConsole.Ask<string>("What is the [blue]Username[/]?");
                string pass = AnsiConsole.Prompt<string>(new TextPrompt<string>("What is the [red]Password[/]?")
                    .PromptStyle("red")
                    .Secret()
                    );
                LINK.Credentials = new System.Net.NetworkCredential(user, pass);
            }//missing proxy
            

            //Download file
            Downloader Dow = new Downloader();
            //fixme: not implemented yet
            AnsiConsole.Progress()
                .AutoClear(false)
                .HideCompleted(false)
                .Columns(new ProgressColumn[]
                {
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new RemainingTimeColumn(),
                    new SpinnerColumn()
                })
                .StartAsync(async ctx =>
                {
                    var task = ctx.AddTask("Download [link=" + URL + "]" + filename + "[/]");

                    while(!ctx.IsFinished)
                    {
                        //cannot await void
                        await Dow.DownloadAsync(LINK);
                        
                        task.Increment(0.1);
                    }
                });
            
        }
    }
}

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace xkpasswd.cli
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var app = new CommandApp();
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, _) => cts.Cancel();
            app.Configure(config => { config.AddCommand<GenerateCommand>("generate"); });
            return await app.RunAsync(args.Any() ? args : new[] {"generate"}).ConfigureAwait(false);
        }
    }
}
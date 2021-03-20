using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Spectre.Console.Cli;

namespace xkpasswd.cli
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var logFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "xkpasswd.cli\\xkpasswd.cli.log");

            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                .WriteTo.Debug()
                .WriteTo.File(logFileName, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            var app = new CommandApp();
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, _) => cts.Cancel();
            app.Configure(config => { config.AddCommand<GenerateCommand>("generate"); });
            return await app.RunAsync(args.Any() ? args : new[] {"generate",}).ConfigureAwait(false);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Logger.Fatal(e.ExceptionObject as Exception, "Unhandled Exception");
        }
    }
}
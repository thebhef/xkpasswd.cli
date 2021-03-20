using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Serilog;
using Spectre.Console;
using Spectre.Console.Cli;

namespace xkpasswd.cli
{
    internal class GenerateCommand : ICommand<GenerateSettings>
    {
        public async Task<int> Execute(CommandContext context, GenerateSettings settings)
        {
            try
            {
                var generator = JsonXkPasswdConverter.FromJson(await LoadJsonAsync(settings.SettingsPath).ConfigureAwait(false));
                var count = settings.Number;
                while (--count > -1)
                {
                    Console.WriteLine(generator().Generate());
                    if (count % 5 == 0) { Console.WriteLine(); }
                }
            }
            catch (Exception e)
            {
                Log.Logger.Fatal(e, "Unhandled exception while generating passwords");
                Console.WriteLine($"Fatal: {e}");
                return -1;
            }

            return 0;
        }

        public ValidationResult Validate(CommandContext context, CommandSettings settings)
        {
            if (settings is GenerateSettings gs && (gs.SettingsPath == null || File.Exists(gs.SettingsPath)))
            {
                return ValidationResult.Success();
            }

            return ValidationResult.Error("invalid path was provided");
        }

        public async Task<int> Execute(CommandContext context, CommandSettings settings)
        {
            if (settings is GenerateSettings gs) { return await Execute(context, gs).ConfigureAwait(false); }

            throw new NotSupportedException("Unsupported settings type");
        }

        private async Task<string> LoadJsonAsync(string? path)
        {
            Log.Logger.Debug($"Loading json from {path ?? "default"}");
            var assembly = Assembly.GetExecutingAssembly();

            var sr = path != null
                ? File.OpenText(path)
                : new StreamReader(assembly.GetManifestResourceStream("xkpasswd.cli.default.json")!);

            Log.Logger.Debug("Opened stream, reading text and returning the contents");
            return await sr.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}
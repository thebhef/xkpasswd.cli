using Spectre.Console.Cli;

namespace xkpasswd.cli
{
    internal class GenerateSettings : CommandSettings
    {
        [CommandOption("-s|--settings <settings_json>")]
        public string? SettingsPath { get; set; }

        [CommandOption("-n|--number <number>")]
        public int Number { get; set; } = 1;
    }
}
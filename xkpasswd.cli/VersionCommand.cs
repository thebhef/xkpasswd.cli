using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace xkpasswd.cli
{
    internal class VersionCommand : ICommand<VersionSettings>
    {
        public Task<int> Execute(CommandContext context, VersionSettings settings) => Execute(context, (CommandSettings) settings);

        public ValidationResult Validate(CommandContext context, CommandSettings settings) => ValidationResult.Success();

        public Task<int> Execute(CommandContext context, CommandSettings settings)
        {
            var assemblyInfo = Assembly.GetExecutingAssembly();
            var name = assemblyInfo.GetName().Name;
            var infoVersion = assemblyInfo.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var assemblyVersion = assemblyInfo.GetName().Version;
            Console.WriteLine($"{name} {infoVersion?.InformationalVersion ?? "none"}");
            Console.WriteLine($"Assembly: {assemblyVersion?.ToString() ?? "none"}");
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty);
            Console.WriteLine($"File: {fvi.FileVersion ?? "none"}");

            return Task.FromResult(0);
        }
    }

    internal class VersionSettings : CommandSettings
    {
    }
}
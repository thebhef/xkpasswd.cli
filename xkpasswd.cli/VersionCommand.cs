using System;
using System.Reflection;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace xkpasswd.cli
{
    internal class VersionCommand : ICommand<VersionSettings>
    {
        public Task<int> Execute(CommandContext context, VersionSettings settings) => Execute(context, settings);

        public ValidationResult Validate(CommandContext context, CommandSettings settings) => ValidationResult.Success();

        public Task<int> Execute(CommandContext context, CommandSettings settings)
        {
            var assemblyInfo = Assembly.GetExecutingAssembly();
            var name = assemblyInfo.GetName().Name;
            var infoVersion = assemblyInfo.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var fileVersion = assemblyInfo.GetCustomAttribute<AssemblyFileVersionAttribute>();
            var assemblyVersion = assemblyInfo.GetCustomAttribute<AssemblyVersionAttribute>();
            Console.WriteLine($"{name} {infoVersion?.InformationalVersion??"none"}");
            Console.WriteLine($"Assembly: {assemblyVersion?.Version??"none"}");
            Console.WriteLine($"File: {fileVersion?.Version??"none"}");

            return Task.FromResult(0);
        }
    }

    internal class VersionSettings : CommandSettings
    {
    }
}
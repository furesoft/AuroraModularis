using AuroraModularis.Core;
using AuroraModularis.DefaultImplementations;

namespace AuroraModularis;

public class BootstrapperBuilder : IBootstrapBuilder
{
    internal BootstrapperBuilder()
    {
    }

    public ModuleConfigration Configuration { get; } = new();

    public static IBootstrapBuilder StartConfigure() => new BootstrapperBuilder();

    public Task BuildAndStartAsync()
    {
        Configuration.SettingsProvider ??= new DefaultSettingsProvider();

        return Bootstrapper.RunAsync(Configuration);
    }
}
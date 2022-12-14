using AuroraModularis.Core;

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
        if (Configuration.SettingsProvider == null)
        {
            Configuration.SettingsProvider = new DefaultSettingsProvider();
        }

        return Bootstrapper.RunAsync(Configuration);
    }
}
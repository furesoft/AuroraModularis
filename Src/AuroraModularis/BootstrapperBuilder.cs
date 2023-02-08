using AuroraModularis.Core;
using AuroraModularis.DefaultImplementations;
using AuroraModularis.ModuleLoaders;

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
        Configuration.Loader ??= new FileModuleLoader(Environment.CurrentDirectory);

        return Bootstrapper.RunAsync(Configuration);
    }
}
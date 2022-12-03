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

        return Task.Run(() => Bootstrapper.RunAsync(Configuration));
    }
}
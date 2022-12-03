namespace AuroraModularis;

public class BootstrapperBuilder : IBootstrapBuilder
{
    internal BootstrapperBuilder()
    {
    }

    public ModularConfiguration Configuration { get; } = new();

    public static IBootstrapBuilder Build() => new BootstrapperBuilder();

    public Task BuildAndStartAsync()
    {
        if (Configuration.SettingsProvider == null)
        {
            Configuration.SettingsProvider = new DefaultSettingsProvider();
        }

        return Bootstrapper.RunAsync(Configuration);
    }
}
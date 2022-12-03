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
        return Bootstrapper.RunAsync(Configuration);
    }
}
namespace AuroraModularis;

public interface IBootstrapBuilder
{
    public ModularConfiguration Configuration { get; }

    Task BuildAndStartAsync();
}
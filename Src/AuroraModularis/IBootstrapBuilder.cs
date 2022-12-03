namespace AuroraModularis;

public interface IBootstrapBuilder
{
    public ModuleConfigration Configuration { get; }

    Task BuildAndStartAsync();
}
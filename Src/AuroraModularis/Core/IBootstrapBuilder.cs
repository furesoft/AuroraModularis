namespace AuroraModularis.Core;

public interface IBootstrapBuilder
{
    public ModuleConfigration Configuration { get; }

    Task BuildAndStartAsync();
}
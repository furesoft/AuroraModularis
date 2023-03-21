namespace AuroraModularis.Core.Hooks;

/// <summary>
/// A hook to tweak the behavior of module loading
/// </summary>
public interface IModuleLoadingHook : IModuleHook
{
    bool ShouldLoadModule(Module module);

    void BeforeLoadModule(Type moduleType);
    void AfterLoadModule(Module module);
}
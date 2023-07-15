namespace AuroraModularis.Core.Hooks;

/// <summary>
/// A hook to do actions before and after module loading
/// </summary>
public interface IModulesLoadedHook : IModuleHook
{
    void AfterModulesLoaded();
    void BeforeModulesLoaded();
}
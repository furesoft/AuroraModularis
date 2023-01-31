namespace AuroraModularis.Core.Hooks;

public interface IModuleLoadingHook : IModuleHook
{
    bool ShouldLoadModule(Module module);

    void BeforeLoadModule(Type moduleType);
    void AfterLoadModule(Module module);
}
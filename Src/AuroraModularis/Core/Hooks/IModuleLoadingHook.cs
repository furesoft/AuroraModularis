using AuroraModularis.Core;

namespace AuroraModularis.Hooks.Core;

public interface IModuleLoadingHook : IModuleHook
{
    bool ShouldLoadModule(Module module);

    void BeforeLoadModule(Type moduleType);
    void AfterLoadModule(Module module);
}
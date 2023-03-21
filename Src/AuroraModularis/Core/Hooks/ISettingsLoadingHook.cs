namespace AuroraModularis.Core.Hooks;

/// <summary>
/// Hook to tweak the loading of specific module settings
/// </summary>
public interface ISettingsLoadingHook : IModuleHook
{
    object LoadSettingForModule(Type moduleType);
}
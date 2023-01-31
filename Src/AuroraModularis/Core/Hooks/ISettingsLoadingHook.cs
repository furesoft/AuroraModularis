namespace AuroraModularis.Core.Hooks;

public interface ISettingsLoadingHook : IModuleHook
{
    object LoadSettingForModule(Type moduleType);
}
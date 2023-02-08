using AuroraModularis.Core;

namespace AuroraModularis;

public class ModuleConfigration
{
    public ModuleConfigration()
    {
    }

    public string ApplicationName { get; set; }

    public ModuleHookProvider Hooks { get; set; } = new();

    public IModuleSettingsProvider SettingsProvider { get; set; }

    public IModuleLoader Loader { get; set; }
}
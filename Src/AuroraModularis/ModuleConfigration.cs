using AuroraModularis.Core;

namespace AuroraModularis;

public class ModuleConfigration
{
    internal ModuleConfigration()
    {
    }

    public string ApplicationName { get; set; }
    public string ModulesPath { get; set; }
    public string SettingsBasePath { get; set; }

    public ModuleHookProvider Hooks { get; set; } = new();

    public IModuleSettingsProvider SettingsProvider { get; set; }

    public ModuleLoader Loader { get; set; }
}
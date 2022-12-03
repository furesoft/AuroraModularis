namespace AuroraModularis;

public class ModuleConfigration
{
    internal ModuleConfigration()
    {
    }

    public string ApplicationName { get; set; }
    public string ModulesPath { get; set; }
    public string SettingsBasePath { get; set; }

    public IModuleSettingsProvider SettingsProvider { get; set; }
}
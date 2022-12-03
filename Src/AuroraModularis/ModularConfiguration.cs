namespace AuroraModularis;

public class ModularConfiguration
{
    public ModularConfiguration(string modulesPath, string applicationName, string settingsBasePath)
    {
        ModulesPath = modulesPath;
        ApplicationName = applicationName;
        SettingsBasePath = settingsBasePath;
    }

    public string ApplicationName { get; set; }
    public string ModulesPath { get; set; }
    public string SettingsBasePath { get; set; }
}
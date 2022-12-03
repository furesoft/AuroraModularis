namespace AuroraModularis;

public class ModularConfiguration
{
    internal ModularConfiguration()
    {
    }

    public string ApplicationName { get; set; }
    public string ModulesPath { get; set; }
    public string SettingsBasePath { get; set; }

    public Type SettingsProviderType { get; set; }
}
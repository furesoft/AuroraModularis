namespace AuroraModularis;

public class ModularConfiguration
{
    public ModularConfiguration(string modulesPath, string applicationName)
    {
        ModulesPath = modulesPath;
        ApplicationName = applicationName;
    }

    public string ApplicationName { get; set; }
    public string ModulesPath { get; set; }
}
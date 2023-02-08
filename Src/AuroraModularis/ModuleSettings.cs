namespace AuroraModularis;

public class ModuleSettings
{
    private readonly string settingsFilePath;
    private readonly ModuleConfigration config;

    internal ModuleSettings(Module module, ModuleConfigration config)
    {
        settingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), config.ApplicationName, module.Name + ".json");
        this.config = config;
        
        EnsureDirectoryExists();
    }

    public void Save(object data)
    {
        EnsureDirectoryExists();

        config.SettingsProvider.Save(data, settingsFilePath);
    }

    private void EnsureDirectoryExists()
    {
        var fileInfo = new FileInfo(settingsFilePath);
        if (!fileInfo.Directory.Exists)
        {
            fileInfo.Directory.Create();
        }
    }

    public object Load(Type type)
    {
        EnsureDirectoryExists();
        
        if (!File.Exists(settingsFilePath))
        {
            Save(Activator.CreateInstance(type));
        }

        return config.SettingsProvider.Load(settingsFilePath, type);
    }
}
﻿namespace AuroraModularis;

public class ModuleSettings
{
    private readonly string path;
    private readonly ModularConfiguration config;

    internal ModuleSettings(Module module, ModularConfiguration config)
    {
        path = Path.Combine(config.ModulesPath, module.ID + ".json");
        this.config = config;
    }

    public void Save(object data)
    {
        if (!Directory.Exists(config.SettingsBasePath))
        {
            Directory.CreateDirectory(config.SettingsBasePath);
        }

        config.SettingsProvider.Save(data, path);
    }

    public object Load(Type type)
    {
        if (!File.Exists(path))
        {
            Save(Activator.CreateInstance(type));
        }

        return config.SettingsProvider.Load(path, type);
    }
}
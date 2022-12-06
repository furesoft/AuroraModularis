namespace AuroraModularis.Core;

public interface IModuleSettingsProvider
{
    void Save(object data, string path);

    object Load(string path, Type type);
}
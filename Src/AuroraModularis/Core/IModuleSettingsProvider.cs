namespace AuroraModularis.Core;

/// <summary>
/// Interface to allow custom setting file types
/// </summary>
public interface IModuleSettingsProvider
{
    void Save(object data, string path);

    object Load(string path, Type type);
}
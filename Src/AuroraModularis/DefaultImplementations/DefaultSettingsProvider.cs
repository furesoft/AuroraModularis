using System.Text.Json;
using AuroraModularis.Core;

namespace AuroraModularis.DefaultImplementations;

/// <summary>
/// Json Settings Provider
/// </summary>
internal class DefaultSettingsProvider : IModuleSettingsProvider
{
    public object Load(string path, Type type)
    {
        return JsonSerializer.Deserialize(File.ReadAllText(path), type);
    }

    public void Save(object data, string path)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(path, json);
    }
}
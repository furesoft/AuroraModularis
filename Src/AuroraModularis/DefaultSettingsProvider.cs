using System.Text.Json;

namespace AuroraModularis;

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
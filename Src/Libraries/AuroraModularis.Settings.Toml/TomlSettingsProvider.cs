using AuroraModularis.Core;
using Tomlet;

namespace AuroraModularis.Settings.Toml;

public class TomlSettingsProvider : IModuleSettingsProvider
{
    public void Save(object data, string path)
    {
        var document = TomletMain.DocumentFrom(data);
        
        File.WriteAllText(path, TomletMain.TomlStringFrom(document));
    }

    public object Load(string path, Type type)
    {
        var source = File.ReadAllText(path);
        var parser = new TomlParser();
        var tomlDocument = parser.Parse(source);
        
        return TomletMain.To(type, tomlDocument);
    }
}
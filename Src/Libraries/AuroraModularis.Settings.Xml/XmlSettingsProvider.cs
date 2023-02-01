using System.Xml.Serialization;
using AuroraModularis.Core;

namespace AuroraModularis.Settings.Xml;

public class XmlSettingsProvider : IModuleSettingsProvider
{
    public void Save(object data, string path)
    {
        var serializer = new XmlSerializer(data.GetType());
        using var fileStream = File.OpenWrite(path);
        
        serializer.Serialize(fileStream, data);
    }

    public object Load(string path, Type type)
    {
        var serializer = new XmlSerializer(type);
        using var fileStream = File.OpenRead(path);
        
        return serializer.Deserialize(fileStream);
    }
}
using System.Text.Json;

namespace AuroraModularis
{
    public class ModuleSettings
    {
        private readonly string path;
        private readonly string basePath;

        //ToDo: Maybe add SettingsProvider to make settings source changable (eg. json, xml, csv, toml)
        internal ModuleSettings(Module module, string basePath)
        {
            path = Path.Combine(basePath, module.ID + ".json");
            this.basePath = basePath;
        }

        public void Save(object data)
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(path, json);
        }

        public object Load(Type type)
        {
            if (!File.Exists(path))
            {
                Save(new());
            }

            return JsonSerializer.Deserialize(File.ReadAllText(path), type);
        }
    }
}
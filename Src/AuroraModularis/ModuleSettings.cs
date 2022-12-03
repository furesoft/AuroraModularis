using System.Text.Json;

namespace AuroraModularis
{
    public class ModuleSettings
    {
        private readonly string path;
        private readonly string basePath;

        internal ModuleSettings(Module module, ModularConfiguration config)
        {
            path = Path.Combine(config.ModulesPath, module.ID + ".json");
            basePath = config.SettingsBasePath;
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
                Save(Activator.CreateInstance(type));
            }

            return JsonSerializer.Deserialize(File.ReadAllText(path), type);
        }
    }
}
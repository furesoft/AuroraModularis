using System.Reflection;
using AuroraModularis.Core;

namespace AuroraModularis.ModuleLoaders;

public class FileModuleLoader : IModuleLoader
{
    public FileModuleLoader(string path)
    {
        Path = path;
    }
    public string Path { get; set; }
    
    public IEnumerable<Type> LoadModuleTypes(ModuleLoadingContext context)
    {
        var moduleTypes = new List<Type>();
        foreach (var modPath in Directory.GetFiles(Path, "*.dll"))
        {
            try
            {
                var moduleType = Assembly.LoadFrom(modPath).GetTypes().FirstOrDefault(type => !type.IsAbstract && type.IsAssignableTo(typeof(Module)));
                if (moduleType != null)
                {
                    moduleTypes.Add(moduleType);

                    
                }
            }
            catch (Exception ex)
            {
            }
        }

        return moduleTypes;
    }
}
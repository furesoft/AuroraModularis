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
    
    static bool IsModuleType(Type t)
    {
        if (t.BaseType == null) return false;

        if (t.BaseType.FullName == typeof(Module).FullName) return true;
        
        return t.BaseType.FullName.StartsWith(typeof(Module).FullName) || IsModuleType(t.BaseType);
    }
    
    public IEnumerable<Type> LoadModuleTypes(ModuleLoadingContext context)
    {
        var moduleTypes = new List<Type>();
        foreach (var modPath in Directory.GetFiles(Path, "*.dll"))
        {
            try
            {
                var types = Assembly.LoadFrom(modPath).GetTypes().Where(_=> !_.IsAbstract && !_.IsInterface);
                
                var moduleType = types.FirstOrDefault(IsModuleType);
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
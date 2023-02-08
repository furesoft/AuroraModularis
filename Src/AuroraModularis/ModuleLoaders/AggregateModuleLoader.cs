using AuroraModularis.Core;

namespace AuroraModularis.ModuleLoaders;

public class AggregateModuleLoader : IModuleLoader
{
    public List<IModuleLoader> Loaders { get; set; } = new();
    public IEnumerable<Type> LoadModuleTypes(ModuleLoadingContext context)
    {
        return Loaders.SelectMany(_ => _.LoadModuleTypes(context));
    }
}
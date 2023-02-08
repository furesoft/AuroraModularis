namespace AuroraModularis.Core;

public interface IModuleLoader
{
    IEnumerable<Type> LoadModuleTypes(ModuleLoadingContext context);
}
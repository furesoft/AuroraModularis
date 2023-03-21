namespace AuroraModularis.Core;

/// <summary>
/// Interface to write custom module loaders
/// </summary>
public interface IModuleLoader
{
    IEnumerable<Type> LoadModuleTypes(ModuleLoadingContext context);
}
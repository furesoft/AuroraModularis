namespace AuroraModularis.Core;

/// <summary>
/// Service to find types that implements a specific interface
/// </summary>
public interface ITypeFinder
{
    Type[] FindTypes<T>();
    T[] FindAndResolveTypes<T>();
}
namespace AuroraModularis.Core;

public interface ITypeFinder
{
    Type[] FindTypes<T>();
    T[] FindAndResolveTypes<T>();
}
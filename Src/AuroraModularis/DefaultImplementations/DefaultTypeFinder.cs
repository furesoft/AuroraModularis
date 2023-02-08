using AuroraModularis.Core;

namespace AuroraModularis.DefaultImplementations;

internal class DefaultTypeFinder : ITypeFinder
{
    public Type[] FindTypes<T>()
    {
        return (
            from assembly in AppDomain.CurrentDomain.GetAssemblies()
            where !assembly.IsDynamic
            from type in TryGetTypes(assembly)
            where typeof(T).IsAssignableFrom(type) && !type.IsInterface
            select type).ToArray();
    }

    public T[] FindAndResolveTypes<T>()
    {
        return (
            from type in FindTypes<T>()
            select ServiceContainer.Current.Resolve<T>(type)).ToArray();
    }
    
    private static Type[] TryGetTypes(System.Reflection.Assembly s)
    {
        try
        {
            return s.GetTypes();
        }
        catch
        {
            return Array.Empty<Type>();
        }
    }
}
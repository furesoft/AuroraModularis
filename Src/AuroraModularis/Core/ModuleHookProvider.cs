namespace AuroraModularis.Core;

public class ModuleHookProvider
{
    private List<IModuleHook> _hooks = new();

    /// <summary>
    /// Regiser a new instance of the hook type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Register<T>()
        where T : IModuleHook, new()
    {
        Register(new T());
    }

    /// <summary>
    /// Register a module hook
    /// </summary>
    /// <param name="hook"></param>
    public void Register(IModuleHook hook)
    {
        _hooks.Add(hook);
    }

    /// <summary>
    /// Invoke a hook that returns a value. The last hook will be used.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetReturningHook<T>()
        where T : IModuleHook
    {
        return _hooks.OfType<T>().LastOrDefault();
    }
    
    /// <summary>
    /// Invoke all hooks by a proxy object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetHook<T>()
        where T : class, IModuleHook
    {
        return (T)ModuleHookProxy<T>.Create(_hooks.OfType<T>());
    }
}
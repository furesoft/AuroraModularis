using System.Reflection;
using System.Runtime.CompilerServices;

namespace AuroraModularis.Core;

public class ModuleHookProvider
{
    private List<IModuleHook> _hooks = new();

    public void Register<T>()
        where T : IModuleHook, new()
    {
        _hooks.Add(new T());
    }

    public T GetReturningHook<T>()
        where T : IModuleHook
    {
        return _hooks.OfType<T>().LastOrDefault();
    }
    
    public T GetHook<T>()
        where T : IModuleHook
    {
        var proxy = new ModuleHookProxy<T>
        {
            Hooks = _hooks.OfType<T>()
        };

        return DispatchProxy.Create<T, ModuleHookProxy<T>>();
    }
}
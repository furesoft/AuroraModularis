using System.Reflection;

namespace AuroraModularis.Core;

internal class ModuleHookProxy<T> : DispatchProxy
    where T : IModuleHook
{
    public IEnumerable<T> Hooks { get; set; } = Array.Empty<T>();
    
    public static IModuleHook Create(IEnumerable<T> hooks)
    {
        var proxy = Create<T, ModuleHookProxy<T>>() as ModuleHookProxy<T>;
        proxy.Hooks = hooks;
        
        return (IModuleHook)proxy;
    }
    
    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        if(targetMethod.ReturnType != typeof(void))
        {
            throw new InvalidOperationException("Hooks cannot work with returntypes. Use `GetReturningHook<T>()` instead");
        }

        if (!Hooks.Any())
        {
            return null;
        }
        
        foreach (var hook in Hooks)
        {
            var hookMethodInfo =
                hook.GetType().GetMethod(targetMethod.Name, BindingFlags.Instance | BindingFlags.Public);

            hookMethodInfo.Invoke(hook, args);
        }

        return null;
    }
}
using System.Reflection;

namespace AuroraModularis.Core;

internal sealed class ModuleHookProxy<T> : DispatchProxy
{
    public IEnumerable<T> Hooks { get; set; }
    
    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        if(targetMethod.ReturnType != typeof(void))
        {
            throw new InvalidOperationException("Hooks cannot work with returntypes. Use `GetReturningHook<T>()` instead");
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
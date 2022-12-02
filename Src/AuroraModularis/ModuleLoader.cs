using AuroraModularis.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Loader;

namespace AuroraModularis;

internal class ModuleLoader
{
    public Dictionary<Module, AssemblyLoadContext> Modules { get; set; } = new();

    public Module Load(string path, MessageBroker messageBroker)
    {
        var loadContext = new AssemblyLoadContext(null, true);
        loadContext.LoadFromAssemblyPath(path);

        var moduleType = loadContext.Assemblies.FirstOrDefault().GetTypes().FirstOrDefault(_ => _.IsAssignableTo(typeof(Module)));
        if (moduleType != null)
        {
            var moduleInstance = (Module)Activator.CreateInstance(moduleType);
            moduleInstance.ID = Guid.NewGuid();
            moduleInstance.Inbox = new(messageBroker);
            moduleInstance.Outbox = new(messageBroker);

            Modules.Add(moduleInstance, loadContext);

            return moduleInstance;
        }

        return null;
    }

    public void Init(IServiceCollection services)
    {
        foreach (var module in Modules)
        {
            module.Key.Init(services);
        }
    }

    public void Unload(Guid id)
    {
        var module = Modules.Keys.FirstOrDefault(_ => _.ID == id);

        if (module == null)
        {
            throw new KeyNotFoundException($"Module with id '{id}' not found");
        }

        UnloadModule(module);
    }

    public void Unload(string name)
    {
        var module = Modules.Keys.FirstOrDefault(_ => _.Name == name);

        if (module == null)
        {
            throw new KeyNotFoundException($"Module '{name}' not found");
        }

        UnloadModule(module);
    }

    private void UnloadModule(Module? module)
    {
        module.OnUnload();

        Modules[module].Unload();

        Modules.Remove(module);
    }
}
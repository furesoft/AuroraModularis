using AuroraModularis.Messaging;
using System.Collections.Concurrent;
using System.Reflection;

namespace AuroraModularis;

internal class ModuleLoader
{
    public ConcurrentBag<Module> Modules { get; set; } = new();

    public Module Load(string path, MessageBroker messageBroker)
    {
        var moduleType = Assembly.LoadFrom(path).GetTypes().FirstOrDefault(type => !type.IsAbstract && type.IsAssignableTo(typeof(Module)));
        if (moduleType != null)
        {
            var moduleInstance = (Module)TinyIoCContainer.Current.Resolve(moduleType);
            moduleInstance.ID = Guid.NewGuid();
            moduleInstance.Inbox = new(messageBroker);
            moduleInstance.Outbox = new(messageBroker);

            Modules.Add(moduleInstance);

            return moduleInstance;
        }

        return null;
    }

    public void Init(TinyIoCContainer container)
    {
        foreach (var module in Modules)
        {
            module.RegisterServices(container);
        }
    }
}
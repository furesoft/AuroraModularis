using AuroraModularis.Messaging;
using System.Collections.Concurrent;
using System.Reflection;

namespace AuroraModularis;

internal class ModuleLoader
{
    public ConcurrentBag<Module> Modules { get; set; } = new();

    public void Load(string modulesPath, MessageBroker messageBroker)
    {
        var moduleTypes = new List<Type>();
        foreach (var modPath in Directory.GetFiles(modulesPath, "*.dll"))
        {
            var moduleType = Assembly.LoadFrom(modPath).GetTypes().FirstOrDefault(type => !type.IsAbstract && type.IsAssignableTo(typeof(Module)));
            if (moduleType != null)
            {
                moduleTypes.Add(moduleType);
            }
        }

        var orderedModulesTypes = moduleTypes.OrderByDescending(HasPriorityAttribute);
        foreach (var moduleType in orderedModulesTypes)
        {
            var moduleInstance = (Module)TinyIoCContainer.Current.Resolve(moduleType);
            moduleInstance.ID = Guid.NewGuid();
            moduleInstance.Inbox = new(messageBroker);
            moduleInstance.Outbox = new(messageBroker);

            Modules.Add(moduleInstance);

            moduleInstance.RegisterServices(TinyIoCContainer.Current);
        }
    }

    private bool HasPriorityAttribute(Type type)
    {
        var attribute = type.GetCustomAttribute<PriorityAttribute>();

        return attribute != null;
    }
}
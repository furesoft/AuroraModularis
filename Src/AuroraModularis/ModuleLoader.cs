using AuroraModularis.Messaging;
using System.Collections.Concurrent;
using System.Reflection;

namespace AuroraModularis;

internal class ModuleLoader
{
    public ConcurrentBag<Module> Modules { get; set; } = new();

    public void Load(ModularConfiguration config, MessageBroker messageBroker)
    {
        var moduleTypes = new List<Type>();
        foreach (var modPath in Directory.GetFiles(config.ModulesPath, "*.dll"))
        {
            var moduleType = Assembly.LoadFrom(modPath).GetTypes().FirstOrDefault(type => !type.IsAbstract && type.IsAssignableTo(typeof(Module)));
            if (moduleType != null)
            {
                moduleTypes.Add(moduleType);
            }
        }

        var orderedModulesTypes = moduleTypes.OrderByDescending(GetModulePriority);
        foreach (var moduleType in orderedModulesTypes)
        {
            var moduleInstance = (Module)TinyIoCContainer.Current.Resolve(moduleType);
            moduleInstance.Inbox = new(messageBroker);
            moduleInstance.Outbox = new(messageBroker);

            moduleInstance.OnInit();

            if (moduleInstance.UseSettings)
            {
                InitSettings(config, moduleInstance);
            }

            Modules.Add(moduleInstance);

            moduleInstance.RegisterServices(TinyIoCContainer.Current);
        }
    }

    private static void InitSettings(ModularConfiguration config, Module moduleInstance)
    {
        moduleInstance.SettingsHandler = new(moduleInstance, config.SettingsBasePath);

        if (moduleInstance.Settings == null)
        {
            moduleInstance.Settings = new();
        }
        else
        {
            moduleInstance.Settings = moduleInstance.SettingsHandler.Load(moduleInstance.Settings.GetType());
        }
    }

    private ModulePriority GetModulePriority(Type type)
    {
        var attribute = type.GetCustomAttribute<PriorityAttribute>();

        return attribute != null ? attribute.Priority : ModulePriority.Normal;
    }
}
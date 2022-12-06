using AuroraModularis.Core;
using AuroraModularis.Hooks.Core;
using AuroraModularis.Messaging;
using System.Collections.Concurrent;
using System.Reflection;

namespace AuroraModularis;

public class ModuleLoader
{
    public ConcurrentBag<Module> Modules { get; set; } = new();

    public void Load(ModuleConfigration config, MessageBroker messageBroker)
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

        var orderedModulesTypes = moduleTypes.OrderByDescending(GetModulePriority).ToArray();
        for (int i = 0; i < orderedModulesTypes.Length; i++)
        {
            var moduleType = orderedModulesTypes[i];
            var moduleInstance = (Module)TinyIoCContainer.Current.Resolve(moduleType);

            var hook = config.Hooks.GetHook<IModuleLoadingHook>();
            if (hook != null && !hook.ShouldLoadModule(moduleInstance))
            {
                moduleTypes.Remove(moduleType);
                continue;
            }

            moduleInstance.Inbox = new(messageBroker);
            moduleInstance.Outbox = new(messageBroker);

            moduleInstance.OnInit();

            if (moduleInstance.UseSettings)
            {
                InitSettings(config, moduleInstance);
            }

            Modules.Add(moduleInstance);

            moduleInstance.RegisterServices(TinyIoCContainer.Current);

            hook?.AfterLoadModule(moduleInstance);
        }
    }

    private static void InitSettings(ModuleConfigration config, Module moduleInstance)
    {
        moduleInstance.SettingsHandler = new(moduleInstance, config);

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
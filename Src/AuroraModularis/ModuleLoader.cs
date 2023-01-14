using AuroraModularis.Core;
using AuroraModularis.Hooks.Core;
using AuroraModularis.Messaging;
using System.Collections.Concurrent;
using System.Reflection;

namespace AuroraModularis;

public class ModuleLoader
{
    private readonly ModuleConfigration _config;

    public ModuleLoader(ModuleConfigration config)
    {
        _config = config;
    }

    public ConcurrentBag<Module> Modules { get; private set; } = new();

    public void Load(MessageBroker messageBroker)
    {
        var moduleTypes = new List<Type>();
        var hook = _config.Hooks.GetHook<IModuleLoadingHook>();

        if (!string.IsNullOrEmpty(_config.ModulesPath))
        {
            foreach (var modPath in Directory.GetFiles(_config.ModulesPath, "*.dll"))
            {
                try
                {
                    var moduleType = Assembly.LoadFrom(modPath).GetTypes().FirstOrDefault(type => !type.IsAbstract && type.IsAssignableTo(typeof(Module)));
                    if (moduleType != null)
                    {
                        moduleTypes.Add(moduleType);

                        hook?.BeforeLoadModule(moduleType);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        var orderedModulesTypes = moduleTypes.OrderByDescending(GetModulePriority).ToArray();

        for (int i = 0; i < orderedModulesTypes.Length; i++)
        {
            var moduleType = orderedModulesTypes[i];
            var moduleInstance = Container.Current.Resolve<Module>(moduleType);

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
                InitSettings(_config, moduleInstance);
            }

            Modules.Add(moduleInstance);

            moduleInstance.RegisterServices(Container.Current);

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
using AuroraModularis.Core;
using AuroraModularis.Messaging;
using System.Collections.Concurrent;
using System.Reflection;
using AuroraModularis.Core.Hooks;
using AuroraModularis.DefaultImplementations;

namespace AuroraModularis;

public class ModuleLoader
{
    private readonly ModuleConfigration _config;

    public ModuleLoader(ModuleConfigration config)
    {
        _config = config;
    }

    public ConcurrentBag<Module> Modules { get; } = new();

    public void Load(MessageBroker messageBroker)
    {
        var moduleTypes = new List<Type>();
        var hook = _config.Hooks.GetHook<IModuleLoadingHook>();
        var returningHook = _config.Hooks.GetReturningHook<IModuleLoadingHook>();

        ServiceContainer.Current.Register<ITypeFinder>(new DefaultTypeFinder());

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

                        hook.BeforeLoadModule(moduleType);
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
            var moduleInstance = ServiceContainer.Current.Resolve<Module>(moduleType);

            if (returningHook != null && !returningHook.ShouldLoadModule(moduleInstance))
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

            moduleInstance.RegisterServices(ServiceContainer.Current);

            hook?.AfterLoadModule(moduleInstance);
        }
    }

    private void InitSettings(ModuleConfigration config, Module moduleInstance)
    {
        moduleInstance.SettingsHandler = new(moduleInstance, config);

        if (moduleInstance.Settings == null)
        {
            moduleInstance.Settings = new();
        }
        else
        {
            var hook = _config.Hooks.GetReturningHook<ISettingsLoadingHook>();

            if (hook != null)
            {
                moduleInstance.Settings = hook.LoadSettingForModule(moduleInstance.GetType());
            }
            else
            {
                moduleInstance.Settings = moduleInstance.SettingsHandler.Load(moduleInstance.Settings.GetType());
            }
        }
    }

    private ModulePriority GetModulePriority(Type type)
    {
        var attribute = type.GetCustomAttribute<PriorityAttribute>();

        return attribute != null ? attribute.Priority : ModulePriority.Normal;
    }
}
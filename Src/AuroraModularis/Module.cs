using AuroraModularis.Core;
using AuroraModularis.Messaging;

namespace AuroraModularis;

/// <summary>
/// The base class of modules. Used to determine if an assembly is a module
/// </summary>
public abstract class Module
{
    internal ModuleSettings SettingsHandler;

    public virtual string Name => GetType().FullName;
    
    public Inbox Inbox { get; set; }
    public Outbox Outbox { get; set; }
    
    /// <summary>
    /// The module specific settings
    /// </summary>
    public object Settings { get; set; }

    public virtual Type SettingsType => null;

    /// <summary>
    /// If UseSettings is true the Settings object will be automaticly serialized/deserialized on start/end of the process
    /// </summary>
    public bool UseSettings { get; set; }

    /// <summary>
    /// Method to register behavior to use it in other modules
    /// </summary>
    /// <param name="serviceContainer"></param>
    public virtual void RegisterServices(ServiceContainer serviceContainer)
    {
    }

    /// <summary>
    /// Will be called after RegisterServices. Can be used to init the inbox/outbox or services
    /// </summary>
    /// <param name="serviceContainer"></param>
    /// <returns></returns>
    public virtual Task OnStart(ServiceContainer serviceContainer)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Method to do initialisation of a module
    /// </summary>
    public virtual void OnInit()
    {
    }

    /// <summary>
    /// Method will be called on application exit
    /// </summary>
    public virtual void OnExit()
    {
        
    }
}
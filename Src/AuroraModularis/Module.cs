using AuroraModularis.Core;
using AuroraModularis.Messaging;

namespace AuroraModularis;

/// <summary>
/// The base class of modules. Used to determine if an assembly is a module
/// </summary>
public abstract class Module
{
    internal ModuleSettings SettingsHandler;

    /// <summary>
    /// A unique module name. If not specified the type's namespace will be used
    /// </summary>
    public virtual string Name
    { get { return this.GetType().Namespace; } }

    /// <summary>
    /// An inbox to receive messages from other modules
    /// </summary>
    public Inbox Inbox { get; internal set; }
    
    /// <summary>
    /// An outbox to send messages to other modules
    /// </summary>
    public Outbox Outbox { get; internal set; }

    /// <summary>
    /// The module specific settings
    /// </summary>
    public object Settings { get; set; }
    
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
    public abstract Task OnStart(ServiceContainer serviceContainer);

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
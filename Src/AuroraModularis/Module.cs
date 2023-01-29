using AuroraModularis.Core;
using AuroraModularis.Messaging;

namespace AuroraModularis;

public abstract class Module
{
    internal ModuleSettings SettingsHandler;

    public virtual string Name
    { get { return this.GetType().Namespace; } }

    public Inbox Inbox { get; internal set; }
    public Outbox Outbox { get; internal set; }

    public object Settings { get; set; }
    public bool UseSettings { get; set; }

    public virtual void RegisterServices(ServiceContainer serviceContainer)
    {
    }

    public abstract Task OnStart(ServiceContainer serviceContainer);

    public virtual void OnInit()
    {
    }

    public virtual void OnExit()
    {
        if (UseSettings)
        {
            SettingsHandler.Save(Settings);
        }
    }
}
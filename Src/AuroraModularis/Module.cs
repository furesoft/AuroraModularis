using AuroraModularis.Messaging;

namespace AuroraModularis;

public abstract class Module
{
    internal ModuleSettings SettingsHandler;

    public abstract ShortGuid ID { get; }
    public abstract string Name { get; }

    public Inbox Inbox { get; internal set; }
    public Outbox Outbox { get; internal set; }

    public object Settings { get; set; }
    public bool UseSettings { get; set; }

    public virtual void RegisterServices(TinyIoCContainer container)
    {
    }

    public abstract Task OnStart();

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
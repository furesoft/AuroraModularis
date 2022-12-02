using AuroraModularis.Messaging;

namespace AuroraModularis;

public abstract class Module
{
    public Guid ID { get; set; }
    public abstract string Name { get; }

    public Inbox Inbox { get; internal set; }
    public Outbox Outbox { get; internal set; }

    public virtual void RegisterServices(TinyIoCContainer container)
    {
    }

    public abstract Task OnStart();
}
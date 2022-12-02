using Microsoft.Extensions.DependencyInjection;

namespace AuroraModularis;

public abstract class Module
{
    public Guid ID { get; set; }
    public abstract string Name { get; }

    public virtual void Init(IServiceCollection services)
    {
    }

    public virtual void OnLoad()
    { }

    public virtual void OnUnload()
    { }
}
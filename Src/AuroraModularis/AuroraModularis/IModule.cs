using Microsoft.Extensions.DependencyInjection;

namespace AuroraModularis;

public interface IModule
{
    Guid ID { get; }
    string Name { get; }

    void Init(IServiceCollection services);

    void OnLoad();

    void OnUnload();
}
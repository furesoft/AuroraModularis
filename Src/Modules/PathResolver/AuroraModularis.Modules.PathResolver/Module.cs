using AuroraModularis.Modules.PathResolver.Models;

namespace AuroraModularis.Modules.PathResolver;

[Priority(ModulePriority.High)]
public class Module : AuroraModularis.Module
{
    public override Task OnStart()
    {
        return Task.CompletedTask;
    }

    public override void RegisterServices(TinyIoCContainer container)
    {
        container.Register<IPathResolver>(new PathResolver());
    }
}
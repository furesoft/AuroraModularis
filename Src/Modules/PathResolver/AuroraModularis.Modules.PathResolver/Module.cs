using AuroraModularis.Core;
using AuroraModularis.Modules.PathResolver.Models;

namespace AuroraModularis.Modules.PathResolver;

[Priority(ModulePriority.Max)]
public class Module : AuroraModularis.Module
{
    public override Task OnStart(TinyIoCContainer container)
    {
        return Task.CompletedTask;
    }

    public override void RegisterServices(TinyIoCContainer container)
    {
        container.Register<IPathResolver>(new PathResolver());
    }
}
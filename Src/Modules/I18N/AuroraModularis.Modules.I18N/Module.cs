using AuroraModularis.Core;

namespace AuroraModularis.Modules.I18N;

[Priority(ModulePriority.High)]
internal class Module : AuroraModularis.Module
{
    public override Task OnStart(ServiceContainer container)
    {
        return Task.CompletedTask;
    }
}

using AuroraModularis;
using Quartz;

namespace TestApp.Modules.TestSchedulingModule;

public class Module : AuroraModularis.Module, IScheduledModule
{
    public override ShortGuid ID => ShortGuid.NewGuid();

    public override string Name => "Scheudling";

    public List<(string, IJob)> Jobs => new() {
        ("0/29 0/1 * 1/1 * ? *", new HelloJob()),
        ("0/15 0/1 * 1/1 * ? *", new HelloJob())
    };

    public override Task OnStart()
    {
        return Task.CompletedTask;
    }
}
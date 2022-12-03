using AuroraModularis;
using Quartz;

namespace TestApp.Modules.TestSchedulingModule;

public class Module : AuroraModularis.Module, IScheduledModule
{
    public override string Name => "SchedulingTest";

    public List<(string, IJob)> Jobs => new() {
        ("0/29 0/1 * 1/1 * ? *", new HelloJob()),
        ("0/15 0/1 * 1/1 * ? *", new HelloJob())
    };

    public override Task OnStart()
    {
        return Task.CompletedTask;
    }
}
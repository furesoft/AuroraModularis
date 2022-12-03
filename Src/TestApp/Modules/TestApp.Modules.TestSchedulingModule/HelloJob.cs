using Quartz;
using System.Diagnostics;

namespace TestApp.Modules.TestSchedulingModule;

public class HelloJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Debug.WriteLine("Hello :)");

        return Task.CompletedTask;
    }
}
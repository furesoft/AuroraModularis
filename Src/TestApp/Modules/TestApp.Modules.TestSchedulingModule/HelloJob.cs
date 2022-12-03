using Quartz;

namespace TestApp.Modules.TestSchedulingModule;

public class HelloJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Hello :)");

        return Task.CompletedTask;
    }
}

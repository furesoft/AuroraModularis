using AuroraModularis.Messaging;
using Quartz;
using Quartz.Impl;

namespace AuroraModularis;

internal class Bootstrapper
{
    public static async Task RunAsync(ModuleConfigration config)
    {
        ModuleLoader moduleLoader = new();

        var messageBroker = new MessageBroker();
        messageBroker.Start();

        TinyIoCContainer.Current.AutoRegister();

        TinyIoCContainer.Current.Register(moduleLoader);
        moduleLoader.Load(config, messageBroker);

        var factory = new StdSchedulerFactory();
        var scheduler = await factory.GetScheduler();
        await ScheduleJobs(moduleLoader, scheduler);

        AppDomain.CurrentDomain.ProcessExit += async (s, e) =>
        {
            await scheduler.Shutdown();
            foreach (var module in moduleLoader.Modules)
            {
                module.OnExit();
            }
        };

        Task.WaitAll(moduleLoader.Modules.Select(_ => _.OnStart()).ToArray());
    }

    private static async Task ScheduleJobs(ModuleLoader moduleLoader, IScheduler scheduler)
    {
        foreach (var module in moduleLoader.Modules)
        {
            if (module is IScheduledModule scheduling)
            {
                foreach (var expr in scheduling.Jobs)
                {
                    var jobname = Guid.NewGuid() + module.Name + expr.Item2.GetType().FullName;
                    IJobDetail job = JobBuilder.Create(expr.Item2.GetType())
                    .WithIdentity(jobname, "group1")
                    .Build();

                    ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(jobname, "group1")
                    .WithCronSchedule(expr.Item1)
                    .ForJob(jobname, "group1")
                    .Build();

                    await scheduler.ScheduleJob(job, trigger);
                }

                await scheduler.Start();
            }
        }
    }
}
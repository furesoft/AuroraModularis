using AuroraModularis.Core;
using AuroraModularis.Messaging;
using Quartz;
using Quartz.Impl;

namespace AuroraModularis;

/// <summary>
/// A helper class to bootstrap an application
/// </summary>
internal static class Bootstrapper
{
    /// <summary>
    /// Run the bootstrapper
    /// </summary>
    /// <param name="config"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task RunAsync(ModuleConfigration config)
    {
        ModuleLoader moduleLoader = new(config);

        var messageBroker = new MessageBroker();
        messageBroker.Start();

        ServiceContainer.Current.Register(moduleLoader);

        if (config.Loader == null)
        {
            throw new InvalidOperationException("Cannot Load Modules without specifying a moduleloader");
        }
        
        moduleLoader.Load(messageBroker);
        
        var factory = new StdSchedulerFactory();
        var scheduler = await factory.GetScheduler();

        ServiceContainer.Current.Register(scheduler);

        await ScheduleJobs(moduleLoader, scheduler);

        AppDomain.CurrentDomain.ProcessExit += async (s, e) =>
        {
            await scheduler.Shutdown();
            
            foreach (var module in moduleLoader.Modules)
            {
                if (module.UseSettings)
                {
                    module.SettingsHandler.Save(module.Settings);
                }
                
                module.OnExit();
            }
        };

        await Task.WhenAll(moduleLoader.Modules.Where(_ => _ is IServiceInitializer)
            .OfType<IServiceInitializer>().Select(_ => _.Init()).ToArray());
            
        await Task.WhenAll(moduleLoader.Modules.Select(_ => _.OnStart(ServiceContainer.Current)).ToArray());
    }

    private static async Task ScheduleJobs(ModuleLoader moduleLoader, IScheduler scheduler)
    {
        foreach (var module in moduleLoader.Modules)
        {
            if (module is not IScheduledModule scheduling) continue;
            
            foreach (var expr in scheduling.Jobs)
            {
                var jobname = Guid.NewGuid() + module.Name + expr.Item2.GetType().FullName;
                var job = JobBuilder.Create(expr.Item2.GetType())
                    .WithIdentity(jobname, "group1")
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity(jobname, "group1")
                    .WithCronSchedule(expr.Item1)
                    .ForJob(jobname, "group1")
                    .Build();

                scheduling.Scheduler = scheduler;

                await scheduler.ScheduleJob(job, trigger);
            }

            await scheduler.Start();
        }
    }
}
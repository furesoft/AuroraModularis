using Quartz;

namespace AuroraModularis.Core;

/// <summary>
/// Interface to schedule subactions for timed jobs
/// </summary>
public interface IScheduledModule
{
    public List<(string, IJob)> Jobs { get; }
    IScheduler Scheduler { get; set; }
}
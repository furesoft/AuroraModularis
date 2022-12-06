using Quartz;

namespace AuroraModularis.Core;

public interface IScheduledModule
{
    public List<(string, IJob)> Jobs { get; }
}
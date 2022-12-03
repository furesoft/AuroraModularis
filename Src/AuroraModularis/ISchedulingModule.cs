using Quartz;

namespace AuroraModularis;

public interface IScheduledModule
{
    public List<(string, IJob)> Jobs { get; }
}
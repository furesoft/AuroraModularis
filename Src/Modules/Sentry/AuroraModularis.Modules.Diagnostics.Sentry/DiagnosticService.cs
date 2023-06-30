using Sentry;
using AuroraModularis.Modules.Diagnostics.Sentry.Models;

namespace AuroraModularis.Modules.Diagnostics.Sentry;

internal class DiagnosticServiceImpl : IDiagnosticService
{
    public IDisposable StartPerformanceMonitoring(string name, string operation)
    {
        return new PerformanceMonitor(name, operation);
    }

    public void TrackException(Exception exception)
    {
        SentrySdk.CaptureException(exception);
    }
}

using AuroraModularis.Core;
using Serilog;

namespace AuroraModularis.Modules.Logging.SeriLog;

[Priority(ModulePriority.Max)]
public class SerilogModule : AuroraModularis.Module
{
    public override Type SettingsType => typeof(SerilogOptions);

    public override void OnInit()
    {
        UseSettings = true;
    }

    public override void RegisterServices(ServiceContainer serviceContainer)
    {
        var logConfig = new LoggerConfiguration()
        .WriteTo.Console();

        var settings = (SerilogOptions) Settings;
        
        if (settings.LogFile != null)
        {
            logConfig.WriteTo.File(settings.LogFile);
        }

        if (settings.WriteToDebug)
        {
            logConfig.WriteTo.Debug();
        }

        serviceContainer.Register<AuroraModularis.Logging.Models.ILogger>(new LoggerImpl(logConfig.CreateLogger()));
    }
}
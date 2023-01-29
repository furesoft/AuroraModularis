using AuroraModularis.Core;
using Serilog;

namespace AuroraModularis.Modules.Logging.SeriLog;

[Priority]
public class Module : AuroraModularis.Module
{
    public override Task OnStart(ServiceContainer serviceContainer)
    {
        return Task.CompletedTask;
    }

    public override void OnInit()
    {
        UseSettings = true;
        Settings = new SettingsModel();
    }

    public override void RegisterServices(ServiceContainer serviceContainer)
    {
        var logConfig = new LoggerConfiguration()
        .WriteTo.Console();

        var settings = (SettingsModel)Settings;

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
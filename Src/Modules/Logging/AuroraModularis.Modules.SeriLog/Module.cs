using Serilog;

namespace AuroraModularis.Modules.Logging.SeriLog;

[Priority]
public class Module : AuroraModularis.Module
{
    public override string Name => "SeriLog";

    public override Task OnStart()
    {
        return Task.CompletedTask;
    }

    public override void OnInit()
    {
        UseSettings = true;
        Settings = new SettingsModel();
    }

    public override void RegisterServices(TinyIoCContainer container)
    {
        var logConfig = new LoggerConfiguration()
        .WriteTo.Console();
        // .WriteTo.File(Path.Combine(_pathManager.ConfigBaseDir, "log.txt"))
        var settings = (SettingsModel)Settings;
        if (settings.WriteToDebug)
        {
            logConfig.WriteTo.Debug();
        }

        container.Register<AuroraModularis.Logging.Models.ILogger>(new LoggerImpl(logConfig.CreateLogger()));
    }
}
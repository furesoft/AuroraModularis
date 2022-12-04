using AuroraModularis.Modules.PathResolver.Models;
using Serilog;

namespace AuroraModularis.Modules.Logging.SeriLog;

[Priority]
public class Module : AuroraModularis.Module
{
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

        var settings = (SettingsModel)Settings;

        if (settings.LogFile != null)
        {
            var pathResolver = container.Resolve<IPathResolver>();
            logConfig.WriteTo.File(Path.Combine(pathResolver.ConvertPath(settings.LogFile), "log.txt"));
        }

        if (settings.WriteToDebug)
        {
            logConfig.WriteTo.Debug();
        }

        container.Register<AuroraModularis.Logging.Models.ILogger>(new LoggerImpl(logConfig.CreateLogger()));
    }
}
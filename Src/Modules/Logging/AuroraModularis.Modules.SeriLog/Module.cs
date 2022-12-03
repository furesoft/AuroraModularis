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

    public override void RegisterServices(TinyIoCContainer container)
    {
        var log = new LoggerConfiguration()
        .WriteTo.Console()
        // .WriteTo.File(Path.Combine(_pathManager.ConfigBaseDir, "log.txt"))
        .CreateLogger();

        container.Register<AuroraModularis.Logging.Models.ILogger>(new LoggerImpl(log));
    }
}
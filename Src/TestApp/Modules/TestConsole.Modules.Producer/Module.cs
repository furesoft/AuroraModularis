using AuroraModularis;
using AuroraModularis.Core;
using AuroraModularis.Logging.Models;

namespace TestConsole.Modules.Producer;

public class Module : AuroraModularis.Module
{
    private readonly ILogger logger;

    public Module(ILogger logger)
    {
        this.logger = logger;
    }

    public override Task OnStart(Container container) => Task.Run(GenerateMessages);

    private async Task GenerateMessages()
    {
        while (true)
        {
            Outbox.Post("Hello");
            logger.Error("Error from module");
            await Task.Delay(1000);
        }
    }
}
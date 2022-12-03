﻿using AuroraModularis;
using AuroraModularis.Logging.Models;

namespace TestConsole.Modules.Producer;

public class Module : AuroraModularis.Module
{
    private readonly ILogger logger;

    public Module(ILogger logger)
    {
        this.logger = logger;
    }

    public override string Name => "Producer";

    public override ShortGuid ID => Guid.Parse("BA877627-C3ED-4FF9-A49F-1F6ACABBD4A9");

    public override Task OnStart() => Task.Run(GenerateMessages);

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
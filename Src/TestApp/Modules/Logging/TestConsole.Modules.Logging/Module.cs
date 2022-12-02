using AuroraModularis.Messaging;

namespace TestConsole.Modules.Logging;

public class Module : AuroraModularis.Module
{
    public override string Name => "Logging";

    public override Task OnStart()
    {
        Inbox.Subscribe<string>(_ =>
        {
            Console.WriteLine(_);
        });

        return Task.CompletedTask;
    }
}
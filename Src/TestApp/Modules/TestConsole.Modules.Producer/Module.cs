namespace TestConsole.Modules.Producer;

public class Module : AuroraModularis.Module
{
    public override string Name => "Producer";

    public override Task OnStart() => Task.Run(GenerateMessages);

    private async Task GenerateMessages()
    {
        while (true)
        {
            Outbox.Post("Hello");
            await Task.Delay(1000);
        }
    }
}
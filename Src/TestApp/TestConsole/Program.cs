using AuroraModularis;

namespace TestConsole;

internal class Program
{
    private static Task Main(string[] args)
    {
        return Bootstrapper.RunAsync(new(Environment.CurrentDirectory, "TestConsole"));
    }
}
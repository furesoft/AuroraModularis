using AuroraModularis;
using AuroraModularis.Modules.Logging.SeriLog;

namespace TestConsole;

internal class Program
{
    private static Task Main(string[] args)
    {
        return BootstrapperBuilder.StartConfigure()
            .WithAppName("TestConsole")
            .WithOptions(new SerilogOptions(){ WriteToDebug = true})
            .BuildAndStartAsync();
    }
}
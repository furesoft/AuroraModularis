using AuroraModularis;

namespace TestConsole;

internal class Program
{
    private static Task Main(string[] args)
    {
        return BootstrapperBuilder.StartConfigure()
            .WithAppName("TestConsole")
            .WithModulesBasePath(Environment.CurrentDirectory)
            .WithSettingsBasePath(".")
            .BuildAndStartAsync();
    }
}
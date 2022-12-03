using AuroraModularis;

namespace TestConsole;

internal class Program
{
    private static Task Main(string[] args)
    {
        return BootstrapperBuilder.Build()
            .WithAppName("TestConsole")
            .WithModulesBasePath(Environment.CurrentDirectory)
            .WithSettingsBasePath(".")
            .BuildAndStartAsync();
    }
}
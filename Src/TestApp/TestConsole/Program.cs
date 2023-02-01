using AuroraModularis;
using AuroraModularis.Hooks.ResourceRegistrationHook;
using Avalonia;

namespace TestConsole;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = new Application();
        
        BootstrapperBuilder.StartConfigure()
            .WithAppName("TestConsole")
            .WithModulesBasePath(Environment.CurrentDirectory)
            .WithSettingsBasePath(".")
            .AddResourceHook(app)
            .BuildAndStartAsync();
    }
}
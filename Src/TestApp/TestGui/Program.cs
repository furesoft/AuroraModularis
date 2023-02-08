using AuroraModularis;

namespace TestGui;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static Task Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        return BootstrapperBuilder.StartConfigure()
            .WithAppName("TestGui")
            .BuildAndStartAsync();
    }
}
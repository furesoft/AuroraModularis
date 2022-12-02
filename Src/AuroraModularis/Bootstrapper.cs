using Microsoft.Extensions.DependencyInjection;

namespace AuroraModularis;

public class Bootstrapper
{
    private static ModuleLoader moduleLoader = new();

    public static void Run(string modulesPath)
    {
        var services = new ServiceCollection();

        foreach (var modPath in Directory.GetFiles(modulesPath, "*.dll"))
        {
            moduleLoader.Load(modPath);
        }

        var provider = services.BuildServiceProvider();
        //ToDo: implement InBox, OutBox, MessageBroker, ...
    }
}
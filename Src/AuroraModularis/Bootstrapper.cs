using AuroraModularis.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace AuroraModularis;

public class Bootstrapper
{
    public static void Run(string modulesPath)
    {
        var services = new ServiceCollection();
        ModuleLoader moduleLoader = new();

        services.AddTransient(_ => moduleLoader);

        var messageBroker = new MessageBroker();
        foreach (var modPath in Directory.GetFiles(modulesPath, "*.dll"))
        {
            moduleLoader.Load(modPath, messageBroker);
        }

        var provider = services.BuildServiceProvider();
        //ToDo: implement InBox, OutBox, MessageBroker, ...
    }
}
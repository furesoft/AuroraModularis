using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuroraModularis;

public class Bootstrapper
{
    private static ModuleLoader moduleLoader = new();

    public static void Run(string modulesPath)
    {
        var services = new ServiceCollection();

        var assemblies = new List<Assembly>();
        foreach (var modPath in Directory.GetFiles(modulesPath, "*.dll"))
        {
            var module = moduleLoader.Load(modPath);

            assemblies.Add(module.GetType().Assembly);
        }

        assemblies.Add(Assembly.GetExecutingAssembly());

        services.AddMediatR(assemblies.ToArray());

        var provider = services.BuildServiceProvider();
        //ToDo: implement InBox, OutBox, MessageBroker, ...
    }
}
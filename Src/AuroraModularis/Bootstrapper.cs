using AuroraModularis.Messaging;

namespace AuroraModularis;

public class Bootstrapper
{
    public static async Task RunAsync(string modulesPath)
    {
        ModuleLoader moduleLoader = new();

        var messageBroker = new MessageBroker();
        messageBroker.Start();

        foreach (var modPath in Directory.GetFiles(modulesPath, "*.dll"))
        {
            moduleLoader.Load(modPath, messageBroker);
        }

        TinyIoCContainer.Current.AutoRegister();
        moduleLoader.Init(TinyIoCContainer.Current);

        foreach (var module in moduleLoader.Modules)
        {
            await module.OnStart();
        }
    }
}
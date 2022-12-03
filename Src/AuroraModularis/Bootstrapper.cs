using AuroraModularis.Messaging;

namespace AuroraModularis;

public class Bootstrapper
{
    public static async Task RunAsync(string modulesPath)
    {
        ModuleLoader moduleLoader = new();

        var messageBroker = new MessageBroker();
        messageBroker.Start();

        TinyIoCContainer.Current.AutoRegister();

        moduleLoader.Load(".", messageBroker);

        Task.WaitAll(moduleLoader.Modules.Select(_ => _.OnStart()).ToArray());
    }
}
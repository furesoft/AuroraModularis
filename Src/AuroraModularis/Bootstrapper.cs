using AuroraModularis.Messaging;

namespace AuroraModularis;

internal class Bootstrapper
{
    public static async Task RunAsync(ModularConfiguration config)
    {
        ModuleLoader moduleLoader = new();

        var messageBroker = new MessageBroker();
        messageBroker.Start();

        TinyIoCContainer.Current.AutoRegister();

        moduleLoader.Load(config, messageBroker);

        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            foreach (var module in moduleLoader.Modules)
            {
                module.OnExit();
            }
        };

        Task.WaitAll(moduleLoader.Modules.Select(_ => _.OnStart()).ToArray());
    }
}
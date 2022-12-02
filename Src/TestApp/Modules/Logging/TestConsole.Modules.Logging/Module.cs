using AuroraModularis.Messaging;
using Microsoft.Extensions.DependencyInjection;
using TestModule.Models;

namespace TestConsole.Modules.Logging
{
    public class Module : AuroraModularis.Module
    {
        public override string Name => "Logging";

        public override void Init(IServiceCollection services)
        {
            Inbox.Subscribe<LoggingMessage>(_ =>
            {
                Console.WriteLine(_.Message);
            });
        }
    }
}
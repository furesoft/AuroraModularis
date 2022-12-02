using AuroraModularis;
using Microsoft.Extensions.DependencyInjection;

namespace TestConsole.Modules.Logging
{
    public class Module : IModule
    {
        public Guid ID => Guid.NewGuid();

        public string Name => "Logging";

        public void Init(IServiceCollection services)
        {
            services.AddLogging();
        }

        public void OnLoad()
        {
        }

        public void OnUnload()
        {
        }
    }
}
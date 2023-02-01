using AuroraModularis.Core;
using Avalonia;

namespace AuroraModularis.Hooks.ResourceRegistrationHook;

public static class BootstrapBuilderExtensions
{
    public static IBootstrapBuilder AddResourceHook(this IBootstrapBuilder builder, Application app)
    {
        var hook = new ModuleLoadingHook(app);
        builder.Configuration.Hooks.Register(hook);

        return builder;
    }
}
using AuroraModularis.Core;

namespace AuroraModularis.Hooks.ResourceRegistrationHook;

public static class BootstrapBuilderExtensions
{
    public static IBootstrapBuilder AddResourceHook(IBootstrapBuilder builder)
    {
        return builder.WithHook<ModuleLoadingHook>();
    }
}
using AuroraModularis.Core;

namespace AuroraModularis.Hooks.ResourceRegistrationHook;

public static class BootstrapBuilderExtensions
{
    public static IBootstrapBuilder AddResourceHook(this IBootstrapBuilder builder)
    {
        return builder.WithHook<ModuleLoadingHook>();
    }
}
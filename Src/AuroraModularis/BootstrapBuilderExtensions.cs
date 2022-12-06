using AuroraModularis.Core;

namespace AuroraModularis;

public static class BootstrapBuilderExtensions
{
    public static IBootstrapBuilder WithAppName(this IBootstrapBuilder builder, string modulesPath)
    {
        builder.Configuration.ApplicationName = modulesPath;

        return builder;
    }

    public static IBootstrapBuilder WithModulesBasePath(this IBootstrapBuilder builder, string modulesBasePath)
    {
        builder.Configuration.ModulesPath = modulesBasePath;

        return builder;
    }

    public static IBootstrapBuilder WithSettingsBasePath(this IBootstrapBuilder builder, string settingsBasePath)
    {
        builder.Configuration.SettingsBasePath = settingsBasePath;

        return builder;
    }

    public static IBootstrapBuilder WithSettingsProvider<T>(this IBootstrapBuilder builder)
        where T : IModuleSettingsProvider, new()
    {
        builder.Configuration.SettingsProvider = new T();

        return builder;
    }

    public static IBootstrapBuilder WithHook<T>(this IBootstrapBuilder builder)
        where T : IModuleHook, new()
    {
        builder.Configuration.Hooks.Register<T>();

        return builder;
    }
}
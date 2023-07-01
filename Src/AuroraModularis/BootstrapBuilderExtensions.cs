using AuroraModularis.Core;

namespace AuroraModularis;

public static class BootstrapBuilderExtensions
{
    public static IBootstrapBuilder WithAppName(this IBootstrapBuilder builder, string modulesPath)
    {
        builder.Configuration.ApplicationName = modulesPath;

        return builder;
    }

    public static IBootstrapBuilder WithModuleLoader(this IBootstrapBuilder builder, IModuleLoader loader)
    {
        builder.Configuration.Loader = loader;

        return builder;
    }

    public static IBootstrapBuilder WithOptions<T>(this IBootstrapBuilder builder, T options)
    {
        ServiceContainer.Current.Register(options).AsSingleton();

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
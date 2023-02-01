using AuroraModularis.Core;

namespace AuroraModularis.Settings.LiteDb;

public static class BootstrapBuilderExtensions
{
    public static IBootstrapBuilder UseLiteDb(this IBootstrapBuilder builder)
    {
        var provider = new LiteDbSettingsProvider(builder);

        builder.Configuration.SettingsProvider = provider;
        
        return builder;
    }
}
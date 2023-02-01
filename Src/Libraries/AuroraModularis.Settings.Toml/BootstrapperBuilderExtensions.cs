using AuroraModularis.Core;

namespace AuroraModularis.Settings.Toml;

public static class BootstrapperBuilderExtensions
{
    public static IBootstrapBuilder UseToml(this IBootstrapBuilder builder)
    {
        return builder.WithSettingsProvider<TomlSettingsProvider>();
    }
}
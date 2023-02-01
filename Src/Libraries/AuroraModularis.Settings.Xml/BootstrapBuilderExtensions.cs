using AuroraModularis.Core;

namespace AuroraModularis.Settings.Xml;

public static class BootstrapBuilderExtensions
{
    public static IBootstrapBuilder UseXml(this IBootstrapBuilder builder)
    {
        return builder.WithSettingsProvider<XmlSettingsProvider>();
    }
}
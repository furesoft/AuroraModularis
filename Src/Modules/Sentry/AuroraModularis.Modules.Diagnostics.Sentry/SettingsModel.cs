namespace AuroraModularis.Modules.Diagnostics.Sentry;

internal class SettingsModel
{
    public string Environment { get; set; } = "Debug";
    public bool Debug { get; set; } = true;
    public double TracesSampleRate { get; set; } = 1.0;
    public string DSN { get; set; }
}

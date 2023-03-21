using AuroraModularis.Core.Hooks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace AuroraModularis.Hooks.ResourceRegistrationHook;

public class ModuleLoadingHook : IModuleLoadingHook
{
    private readonly Application _app;

    public ModuleLoadingHook(Application app)
    {
        _app = app;
    }

    public bool ShouldLoadModule(AuroraModularis.Module module)
    {
        return true;
    }

    public void BeforeLoadModule(Type moduleType)
    {
        RegisterIconsFrom(moduleType);
        RegisterDataTemplates(moduleType);
        RegisterStylesFrom(moduleType);
    }

    public void AfterLoadModule(AuroraModularis.Module module)
    {
    }

    private void RegisterIconsFrom(Type type)
    {
        var resDictionary = GetFromResource<ResourceDictionary>(type, "Icons");

        if (resDictionary is null) return;

        _app.Resources.MergedDictionaries.Add(resDictionary);
    }

    private void RegisterStylesFrom(Type type)
    {
        var styles = GetFromResource<Styles>(type, "Styles");

        if (styles is null) return;
        
        _app.Styles.AddRange(styles);
    }

    private void RegisterDataTemplates(Type type)
    {
        var dataTemplates = GetFromResource<DataTemplates>(type, "DataTemplates");
        if (dataTemplates is null) return;

        _app.DataTemplates.AddRange(dataTemplates);
    }

    private static T? GetFromResource<T>(Type type, string name)
    {
        var stream = type.Assembly.GetManifestResourceStream($"{type.Assembly.GetName().Name}.Resources.{name}.xml");
        if (stream is null) return default;

        return (T?)AvaloniaRuntimeXamlLoader.Load(stream, type.Assembly);
    }
}

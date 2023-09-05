using System.Reflection;
using Newtonsoft.Json;

namespace AuroraModularis.Modules.I18N.Models;

public class ResourcesLocalisationService : ILocalisationService
{
    private Dictionary<string, string> _localization;

    public ResourcesLocalisationService(Assembly resourceAssembly, string resourcePath)
    {
        var localeNames = resourceAssembly
                                    .GetManifestResourceNames()
                                    .Select(_ => _.Replace(resourcePath, ""))
                                    .Select(Path.GetFileNameWithoutExtension);

        var resourceName = localeNames.FirstOrDefault(_ => _ == Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName) ?? "en";

        //ToDo: does not work for other assemblies
        var strm = GetType().Assembly.GetManifestResourceStream($"{resourcePath}.{resourceName}.json");
        var json = new StreamReader(strm!).ReadToEnd();

        _localization = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)!;
    }

    public string GetString(string key)
    {
        return _localization.TryGetValue(key, out var value) ? value : key;
    }
}

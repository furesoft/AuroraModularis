using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Build.Utilities;

namespace AuroraModularis.I18N.MsBuild;

public class SearchStringsTask : Task
{
    [Required]
    public string DirectoryToSearch { get; set; }
    
    private static string GetStringsPath(string directoryToSearch)
    {
        return Directory.GetFiles(directoryToSearch, "strings.json", SearchOption.AllDirectories).First();
    }

    public override bool Execute()
    {
        var stringsPath = GetStringsPath(DirectoryToSearch);
        var resultObj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(stringsPath));

        var csPattern = "(GetString|GetStringFormat|WithLocalizedMessage)\\(\"(?<key>[^\"]+)\"\\)";
        var axamlPattern = @"Localisation '(?<key>[^}]+)'\}";

        var regxp = new Regex($"{csPattern}|{axamlPattern}");
        var csFiles = Directory.GetFiles(DirectoryToSearch, "*.cs", SearchOption.AllDirectories);
        var axamlFiles = Directory.GetFiles(DirectoryToSearch, "*.axaml", SearchOption.AllDirectories);

        var files = csFiles.Concat(axamlFiles);

        foreach (var file in files)
        {
            var matches = regxp.Matches(File.ReadAllText(file));

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    var key = match.Groups["key"].Value;
                    resultObj.TryAdd(key, key);
                }
            }
        }

        var json = JsonConvert.SerializeObject(resultObj, Formatting.Indented);

        File.WriteAllText(stringsPath, json);

        return true;
    }
}
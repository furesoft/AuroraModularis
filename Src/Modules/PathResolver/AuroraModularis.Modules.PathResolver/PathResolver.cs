using AuroraModularis.Modules.PathResolver.Models;

namespace AuroraModularis.Modules.PathResolver;

public class PathResolver : IPathResolver
{
    private Dictionary<string, string> paths = new();

    public string ConvertPath(string filename)
    {
        foreach (var path in paths)
        {
            filename = filename.Replace("{" + path.Key + "}", path.Value);
        }

        return filename;
    }

    public void Map(string identifier, string path)
    {
        paths.Add(identifier, path);
    }

    public string Resolve(string identifier)
    {
        return paths[identifier];
    }
}
using AuroraModularis.Modules.PathResolver.Models;

namespace AuroraModularis.Modules.PathResolver;

public class PathResolver : IPathResolver
{
    private Dictionary<string, string> paths = new();

    public void Map(string identifier, string path)
    {
        paths.Add(identifier, path);
    }

    public string Resolve(string identifier)
    {
        return paths[identifier];
    }
}
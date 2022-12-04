namespace AuroraModularis.Modules.PathResolver.Models;

public interface IPathResolver
{
    void Map(string identifier, string path);

    string Resolve(string identifier);
}
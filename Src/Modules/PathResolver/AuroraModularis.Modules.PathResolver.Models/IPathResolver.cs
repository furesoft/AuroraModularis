namespace AuroraModularis.Modules.PathResolver.Models;

public interface IPathResolver
{
    string ConvertPath(string logFile);
    void Map(string identifier, string path);

    string Resolve(string identifier);
}
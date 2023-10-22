namespace AuroraModularis.Logging.Models;

public interface ILogger
{
    void Error(string message);

    void Info(string message);

    void Debug(string message);
}
using AuroraModularis.Logging.Models;
using Serilog.Core;

namespace AuroraModularis.Modules.Logging.SeriLog;

internal class LoggerImpl : ILogger
{
    private readonly Logger _logger;

    public LoggerImpl(Logger logger)
    {
        _logger = logger;
    }

    public void Error(string message)
    {
        _logger.Error(message);
    }

    public void Info(string message)
    {
        _logger.Information(message);
    }

    public void Debug(string message)
    {
        _logger.Debug(message);
    }
}
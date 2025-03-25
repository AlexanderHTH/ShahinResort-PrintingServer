using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.Logging;

public class TQLogger<T>(ILogger<T> logger, IHostEnvironment hostEnvironment) : ITQLogger<T> where T : class
{
    private bool IsDevelopment() => hostEnvironment.IsDevelopment();//|| hostEnvironment.IsStaging();
    public void LogInformation(string message, params object[] args)
    {
        if (IsDevelopment()) logger.LogInformation(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        if (IsDevelopment()) logger.LogWarning(message, args);
    }

    public void LogError(string message, params object[] args)
    {
        if (IsDevelopment()) logger.LogError(message, args);
    }

    public void LogError(Exception ex, string message)
    {
        if (IsDevelopment()) logger.LogError(ex, message);
    }

    public void LogDebug(string message, params object[] args)
    {
        if (IsDevelopment()) logger.LogDebug(message, args);
    }

    public void LogCritical(string message, params object[] args)
    {
        if (IsDevelopment()) logger.LogCritical(message, args);
    }
}

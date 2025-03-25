namespace Microsoft.Extensions.Logging;

public interface ITQLogger<T> where T : class
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(string message, params object[] args);
    void LogError(Exception ex, string message);
    void LogDebug(string message, params object[] args);
    void LogCritical(string message, params object[] args);
}

namespace Base.Broker.Loggings
{
    public interface ILoggingBroker
    {
        void LogCritical(Exception exception);
        void LogDebug(string message);
        void LogError(Exception exception);
        void LogInformation(string message);
        void LogTrace(string message);
        void LogWarning(string message);
    }
}
namespace LogAn
{
    public interface ILogger
    {
        void LogError(string msg, int level, string location);
    }
}

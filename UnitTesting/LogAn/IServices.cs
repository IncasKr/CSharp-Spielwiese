namespace LogAn
{
    public interface IWebService
    {
        void LogError(string message);
        void LogError(TraceMessage message);
    }

    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}

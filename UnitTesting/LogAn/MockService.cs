namespace LogAn
{
    public class MockService : IWebService
    {
        public string LastError;

        public void LogError(string message)
        {
            LastError = message;
        }
        
        public void LogError(TraceMessage message)
        {
            LogError(message.Message);
        }
    }
}

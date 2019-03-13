using System;

namespace LogAn
{
    public class StubService : IWebService
    {
        public Exception ToThrow;

        public void LogError(string message)
        {
            if (ToThrow != null)
            {
                throw ToThrow;
            }
        }

        public void LogError(TraceMessage message)
        {
            LogError(message.Message);
        }

        public void LogInfo(string message)
        {

        }
    }
}

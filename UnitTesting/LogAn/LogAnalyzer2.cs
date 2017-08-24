using System;

namespace LogAn
{
    public class LogAnalyzer2
    {
        private IWebService _service;

        private IEmailService _email;

        public IWebService Service
        {
            get { return _service; }
            set { _service = value; }
        }

        public IEmailService Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public LogAnalyzer2():this(null, null){ }

        public LogAnalyzer2(IWebService service, IEmailService email = null)
        {
            _service = service;
            _email = email;
        }
        
        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                try
                {
                    _service.LogError($"Filename too short:{fileName}");
                }
                catch (Exception e)
                {
                    _email.SendEmail("a", "subject", e.Message);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public static class LoggingFacility
    {
        private static ILogger logger;
        public static ILogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        public static void Log(string text)
        {
            logger.Log(text);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern.Tests.Integration
{
    internal class StubLogger : ILogger
    {
        public void Log(string text)
        {
            //do nothing
        }
    }
}

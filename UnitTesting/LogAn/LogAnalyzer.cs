using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string filename)
        {
            return filename.ToLower().EndsWith(".slf");
        }
    }
}

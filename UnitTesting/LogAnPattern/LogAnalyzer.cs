using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public class LogAnalyzer
    {
        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                LoggingFacility.Log("Filename too short:" + fileName);
            }
            if (fileName.Equals("myemptyfile.txt"))
            {
                throw new TypeLoadException($"The file '{fileName}' is empty!");
            }
        }
    }
}

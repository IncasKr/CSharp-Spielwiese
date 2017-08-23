using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class LogAnalyzerUsingFactoryMethod
    {
        public bool IsValidLogFileName(string fileName)
        {
            return GetManager().IsValid(fileName);
        }
        protected virtual IExtensionManager GetManager()
        {
            return new FileExtensionManager();
        }
    }

    public class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
    {
        public IExtensionManager Manager;
        protected override IExtensionManager GetManager()
        {
            return Manager;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class LogAnalyzer
    {
        private bool _wasLastFileNameValid;

        public bool WasLastFileNameValid
        {
            get { return _wasLastFileNameValid; }
            set { _wasLastFileNameValid = value; }
        }

        public bool IsValidLogFileName(string fileName)
        {
            IExtensionManager mgr = new FileExtensionManager();
            _wasLastFileNameValid = mgr.IsValid(fileName);
            return _wasLastFileNameValid;
        }
    }
}

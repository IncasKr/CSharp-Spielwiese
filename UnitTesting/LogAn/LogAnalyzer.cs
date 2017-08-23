using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class LogAnalyzer
    {
        private IExtensionManager _manager;

        private bool _wasLastFileNameValid;

        public bool WasLastFileNameValid
        {
            get { return _wasLastFileNameValid; }
            set { _wasLastFileNameValid = value; }
        }

        public LogAnalyzer()
        {
            _manager = new FileExtensionManager();
        }

        public LogAnalyzer(IExtensionManager mgr)
        {
            _manager = mgr;
        }

        public bool IsValidLogFileName(string fileName)
        {
            _wasLastFileNameValid = _manager.IsValid(fileName);
            return _wasLastFileNameValid;
        }
    }
}

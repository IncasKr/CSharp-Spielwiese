using System.IO;

namespace LogAn
{
    public class LogAnalyzer
    {
        private IExtensionManager _manager;

        private bool _wasLastFileNameValid;

        public IExtensionManager ExtensionManager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        public bool WasLastFileNameValid
        {
            get { return _wasLastFileNameValid; }
            set { _wasLastFileNameValid = value; }
        }

        public LogAnalyzer()
        {
            _manager = ExtensionManagerFactory.Create();
        }
        
        public bool IsValidLogFileName(string fileName)
        {
            _wasLastFileNameValid = _manager.IsValid(fileName) && Path.GetFileNameWithoutExtension(fileName).Length > 5;
            return _wasLastFileNameValid;
        }
    }
}

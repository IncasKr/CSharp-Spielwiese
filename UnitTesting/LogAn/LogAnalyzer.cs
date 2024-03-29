﻿using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LogAnTests")]
namespace LogAn
{
    public class LogAnalyzer
    {
        private IExtensionManager _manager;
        
        private bool _wasLastFileNameValid;

        internal IExtensionManager ExtensionManager
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

        /// <summary>
        /// Constructor that visible only for the test LogAnTests
        /// </summary>
        /// <param name="extentionMgr">extention manager (e.g Fake)</param>
        internal LogAnalyzer(IExtensionManager extentionMgr)
        {
            _manager = extentionMgr;
        }

        public bool IsValidLogFileName(string fileName)
        {
            _wasLastFileNameValid = _manager.IsValid(fileName) && Path.GetFileNameWithoutExtension(fileName).Length > 5;
            return _wasLastFileNameValid;
        }
    }
}

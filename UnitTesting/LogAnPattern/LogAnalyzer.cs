using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public class LogAnalyzer
    {
        private string _nameToTest = "myemptyfile.txt";

        private FileData _file;

        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                LoggingFacility.Log($"Filename too short: {fileName}");
            }
            if (fileName.Equals(_nameToTest))
            {
                throw new TypeLoadException($"The file '{fileName}' is empty!");
            }
            _nameToTest = fileName;
        }

        public void Initialize()
        {
            _file = new FileData();
            _file.FileName = _nameToTest;
            _nameToTest = "myemptyfile.txt";
        }

        public bool IsValid(string fileName)
        {
            if (_file == null)
            {
                throw new NotInitializedException($"The LogAnalyzer.Initialize() method should be called before any other operation!");
            }
            return _file.FileName.ToLower().EndsWith(".txt") && _file.Load(fileName);
        }       
    }

    public class FileData
    {
        public string Data { get; private set; }

        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    Data = string.Empty;
                }               
            }
        }
        
        public FileData()
        {
            _fileName = string.Empty;
            Data = string.Empty;
        }

        public bool Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            FileName = fileName;
            return true;
        }
    }
}

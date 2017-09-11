using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LogAnPattern
{
    public class AnalyzedOutput
    {
        public int LineCount
        {
            get { return Lines.Count; }
        }

        private List<string> Lines;

        public AnalyzedOutput(List<string> list = null)
        {
            if (list != null)
            {
                Lines = list;
            }
            else
            {
                Lines = new List<string>();
            }            
        }

        public void AddLine(string time, string state, string name)
        {
            Lines.Add($"{time}\t{state}\t{name}");
        }

        public override bool Equals(object obj)
        {
            var item = obj as AnalyzedOutput;
            if (item == null || !this.LineCount.Equals(item.LineCount))
            {
                return false;
            }
            foreach (var line in this.Lines)
            {
                if (!item.Lines.Contains(line))
                {
                    return false;
                }
            }
            return true;
        }

        public string[] GetLine(int position)
        {
            if (position - 1 < 0 || position - 1 >= LineCount)
            {
                return null;
            }
            return Lines[position - 1].Split('\t');
        }
    }
    public class LogAnalyzer
    {
        private string _nameToTest = "myemptyfile.txt";

        private FileData _file;

        public AnalyzedOutput Analyze(string fileName)
        {
            var validCount = fileName != null ? fileName.Split('\t').Length : 0;
            if (validCount.Equals(3))
            {
                return new AnalyzedOutput(new List<string>{ fileName });
            }
            else
            {
                if (fileName.Length < 8)
                {
                    LoggingFacility.Log($"Filename too short: {fileName}");
                }
                if (fileName.Equals(_nameToTest))
                {
                    throw new TypeLoadException($"The file '{fileName}' is empty!");
                }
                _file.FileName = fileName;
                return new AnalyzedOutput();
            }            
        }

        public void Initialize()
        {
            _file = new FileData();
            _nameToTest = "myemptyfile.txt";            
        }

        public bool IsValid(FileInfo fileObj)
        {
            if (!fileObj.Exists)
            {
                return false;
            }
            return IsValid(fileObj.Name);
        }

        public bool IsValid(string fileName)
        {
            if (_file == null)
            {
                throw new NotInitializedException($"The LogAnalyzer.Initialize() method should be called before any other operation!");
            }
            Analyze(fileName);
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

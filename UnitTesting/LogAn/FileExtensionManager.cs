using System;

namespace LogAn
{
    public class FileExtensionManager : IExtensionManager
    {
        public bool IsValid(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("No filename provided!");
            }
            return fileName.ToLower().EndsWith(".slf");
        }
    }
}

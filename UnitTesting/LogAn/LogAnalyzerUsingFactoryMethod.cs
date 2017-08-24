namespace LogAn
{
    public class LogAnalyzerUsingFactoryMethod
    {
        public bool IsValidLogFileName(string fileName)
        {
            if (GetManager() != null)
            {
                return GetManager().IsValid(fileName);
            }
            int len = fileName.Length;
            return this.IsValid(fileName) && len > 5;
        }

        protected virtual bool IsValid(string fileName)
        {
            FileExtensionManager mgr = new FileExtensionManager();
            return mgr.IsValid(fileName);
        }

        protected virtual IExtensionManager GetManager()
        {
            return new FileExtensionManager();
        }
    }

    public class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
    {
        public IExtensionManager Manager;

        public bool IsSupported;

        protected override IExtensionManager GetManager()
        {
            return Manager;
        }

        protected override bool IsValid(string fileName)
        {
            return IsSupported;
        }
    }
}

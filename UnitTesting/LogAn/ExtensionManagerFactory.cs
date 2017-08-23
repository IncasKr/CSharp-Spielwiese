namespace LogAn
{
    public class ExtensionManagerFactory
    {
        static private IExtensionManager customManager=null;

        static public IExtensionManager Create()
        {
            if (customManager != null)
            {
                return customManager;
            }
            return new FileExtensionManager();
        }

        static public void SetManager(IExtensionManager mgr)
        {
            customManager = mgr;
        }
    }
}

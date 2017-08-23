using System.Diagnostics;

namespace LogAn
{
    public class ExtensionManagerFactory
    {
        static private IExtensionManager customManager=null;

        static public IExtensionManager Create()
        {
            #if DEBUG
            if (customManager != null)
            {
                return customManager;
            }
            #endif
            return new FileExtensionManager();
        }

        [Conditional("DEBUG")]
        static public void SetManager(IExtensionManager mgr)
        {
            customManager = mgr;
        }
    }
}

using System;
using System.Security.Principal;
using System.Text;

namespace UserInfos
{
    class Program
    {
        static void Main(string[] args)
        {
            WindowsIdentity currentAccount = WindowsIdentity.GetCurrent();
            string accountToken1 = currentAccount.Token.ToString();
            Console.WriteLine($"User name: {currentAccount.Name} | Label: {currentAccount.Label} | token: {accountToken1}");
            Console.WriteLine($"\tGroups:");
            foreach (var group in currentAccount.Groups)
                Console.WriteLine($"\t\t{group.Translate(typeof(NTAccount)).ToString()}");
            Console.WriteLine($"\tImpersonation level: {currentAccount.ImpersonationLevel} | Is authenticated: {currentAccount.IsAuthenticated} | Authenticate type: {currentAccount.AuthenticationType}");
            Console.WriteLine($"\tAccount type ==> System: {currentAccount.IsSystem} | Guest: {currentAccount.IsGuest} | Anonymous: {currentAccount.IsAnonymous}");


            Console.ReadLine();
        }
    }
}

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading.Tasks;

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
            Console.Write($"Please enter the password of current user:");
            string pwd = Console.ReadLine();
            if (LdapHelper.User(pwd))
            {
                Console.WriteLine("User authenticated");
                LdapHelper.fnImp();
            }
            else
            {
                Console.WriteLine("User not authenticated!");
            }
            

           Console.ReadLine();
        }

        private static string GetTokenFromAD(string clientId, string appKey)
        {
            string tenantName = "incas.com";
            string authString = "https://login.microsoftonline.com/" + tenantName;
            AuthenticationContext authenticationContext = new AuthenticationContext(authString, false);
            // Config for OAuth client credentials             
            ClientCredential clientCred = new ClientCredential(clientId, appKey);
            string resource = "https://graph.windows.net";
            string token;
            try
            {
                IEnumerable<TokenCacheItem> adList = authenticationContext.TokenCache.ReadItems();
                Task<AuthenticationResult> authenticationResult = authenticationContext.AcquireTokenAsync(resource, clientCred);
                token = authenticationResult.Result.AccessToken;
                return token;
            }
            catch (AuthenticationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Acquiring a token failed with the following error: {0}", ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Error detail: {0}", ex.InnerException.Message);
                }
                return null;
            }    
        }
    }
}

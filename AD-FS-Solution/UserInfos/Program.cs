using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading.Tasks;

namespace UserInfos
{
    class Program
    {
        private static string GetInputPassword(char passwordCharShowed = '*')
        {
            Queue<char> queue = new Queue<char>();
            ConsoleKeyInfo consoleKeyInfo;
            string password = string.Empty;
            
            // push until the enter key is pressed
            while ((consoleKeyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (consoleKeyInfo.Key != ConsoleKey.Backspace)
                {
                    queue.Enqueue(consoleKeyInfo.KeyChar);
                    Console.Write(passwordCharShowed);                    
                }
                else
                {
                    if (queue.Count > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        queue.Dequeue();
                    }
                }
            }

            if (consoleKeyInfo.Key == ConsoleKey.Enter)
            {
                Console.Write(consoleKeyInfo.KeyChar);
            }
            
            return new string(queue.ToArray());
        }

        static void Main(string[] args)
        {
            string user = Environment.UserName;
            Console.Write("Please geven the password:");
            string password = GetInputPassword();

            WindowsIdentity currentAccount = WindowsIdentity.GetCurrent();
            Console.WriteLine($"User name: {currentAccount.Name} | Label: {currentAccount.Label} | AD token: {currentAccount.Token.ToString()}");
            Console.WriteLine($"\tGroups:");
            foreach (var group in currentAccount.Groups)
            {
                string groupName = group.Translate(typeof(NTAccount)).ToString();
                if (groupName.EndsWith(Environment.UserDomainName))
                {
                    Console.WriteLine($"\t\t{groupName}");
                }
            }
            Console.WriteLine($"\tImpersonation level: {currentAccount.ImpersonationLevel} | Is authenticated: {currentAccount.IsAuthenticated} | Authenticate type: {currentAccount.AuthenticationType}");
            Console.WriteLine($"\tAccount type ==> System: {currentAccount.IsSystem} | Guest: {currentAccount.IsGuest} | Anonymous: {currentAccount.IsAnonymous}");
            
            Console.WriteLine($"{user} authentication with DirectoryEntry method: {LdapHelper.AuthenticatedWithDirectoryEntry("LDAP://incas.com", user, password)}");

            Console.WriteLine($"{user} authentication with LdapConnection method: {LdapHelper.AuthenticatedWithLdapConnection(password)}");
            Console.WriteLine("\nUser info details:");
            LdapHelper.GetCurrentUserAccountInfo();
            
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

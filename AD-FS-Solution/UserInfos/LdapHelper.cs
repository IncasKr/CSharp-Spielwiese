using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.IdentityModel.Tokens;
using System.Net;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace UserInfos
{
    public class LdapHelper
    {
        private const int ACCOUNTDISABLE = 0x0002;
        private const int NORMAL_ACCOUNT = 0x0200;

        #region Token parameters

        private const string ALGORITHM_NAME = "HmacSHA256";
        private const string SALT = "ZyCwQjKQuK9OLsmNZIL7"; // Generated at https://www.random.org/strings
        private const int TOKEN_EXPIRATION_SECONDS = 10;

        #endregion

        #region Token Methods

        /// <summary>
        /// Generates a token for an agent with a specified IP address.
        /// </summary>
        /// <param name="username">The username access.</param>
        /// <param name="password">The password access.</param>
        /// <param name="ipAddress">The IP address for the agent.</param>
        /// <param name="agent">The agent name.</param>
        /// <param name="ticks">The ticks that the token will be created.</param>
        /// <returns>The created token.</returns>
        public static string GenerateToken(string username, string password, string ipAddress, string agent, long ticks)
        {
            string hash = string.Join(":", new string[] { username, ipAddress, agent, ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";

            using (HMAC hmac = HMACSHA256.Create(ALGORITHM_NAME))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));

                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { username, ticks.ToString() });
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
        }

        /// <summary>
        /// Encrypts a given password.
        /// </summary>
        /// <param name="password">The password in plaintext.</param>
        /// <returns>The password encrypted.</returns>
        private static string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, SALT });

            using (HMAC hmac = HMACSHA256.Create(ALGORITHM_NAME))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(SALT);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));

                return Convert.ToBase64String(hmac.Hash);
            }
        }

        /// <summary>
        /// Checks the validity of a token for an agent with a specified IP address.
        /// </summary>
        /// <param name="token">The token to verify.</param>
        /// <param name="ipAddress">The address for the agent.</param>
        /// <param name="agent">The agent name.</param>
        /// <returns>Returns true if the token is validated, otherwise false.</returns>
        public static bool IsTokenValid(string token, string ipAddress, string agent)
        {
            bool validated = false;

            try
            {
                // Base64 decode the string, obtaining the token:username:timeStamp.
                string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                
                // Split the parts.
                string[] parts = key.Split(new char[] { ':' });

                if (parts.Length == 3)
                {
                    // Get the hash message, username, and timestamp.
                    string hash = parts[0];
                    string username = parts[1];
                    long ticks = long.Parse(parts[2]);
                    DateTime timeStamp = new DateTime(ticks);

                    // Ensure the timestamp is valid.
                    bool expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalSeconds) > TOKEN_EXPIRATION_SECONDS;
                    if (!expired)
                    {
                        //
                        // Lookup the user's account from the db.
                        //
                        if (username == "dummy")
                        {
                            string password = "dummy";

                            // Hash the message with the key to generate a token.
                            string computedToken = GenerateToken(username, password, ipAddress, agent, ticks);

                            // Compare the computed token with the one supplied and ensure they match.
                            validated = (token == computedToken);
                        }
                    }
                }
                else
                {
                    throw new Exception("The token format is not valid!");
                }
            }
            catch (Exception ex)
            {

            }

            return validated;
        }

        #endregion

        /// <summary>
        /// Validation for a single user to the active directory
        /// </summary>
        /// <param name="password">The password of the user</param>
        /// <returns>Returns true if the user is authenticated, otherwise false.</returns>
        public static bool AuthenticatedWithLdapConnection(string password)
        {
            bool validation;
            try
            {
                using (LdapConnection lcon = new LdapConnection(new LdapDirectoryIdentifier((string)null, false, false)))
                {
                    NetworkCredential nc = new NetworkCredential(Environment.UserName, password, Environment.UserDomainName);
                    lcon.Credential = nc;
                    lcon.AuthType = AuthType.Negotiate;
                    lcon.Bind(nc); // user has authenticated at this point, as the credentials were used to login to the dc.
                    validation = true;
                }
            }
            catch (LdapException e)
            {
                validation = false;
            }
            return validation;
        }

        /// <summary>
        /// Lists all the users from current domain
        /// </summary>
        public static void GetUsers()
        {
            DirectoryEntry directoryEntry = new DirectoryEntry("WinNT://" + Environment.UserDomainName);
            string userNames = "";
            string authenticationType = "";
            foreach (DirectoryEntry child in directoryEntry.Children)
            {
                if (child.SchemaClassName == "User")
                {
                    userNames += child.Name + Environment.NewLine; //Iterates and binds all user using a newline
                    authenticationType += child.Username + Environment.NewLine;
                }
            }
            Console.WriteLine("************************Users************************");
            Console.WriteLine(userNames);
            //Console.WriteLine("*****************Authentication Type*****************");
            //Console.WriteLine(authenticationType);
        }

        /// <summary>
        /// Gets user groups names.
        /// </summary>
        public static void GetGroupListOfUsers()
        {
            // set up domain context
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

            // find the group in question
            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, "USERS");

            // if found....
            if (group != null)
            {
                foreach (Principal p in group.GetMembers())
                {
                    Console.WriteLine("{0}: {1}", p.StructuralObjectClass, p.DisplayName);                    
                }
            }
        }
        
        /// <summary>
        /// Gets a particular details for the current user account.
        /// </summary>
        public static void GetCurrentUserAccountInfo()
        {
            using (var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        if (((string)de.Properties["sn"].Value??"").ToLower().Contains(Environment.UserName))
                        {
                            Console.WriteLine();
                            Console.WriteLine("User info details:");
                            Console.WriteLine($"\tFirst Name: {de.Properties["givenName"].Value}");
                            Console.WriteLine($"\tLast Name: {de.Properties["sn"].Value}");
                            Console.WriteLine($"\tAccount name: {de.Properties["sAMAccountName"].Value}");
                            Console.WriteLine($"\tLine: {de.Properties["telephoneNumber"].Value}");
                            Console.WriteLine($"\tMail: {de.Properties["mail"].Value}");
                            string accountStatus = Convert.ToBoolean((int)de.Properties["userAccountControl"].Value & ACCOUNTDISABLE) ? "not activ" : "activ";
                            Console.WriteLine($"\tUser account status: {accountStatus}");
                            string accountType = Convert.ToBoolean((int)de.Properties["userAccountControl"].Value & NORMAL_ACCOUNT) ? "normal account" : "other account type";
                            Console.WriteLine($"\tUser account type: {accountType}");
                            Console.WriteLine($"\tObject Category: {de.Properties["objectCategory"].Value}");
                            Console.WriteLine($"\tDistinguished Name: {de.Properties["distinguishedName"].Value}");

                            WindowsIdentity currentAccount = WindowsIdentity.GetCurrent();
                            string[] currentAccountNames = currentAccount.Name.Split('\\');
                            Console.WriteLine($"\tAccount: {currentAccountNames[currentAccountNames.Length - 1]} | Label: {currentAccount.Label} | AD token: {currentAccount.Token.ToString()}");
                            Console.WriteLine($"\tImpersonation level: {currentAccount.ImpersonationLevel} | Is authenticated: {currentAccount.IsAuthenticated} | Authenticate type: {currentAccount.AuthenticationType}");
                            Console.WriteLine($"\tAccount type ==> System: {currentAccount.IsSystem} | Guest: {currentAccount.IsGuest} | Anonymous: {currentAccount.IsAnonymous}");
                            Console.WriteLine($"\tGroups:");
                            foreach (var group in currentAccount.Groups)
                            {
                                string groupName = group.Translate(typeof(NTAccount)).ToString();
                                //if (groupName.EndsWith(Environment.UserDomainName))
                                //{
                                    Console.WriteLine($"\t\t{groupName}");
                                //}
                            }
                            
                            Console.WriteLine();
                            /*PropertyCollection pc = de.Properties;
                            foreach (PropertyValueCollection col in pc)
                            {
                                Console.WriteLine(col.PropertyName + " : " + col.Value);
                                Console.WriteLine();
                            }*/
                        }
                    }
                }
            }            
        }

        /// <summary>
        /// Authenticates an user on Active Directory.
        /// </summary>
        /// <param name="serverAD">LDAp server (in the form LDAP://domain.com).
        /// </param>
        /// <param name="agent">The username</param>
        /// <param name="password">The password of the user</param>
        /// <returns>Returns true if the user is authenticated, otherwise false.</returns>
        public static bool AuthenticatedWithDirectoryEntry(string serverAD, string agent, string password)
        {
            bool authenticated = false;

            try
            {
                DirectoryEntry entry = new DirectoryEntry(serverAD, agent, password);
                object nativeObject = entry.NativeObject;
                authenticated = true;
            }
            catch (DirectoryServicesCOMException cex)
            {
                
            }
            catch (Exception ex)
            {
                
            }
            return authenticated;
        }

        public static bool AuthenticatedWithWindowsIdentity()
        {
            WindowsIdentity currentAccount = WindowsIdentity.GetCurrent();
            return currentAccount.IsAuthenticated;
        }

        public static string GetTokenFromAD(string clientId, string appKey)
        {
            string tenantName = "incas.com";
            string authString = "https://login.microsoftonline.com/" + tenantName;
            Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authenticationContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authString, false);
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

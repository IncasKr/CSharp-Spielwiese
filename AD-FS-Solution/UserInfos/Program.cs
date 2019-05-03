using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Security.Principal;

namespace UserInfos
{
    class Program
    {
        private static string GetInputString(string charShowed = null)
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
                    Console.Write(string.IsNullOrEmpty(charShowed) ? consoleKeyInfo.KeyChar : charShowed[0]);                    
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
                Console.WriteLine(consoleKeyInfo.KeyChar);
            }
            
            return new string(queue.ToArray());
        }

        static void Main(string[] args)
        {
            /*string user = Environment.UserName;
            Console.Write("Please enter the AD user password:");
            string password = GetInputString("*");
            
            Console.WriteLine($"{user} authentication with DirectoryEntry method: {LdapHelper.AuthenticatedWithDirectoryEntry("LDAP://incas.com", user, password)}");
            Console.WriteLine($"{user} authentication with LdapConnection method: {LdapHelper.AuthenticatedWithLdapConnection(password)}");
            Console.WriteLine($"{user} authentication with WindowsIdentity method: {LdapHelper.AuthenticatedWithWindowsIdentity()}");
            
            LdapHelper.GetCurrentUserAccountInfo();
            
            Console.WriteLine($"Domains details:");
            foreach (DomainInfo item in LdapHelper.EnumerateDomainControllers())
            {
                Console.WriteLine($"\tName: {item.Name} | IP: {item.IP} | OS version: {item.OSVersion}");
            }*/
            /*
            Console.Write("Please enter the agent name to check:");
            string agentToCheck = GetInputString();
            Console.Write("Please enter the group to which the agent should be assigned:");
            string groupToCheckAgent = GetInputString();
            LdapHelper.GetUserForGroup(user, password, agentToCheck, groupToCheckAgent);
            
            //LdapHelper.GetUserForGroup(null, null, "douabalet", "intracall-test");
            string[] departmentsToLook = { "systemhaus" };
            foreach (UserInfo item in LdapHelper.GetADUsers("incas.com", departmentsToLook))
            {
                Console.WriteLine($"\tAccount name: {item.Account} | Full name: {item.FullName} | mail: {item.Email} | Line: {item.Line}");
            }
            */
            //var adServerAddress = Domain.GetDomain();
            Console.WriteLine($"##{Domain.GetCurrentDomain().Name}##");
            foreach (GroupInfo item in LdapHelper.GetADGroups("incas.com", null))
            {
                Console.WriteLine($"\tName: {item.Name} | Guid: {item.GUID}");
            }

            Console.WriteLine("\nEND");
            Console.ReadLine();
        }
    }
}

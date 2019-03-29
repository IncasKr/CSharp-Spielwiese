using System;
using System.Collections.Generic;
using System.Security.Principal;

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
            
            Console.WriteLine($"{user} authentication with DirectoryEntry method: {LdapHelper.AuthenticatedWithDirectoryEntry("LDAP://incas.com", user, password)}");
            Console.WriteLine($"{user} authentication with LdapConnection method: {LdapHelper.AuthenticatedWithLdapConnection(password)}");
            Console.WriteLine($"{user} authentication with WindowsIdentity method: {LdapHelper.AuthenticatedWithWindowsIdentity()}");

            LdapHelper.GetCurrentUserAccountInfo();

            Console.WriteLine($"Domains details:");
            foreach (DomainInfo item in LdapHelper.EnumerateDomainControllers())
            {
                Console.WriteLine($"\tName: {item.Name} | IP: {item.IP} | OS version: {item.OSVersion}");
            }
            
           Console.ReadLine();
        }
    }
}

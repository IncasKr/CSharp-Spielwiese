using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace RDPLoader
{
    class Program
    {
        private static RDPDocument RDPFile = new RDPDocument();

        private static string remoteUser = "administrator";

        private static string remotePwd = "xs4intracall";

        private static string remoteHost = "IC-Webdev";

        private static int remotePort = 3389;


        public static void StartRDP(RDPDocument info)
        {
            string strCmdText;
            strCmdText = $" {info.Filename}"; // "/noConsentPrompt /v:IC-Webdev /admin /w:420/ h:680 /public";
            Console.WriteLine("mstsc.exe" + strCmdText);
            System.Diagnostics.Process.Start("mstsc.exe", strCmdText);
        }

        private static bool IsParamVerified(string height = "0", string width = "0", string colorBit = "32")
        {
            int resInt = 0;
            return int.TryParse(height, out resInt) && int.TryParse(width, out resInt) && int.TryParse(colorBit, out resInt);
        }

        private static int ToInt(string value)
        {
            int result = 0;
            int.TryParse(value, out result);
            return result;
        }
        
        private static bool AgentExist(string user, string pwd)
        {
            return true;
        }

        private static bool isAdmin(string user, string pwd)
        {
            return (user.ToLower() == "gladis") ? true : false;
        }

        static void Main(string[] args)
        {
            if (args.Length >= 1 && args[0].Contains(":"))
            {
                string[] uriParams = args[0].Substring(args[0].IndexOf(":") + 1).Split(new string[] { "%20" }, StringSplitOptions.None);
                args = uriParams;                              
            }
            
            if (args.Length > 1 && AgentExist(args[0], args[1]))            
            {                
                switch (args.Length)
                {
                    case 3:
                        if (IsParamVerified(args[2]))
                        {
                            RDPFile = new RDPDocument(remoteUser, remotePwd, remoteHost, remotePort, isAdmin(args[0], args[1]), ToInt(args[2]));
                        }
                        else
                        {
                            RDPFile = new RDPDocument(remoteUser, remotePwd, remoteHost, remotePort, isAdmin(args[0], args[1]));
                        }
                            break;
                    case 4:
                        if (IsParamVerified(args[2], args[3]))
                        {
                            RDPFile = new RDPDocument(remoteUser, remotePwd, remoteHost, remotePort, isAdmin(args[0], args[1]), ToInt(args[2]), ToInt(args[3]));
                        }
                        else
                        {
                            RDPFile = new RDPDocument(remoteUser, remotePwd, remoteHost, remotePort, isAdmin(args[0], args[1]));
                        }
                        break;
                    case 5:
                        if (IsParamVerified(args[2], args[3], args[4]))
                        {
                            RDPFile = new RDPDocument(remoteUser, remotePwd, remoteHost, remotePort, isAdmin(args[0], args[1]), ToInt(args[2]), ToInt(args[3]), ToInt(args[4]));
                        }
                        else
                        {
                            RDPFile = new RDPDocument(remoteUser, remotePwd, remoteHost, remotePort, isAdmin(args[0], args[1]));
                        }
                        break;                    
                    default:
                        RDPFile = new RDPDocument(remoteUser, remotePwd, remoteHost, remotePort, isAdmin(args[0], args[1]));
                        break;
                }
                RDPFile.Filename = $"{args[0]}.RDP";
                Console.WriteLine(RDPFile.Filename);
                RDPFile.Save(RDPFile.Filename);
                StartRDP(RDPFile);
            }
            else
            {
                if (((args.Length == 1) && (args[0] == "")) || args.Length == 0)
                {
                    Console.WriteLine($"there is no paramater!");
                }
                else if (args.Length == 1)
                {
                    Console.WriteLine($"Password is needing for the user \"{args[0]}\"!");
                }
                else
                {
                    Console.WriteLine($"Mistake on the Agentname or password!");
                }                
            }
            //aktiviere für die Tests
            //Console.ReadLine();
        }
    }
}

using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using WebSocketSharp;

namespace websocket_sharpSecureClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ws = new WebSocket("wss://172.27.234.119:23600"))
            {
                ws.SslConfiguration.ServerCertificateValidationCallback =
                    (sender, certificate, chain, sslPolicyErrors) => {
                        // If you want to accept all certificates, write return true.
                        return certificate.Equals(new X509Certificate2(@"..\..\..\certificat\Test.pfx", "ndsoft"));                       
                        //if (sslPolicyErrors == SslPolicyErrors.None)
                        //    return true;
                        //Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
                        //// Do not allow this client to communicate with unauthenticated servers.
                        //return false;
                    };
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine("Server:> " + e.Data);
                ws.OnOpen += (sender, e) =>
                    Console.WriteLine("Welcome to Intracall Server (IC)");

                ws.Connect();
                Console.WriteLine("Give a name of firm:");
                string chaine;
                do
                {
                    chaine = Console.ReadLine();
                    ws.Send(chaine);
                } while (chaine.ToUpper() != "EXIT");
                ws.Close();
            }
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }

    }
}

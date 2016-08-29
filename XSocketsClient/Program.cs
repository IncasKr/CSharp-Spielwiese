using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XSockets.Client40;

namespace XSocketsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            XSocketClient client = new XSocketClient("ws://localhost:23600", "http://localhost", "server");
            //client.AddClientCertificate(new X509Certificate2($"..\\certificat\\Test.pfx", "ndsoft"));

            //client.Controller("chat").On("message", () => Console.WriteLine("Test Was Called"));
            

            client.OnConnected += (ws, e) =>
            {
                Console.WriteLine($"You are connected");
            };
            
            client.OnDisconnected += (ws, e) =>
            {
                Console.WriteLine($"You are disconnected");
            };

            client.OnError += (ws, e) =>
            {
                Console.WriteLine($"Connection error !");
            };

            client.Open();           
            int i = 10;
            do
            {
                Console.Write($"it writes some text. \nDisconnection in: {i} second(s)... ");
                Thread.Sleep(1000);
                i--;
                Console.Clear();
            } while (i > 0);
            Console.Clear();
            client.Disconnect();

            Console.ReadLine();
        }       
    }
}

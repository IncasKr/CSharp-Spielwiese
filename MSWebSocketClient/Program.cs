using Intracall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSWebSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WebsocketClient client = new WebsocketClient();
            client.Connect("ws://localhost:23600").Wait();

            
            Console.ReadLine();
        }
    }
}

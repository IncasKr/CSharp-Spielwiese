using System;
using WebSocketSharp;

namespace websocket_sharpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ws = new WebSocket("ws://172.27.234.119:23600"))
            {
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine(e.Data);
                ws.OnOpen += (sender, e) =>
                    Console.WriteLine("IC:> **** Welcome to Intracall Server (IC) ****");
                ws.OnClose += (sender, e) =>
                    Console.WriteLine("IC:> **** Goodbye! ****");

                Console.WriteLine("\nTape this:\n\t'exit' to exit\n\t'connect' to connect\n\t'deconnect' to deconnect\n\tOther text to send it");        
                string result = "";
                while (result != "exit")
                {
                    result = Console.ReadLine();
                    switch(result)
                    {
                        case "connect":
                            ws.Connect();
                            break;
                        case "deconnect":
                            ws.Close();
                            break;
                        case "exit":
                            ws.Close();
                            break;
                        default:
                            if(ws.IsAlive)
                                ws.Send(result.ToUpper() == "INCAS" ? result.ToUpper() : result);
                            else
                                Console.WriteLine("please first connect to server!");
                            break;
                    }
                }
            }
        }
    }
}

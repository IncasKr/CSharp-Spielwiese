using SuperSocket.ClientEngine;
using System;
using System.Text;
using WebSocket4Net;

namespace WebSocket4NetClient
{
    class Program
    {
        public static WebSocket websocket;
        static void Main(string[] args)
        {
            websocket = new WebSocket("ws://localhost:23600/", "", WebSocketVersion.Rfc6455);
            websocket.Opened += new EventHandler(websocket_Opened);
            websocket.Error += new EventHandler<ErrorEventArgs> (websocket_Error);
            websocket.Closed += new EventHandler(websocket_Closed);
            websocket.DataReceived += new EventHandler<DataReceivedEventArgs>(websocke_DataReceived);
            websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
            websocket.Open(); 

            string msg;
            do
            {
                msg = Console.ReadLine();
                if (msg.ToUpper() != "EXIT")
                {
                    websocket.Send(msg);
                    //string textTest = "Test de donnéeseztrzuwefrgtfwezfiuhgsbvhafnbhdgsdhfjebnbkjqjhuwdghbhvdhjggaknjb bhdgzfdbjsghghbqwhsgdzfdded";
                    //websocket.Send(Encoding.UTF8.GetBytes(textTest), 0, textTest.Length);
                }
                else
                {
                    websocket.Close();
                    System.Threading.Thread.Sleep(10);
                    Console.WriteLine(@"Press ""Enter"" to close the windows.");
                }                
            } while (msg.ToUpper() != "EXIT");
            Console.ReadLine();
        }        

        private static void websocket_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Welcom to Intracall server test");
        }

        private static void websocket_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Error connection! " + e.Exception);
        }

        private static void websocket_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("The server is closed or you have been disconnected from server. Goodbye!");
        }

        private static void websocke_DataReceived(object sender, DataReceivedEventArgs e)
        {
            string fullData = Encoding.UTF8.GetString(e.Data);
            int sizeDataCache = 80;
            int pos = 0;

            Console.WriteLine("DATA\n{");
            do
            {
                Console.WriteLine($"\t{Encoding.UTF8.GetString(e.Data, pos, sizeDataCache)}");
                pos += sizeDataCache;
            } while (pos < fullData.Length);
            Console.WriteLine("}");
        }

        private static void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Message: " + e.Message);            
        }
    }
}

using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intracall
{
    class WebsocketClient
    {
        /// <summary>
        /// It represents the instance of client
        /// </summary>
        private ClientWebSocket clientInstance;
        
        /// <summary>
        /// The byte size of the data block to send (and receive) to (from) server.
        /// </summary>
        public int DataBlockSize { get; set; }

        /// <summary>
        /// timeout in second before connecting to the server.
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// The constructor
        /// </summary>
        public WebsocketClient()
        {
            DataBlockSize = 1024;
            TimeOut = 1;            
        }

        /// <summary>
        /// Connect to the listener at the same endpoint to which its listening; 
        /// the only difference is the protocol over which you are connecting.
        /// </summary>
        /// <param name="uri">URL to connect on</param>
        /// <returns></returns>
        public async Task Connect(string uri)
        {
            Thread.Sleep(TimeOut * 1000); //Wait for a sec, so server starts and ready to accept connection...

            try
            {
                clientInstance = new ClientWebSocket();
                await clientInstance.ConnectAsync(new Uri(uri), CancellationToken.None);               
                Console.WriteLine($"Connection state: {clientInstance.State.ToString()}");
                await Task.WhenAll(Receive(clientInstance), Send(clientInstance));
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine("Exception {0}", ex);
            }
            finally
            {
                if (clientInstance != null)
                    clientInstance.Dispose();
                Console.WriteLine();
                Console.WriteLine("Websocket closed.");
            }
        }
        
        /// <summary>
        /// Write Send async Tasks
        /// </summary>
        /// <param name="webSocket">The Client Websocket</param>
        /// <returns></returns>
        private async Task Send(ClientWebSocket webSocket)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                Console.WriteLine("Write data to send to server or 'Exit' to disconnect.");
                string dataToSend = Console.ReadLine();
                if (dataToSend.ToUpper().Equals("EXIT"))
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "the client is disconnected.", CancellationToken.None);
                }
                else
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(dataToSend);

                    await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Binary, true, CancellationToken.None);
                    
                    await Task.Delay(TimeOut * 1000);
                }
            }
        }

        /// <summary>
        /// Write Receive async Tasks
        /// </summary>
        /// <param name="webSocket">The Client Websocket</param>
        /// <returns></returns>
        private async Task Receive(ClientWebSocket webSocket)
        {
            byte[] buffer = new byte[DataBlockSize];
            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
                else
                {
                    await Task.Delay(100);
                    Array.Resize<byte>(ref buffer, result.Count); // resizes the buffer to receive only the data sent.
                    string stringToTrim = "\0";
                    Console.WriteLine($"Receive:     " + Encoding.UTF8.GetString(buffer).TrimEnd(stringToTrim.ToCharArray()));
                    Array.Resize<byte>(ref buffer, DataBlockSize); // reset the original size of the buffer.
                }
            }
        }
    }
}

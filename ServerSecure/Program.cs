using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using vtortola.WebSockets;
using vtortola.WebSockets.Rfc6455;

namespace ServerSecure
{
    public struct Client
    {
        public int ID;
        public WebSocket Instance;

        public Client(int clientID, WebSocket clientObject)
        {
            ID = clientID;
            Instance = clientObject;
        }
    }
    class Program
    {
        //private static List<Client> ClientList; // Contain all clients connected
        static void Main(string[] args)
        {
            var options = new WebSocketListenerOptions()
            {
                /* The amount of TCP connections that are accepted and 
                queued before doing the WebSocket handshake. 
                Default: Environment.ProcessorCount * 10 */
                NegotiationQueueCapacity = 128,
                /* The amount of parallel WebSocket handshakes that can be done. 
                In some situations, like when using TLS, this process could be 
                slower because it needs more round trips, and increment this value 
                can improve the performance. Default: Environment.ProcessorCount * 2. */
                ParallelNegotiations = 16
            };

            //#############################################################################
            //X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            //store.Open(OpenFlags.ReadOnly);
            //var certificate = store.Certificates[1];
            //store.Close();
            //#############################################################################

            CancellationTokenSource cancellation = new CancellationTokenSource();
            
            var endpoint = new IPEndPoint(IPAddress.Any, 23600);
            WebSocketListener server = new WebSocketListener(endpoint, options);
            server.Standards.RegisterStandard(new WebSocketFactoryRfc6455(server));
            server.ConnectionExtensions.RegisterExtension(new WebSocketSecureConnectionExtension(new X509Certificate2(@"..\..\..\certificat\Test.pfx", "ndsoft")));

            //ClientList = new List<Client>();

            server.Start();
            Console.WriteLine("Welcom to the test of intracall");
            Console.WriteLine($"Server started on port {endpoint.Port.ToString()}");
            Console.WriteLine("\t \"exit\" to close the server");

            var task = Task.Run(() => AcceptWebSocketClientsAsync(server, cancellation.Token));

            string ServerCom;
            do
            {
                ServerCom = Console.ReadLine();                               
            } while (ServerCom != "exit");

            Console.Clear();
            Console.WriteLine("Goobye !");
            cancellation.Cancel();
            server.Stop();
            task.Wait();
        }

        static async Task AcceptWebSocketClientsAsync(WebSocketListener server, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                WebSocket ws = null;
                try
                {
                    ws = await server.AcceptWebSocketAsync(token).ConfigureAwait(false);
                    if (ws != null)
                    {
                        Console.WriteLine($"The client {ws.RemoteEndpoint.Address.ToString()} ist connected.");
                        Task.Run(() => HandleConnectionAsync(ws, token));                        
                    }
                }
                catch (Exception aex)
                {
                    Console.WriteLine($"Error Accepting client [{ws.GetHashCode()}]: {aex.GetBaseException().Message}");
                }
            }
            Console.WriteLine("Server Stop accepting clients");
        }

        static async Task HandleConnectionAsync(WebSocket ws, CancellationToken cancellation)
        {
            try
            {
                //if (!ClientList.Exists(client => client.ID.Equals(ws.GetHashCode())))
                //    ClientList.Add(new Client(ws.GetHashCode(), ws));



                while (ws.IsConnected && !cancellation.IsCancellationRequested)
                {
                    String msg = await ws.ReadStringAsync(cancellation).ConfigureAwait(false);
                    if (msg != null)
                    {
                        //if (ClientList.Count > 1)
                        //{
                        //    foreach (var client in ClientList)
                        //    {
                        //        Console.WriteLine($"client [{client.ID}]:> {msg}");
                        //        if (client.ID != ws.GetHashCode())
                        //            client.Instance.WriteString($"client [{ws.GetHashCode()}]:> {msg}");
                        //    }
                        //}
                        //else
                            ws.WriteString(msg);
                    }
                }
            }
            catch (Exception aex)
            {
                Console.WriteLine($"Error Handling connection: {aex.GetBaseException().Message}");
                try { ws.Close(); }
                catch { }
            }
            finally
            {
                //Client tmp = ClientList.Find(client => client.ID.Equals(ws.GetHashCode()));
                //if (tmp.Instance != null)
                //{
                //    ClientList.Remove(tmp);
                //    foreach (var client in ClientList)
                //    {
                //        client.Instance.WriteString($"the client [{ws.GetHashCode()}] is deconnected!");
                //    }
                //}
                Console.WriteLine($"The client {ws.RemoteEndpoint.Address.ToString()} ist deconnected.");
                ws.Dispose();
            }
        }
    }
}

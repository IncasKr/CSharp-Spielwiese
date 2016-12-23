using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AbortMethode
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread newThread = new Thread(new ThreadStart(TestMethod));
            newThread.Start();
            Thread.Sleep(1000);

            // Abort newThread.
            Console.WriteLine("Main aborting new thread.");
            newThread.Abort("Information from Main.");
            
            // Wait for the thread to terminate.
            newThread.Join();
            Console.WriteLine("New thread terminated - Main exiting.");

            Console.ReadLine();
        }

        static void TestMethod()
        {
            try
            {
                try
                {
                    try
                    {
                        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                        TcpClient client = new TcpClient(endpoint.AddressFamily);
                        client.Connect(endpoint);

                        using (var stream = new StreamReader(client.GetStream(), Encoding.Default))
                        {
                            bool clientConnected = client.Connected;
                            
                            while (clientConnected)
                            {
                                Console.WriteLine($"Begin reading...");
                                var rawData = (stream != null) ? stream.ReadLine() : null; /*rawData is also null if the end of the input stream is reached*/
                                Console.WriteLine($"End reading...");
                                if (string.IsNullOrEmpty(rawData))
                                {
                                    if (rawData == null) /* if stream is null or if the end of the input stream is reached (stream.EndOfStream)*/
                                    {
                                        Console.WriteLine($"End of TCP stream reached. Closing connection");
                                        if (client != null)
                                        {
                                            Console.WriteLine($"Closing TCPClient({client.Connected.ToString()}) Connection...");
                                            client.Close();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Empty message received from IC Server.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("New thread running.");
                                    Thread.Sleep(1000);
                                }
                                clientConnected = (client == null) ? false : client.Connected;
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        var closed = false;
                        var socketEx = ex.InnerException as SocketException;
                        if (socketEx != null)
                        {
                            if (socketEx.ErrorCode == 10004)
                            {
                                closed = true;
                            }
                        }
                        if (!closed)
                        {
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error on IntracallClient receivedThread", ex);
                }
            }
            catch (ThreadAbortException abortException)
            {
                Console.WriteLine((string)abortException.ExceptionState);
            }
        }
    }
}

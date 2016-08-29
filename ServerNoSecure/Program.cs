using System;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using vtortola.WebSockets;
using WebSocketListenerServer;

namespace ServerNoSecure
{
    class Program
    {
        private static string SQLResponse = string.Empty;
        static void Main(string[] args)
        {
            using (var server = new WebSocketEventListener(new IPEndPoint(IPAddress.Any, 23600)/*, new X509Certificate2(@"..\certificat\Test.pfx", "ndsoft")*/))
            {               
                server.OnConnect += (ws) =>
                {
                    Console.WriteLine($"Connection from {ws.RemoteEndpoint}");
                    ws.WriteStringAsync("You  are successfull connected!", CancellationToken.None).Wait();
                };
                server.OnDisconnect += (ws) => Console.WriteLine($"Disconnection from {ws.RemoteEndpoint}");
                server.OnError += (ws, ex) => { if (ex.HResult != -2146233088) Console.WriteLine($"Error: {ex.Message}"); };
                server.OnMessage += (ws, msg) =>
                {
                    if (msg[0].ToString() == "#")
                    {
                        string[] sqlElement = msg.Substring(1, msg.Length - 1).Split('#');
                        if (sqlElement.Length == 3)
                        {
                            switch (sqlElement[0].ToUpper())
                            {
                                case "SQL":
                                    GetRequest(sqlElement[1], sqlElement[2]);
                                    break;
                            }
                            ws.WriteStringAsync(SQLResponse, CancellationToken.None).Wait();
                        }
                        else
                        {
                            ws.WriteStringAsync($"IC:> {sqlElement[0]} error !", CancellationToken.None).Wait();
                        }
                    } 
                    else
                    {
                        Console.WriteLine($"[{ws.RemoteEndpoint.Address}]:> {msg}");
                        ws.WriteStringAsync($"IC:> {msg}", CancellationToken.None).Wait();
                        WebSocketStringExtensions.WriteString(ws, $"Message from Extension: {msg}");
                    }                 
                };
                server.OnData += (ws, data) =>
                {
                    string fullData = Encoding.UTF8.GetString(data);
                    int sizeDataCache = 10;
                    int pos = 0;

                    Console.WriteLine($"[{ws.RemoteEndpoint.Address}]:> DATA\n{{");
                    do
                    {
                        Console.WriteLine($"\t{Encoding.UTF8.GetString(data, pos, sizeDataCache)}");
                        pos += sizeDataCache;
                    } while (pos < fullData.Length);
                    Console.WriteLine("}");

                    ws.WriteStringAsync($"IC:> {Encoding.UTF8.GetString(data)}", CancellationToken.None).Wait();
                };

                server.Start();
                Console.WriteLine("Welcome by Intracall Server test");
                Console.WriteLine("Tape 'exit' to exit.");
                do
                { } while (Console.ReadLine() != "exit");
                server.Stop();
            }
        }

        private static void GetRequest(string sqlTableName, string sqlRequest)
        {
            /* Begin connect to Databank  */
            Data dataIC = new Data();
            dataIC.GetData(sqlTableName, sqlRequest);
            string columnName = "| ";
            string rowValue;
            for (int i = 0; i < dataIC.Result.ColumnCount; i++)
            {
                columnName += $"{dataIC.Result.Heads[i].Name.ToUpper()} | ";
            }
            string line = new string('\u2500', columnName.Length - 1); // \u2500 ist die Dünne horizontale Linie

            Console.WriteLine();
            Console.WriteLine("Display of table result");
            SQLResponse += "Display of table result\n";           
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(line);
            SQLResponse += $"{line}\n";
            Console.WriteLine(columnName);
            SQLResponse += $"{columnName}\n";
            Console.WriteLine(line);
            SQLResponse += $"{line}\n";
            Console.ResetColor();

            foreach (var value in dataIC.Result.Rows)
            {
                rowValue = "| ";
                for (int i = 0; i < value.Length; i++)
                {
                    rowValue += $"{value[i]} | ";
                }
                Console.WriteLine(rowValue);
                SQLResponse += $"{rowValue}\n";
                Console.WriteLine(line);
                SQLResponse += $"{line}\n";
            }

            Console.WriteLine("\n\n");
            /* End Connect to Databank  */
        }
    }
}

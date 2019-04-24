using Microsoft.Owin.Hosting;
using OwinLib;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OwinAPI
{
    public class Program
    {
        static string url = $"http://{Dns.GetHostEntry("localhost").HostName}:28000/";

        static void Main(string[] args)
        {
            /*var testURL = $"http://{ExtractIPString("localhost")}:9200/";
            var test = WebApp.Start<Startup>(testURL);
            Console.WriteLine("Server test started at:" + testURL);
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server started at:" + url);

                List<HttpResponseMessage> tasks = new List<HttpResponseMessage>();
                HttpClient client = new HttpClient();
                var response = client.GetAsync(url + "api/test/gettest").Result;
                Console.WriteLine($"Test:\n{response.Content.ReadAsStringAsync().Result}");
                var response2 = client.GetAsync(url + "api/test/getauthenticate/2/2").Result;
                Console.WriteLine($"Test2:\n{response2.Content.ReadAsStringAsync().Result}");

                for (int i = 0; i < 10; i++)
                {
                    tasks.Add(Task.Factory.StartNew(() => { return client.GetAsync($"{url}api/test/getfacultyof/{i}").Result; }).Result);
                }

                Console.WriteLine($"Calculate faculty:");
                foreach (var item in tasks.ToArray())
                {
                    Console.WriteLine($"\t{item.Content.ReadAsStringAsync().Result}");
                }
                
                Console.ReadLine();
                test.Dispose();
            }*/

            string user = Environment.UserName;
            Console.Write($"Please enter the AD user password for {user}: ");
            string password = GetInputString("*");
            Console.Write($"Please enter the timeout in second: ");
            int.TryParse(Console.ReadLine(), out int timeout);
            HttpClient client = new HttpClient(); 

            while (true)
            {
                try
                {
                    var response = client.GetAsync($"{url}auth/ad/register/{user}/{password}").Result;
                    response = client.GetAsync($"{url}auth/ad/gettoken/{user}").Result;
                    var token = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                    Console.WriteLine($"token generated: {token}");
                    response = client.GetAsync($"{url}auth/ad/checktoken?token={token}").Result;
                    Console.WriteLine($"token generated before {timeout} second(s): {response.Content.ReadAsStringAsync().Result}");
                    for (int i = 0; i < timeout; i++)
                    {
                        string val = (timeout - i).ToString();
                        val = val.Insert(0, new string('0', (4-val.Length))); 
                        Console.Write($"{val} second(s)...");
                        Thread.Sleep(1000);
                        if (i < timeout)
                        {
                            Console.SetCursorPosition(Console.CursorLeft - 17, Console.CursorTop);
                            Console.Write(new string(' ', 17));
                            Console.SetCursorPosition(Console.CursorLeft - 17, Console.CursorTop);
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                    }
                    
                    response = client.GetAsync($"{url}auth/ad/checktoken?token={token}").Result;
                    Console.WriteLine($"check token after {timeout} second(s):: {response.Content.ReadAsStringAsync().Result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                Console.ReadLine();
            }           
        }

        private static string GetInputString(string charShowed = null)
        {
            Queue<char> queue = new Queue<char>();
            ConsoleKeyInfo consoleKeyInfo;
            string password = string.Empty;

            // push until the enter key is pressed
            while ((consoleKeyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (consoleKeyInfo.Key != ConsoleKey.Backspace)
                {
                    queue.Enqueue(consoleKeyInfo.KeyChar);
                    Console.Write(string.IsNullOrEmpty(charShowed) ? consoleKeyInfo.KeyChar : charShowed[0]);
                }
                else
                {
                    if (queue.Count > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        queue.Dequeue();
                    }
                }
            }

            if (consoleKeyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine(consoleKeyInfo.KeyChar);
            }

            return new string(queue.ToArray());
        }

        /// <summary>
        /// Converts an host to IP address and gets the valid ip address.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or host address.</param>
        /// <returns>The valid IP address.</returns>
        private static string ExtractIPString(string hostNameOrAddress)
        {
            string ipString = null;
            string[] addressComponent = hostNameOrAddress.Split('/');
            foreach (var partOfAddress in addressComponent)
            {
                try
                {
                    if (string.IsNullOrEmpty(hostNameOrAddress))
                    {
                        return null;
                    }

                    if (IPAddress.TryParse(hostNameOrAddress, out IPAddress ipObj))
                    {
                        ipString = ipObj.MapToIPv4().ToString();
                        break;
                    }

                    IPAddress[] ipList;
                    Ping pingSender = new Ping();

                    // Solves the host address.
                    if (hostNameOrAddress.ToLower() == "localhost")
                    {
                        ipList = Dns.GetHostAddresses(Environment.MachineName);
                    }
                    else
                    {
                        ipList = Dns.GetHostAddresses(hostNameOrAddress);
                    }
                    // Gets only the ip for version 4.
                    List<IPAddress> ipListSort = new List<IPAddress>();
                    foreach (var ip in ipList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipListSort.Add(ip);
                        }
                    }

                    // Orders by IP Desc
                    ipListSort.Sort(delegate (IPAddress x, IPAddress y)
                    {
                        if (x == null && y == null) return 0;
                        else if (x == null) return -1;
                        else if (y == null) return 1;
                        else return y.ToString().CompareTo(x.ToString());
                    });

                    // Searches for a valid IP.
                    foreach (IPAddress ip in ipListSort)
                    {
                        if (pingSender.Send(ip).Status == IPStatus.Success)
                        {
                            ipString = ip.ToString();
                            break;
                        }
                    }
                    break;
                }
                catch { }
            }
            return ipString;
        }
    }
}

using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace OwinSelfhostSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = $"http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                Random random = new Random();
                for (int i = 0; i < 5; i++)
                {
                    ushort id = (ushort)random.Next(0, ushort.MaxValue);
                    var content = JsonConvert.SerializeObject(new Data(id, $"test value {id}"));
                    client.PostAsJsonAsync($"{baseAddress}api/values/", content).Wait();
                }
                
                var response = client.GetAsync(baseAddress + "api/values").Result;

                Console.WriteLine($"Server starting on {baseAddress}api/values...");
                Console.WriteLine(response);
                Console.WriteLine($"\n{response.Content.ReadAsStringAsync().Result}");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Converts an host name or address to IP address.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or address.</param>
        /// <returns>The IP address string.</returns>
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
                    else if (IPAddress.TryParse(hostNameOrAddress, out IPAddress ipObj))
                    {
                        ipString = ipObj.MapToIPv4().ToString();
                        break;
                    }
                    else
                    {
                        IPAddress[] ipList;
                        if (hostNameOrAddress == "localhost")
                        {
                            ipList = Dns.GetHostAddresses(Environment.MachineName);
                            foreach (IPAddress ip in ipList)
                            {
                                if (ip.AddressFamily == AddressFamily.InterNetwork)
                                {
                                    ipString = ip.ToString();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            ipList = Dns.GetHostAddresses(hostNameOrAddress);
                            ipString = ipList[ipList.Length - 1].MapToIPv4().ToString();
                        }
                        break;
                    }
                }
                catch { }
            }
            return ipString;
        }
    }
}

using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace OwinSelfhostSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

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

                Console.WriteLine(response);
                Console.WriteLine($"\n{response.Content.ReadAsStringAsync().Result}");
                Console.ReadLine();
            }
        }
    }
}

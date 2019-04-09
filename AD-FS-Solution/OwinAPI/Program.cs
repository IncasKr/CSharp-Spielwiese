using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OwinAPI
{
    public class Program
    {
        const string url = "http://localhost:9100/";

        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server started at:" + url);

                List<HttpResponseMessage> tasks = new List<HttpResponseMessage>();
                HttpClient client = new HttpClient();
                var response = client.GetAsync(url + "api/test/gettest").Result;
                Console.WriteLine($"Test:\n{response.Content.ReadAsStringAsync().Result}");

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
            }
        }
    }
}

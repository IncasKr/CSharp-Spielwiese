using Microsoft.Owin.Hosting;
using System;

namespace OwinAPI
{
    public class Program
    {
        const string url = "http://localhost:9100";

        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server started at:" + url);
                Console.ReadLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hostConvert
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string hostname = "";
        
            IPAddress IP;
            if (IPAddress.TryParse(hostname, out IP))
            {
                Console.WriteLine($"IP: {IP.MapToIPv4()}");
            }
            else
            {
                IP = Dns.GetHostAddresses(hostname)[0];
                Console.WriteLine($"IP 1: {IP.MapToIPv4()}");
            }

            GetRdpFiles();

            Console.ReadLine();
        }  
        private static void GetRdpFiles()
        {
            try
            {
                string[] dirs = Directory.GetFiles(@"c:\", "c*");


                if (dirs.Length == 0)
                {
                    Console.WriteLine("No files!");
                }
                
                foreach (string file in dirs)
                {
                    DateTime filedate = File.GetCreationTimeUtc(file);
                    Console.WriteLine($"Filename: {file}");
                    Console.WriteLine($"Creation time: {filedate.ToShortDateString()}");
                    int days = DateTime.Now.Subtract(filedate).Days;
                    string text = (days > 0) ? $"" : $"not ";
                    string pwd = "qwertz";
                    Console.WriteLine($"Creation time is {text}older from {days} days.");
                    Console.WriteLine($"{Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(pwd))}");
                }
            }
            catch (Exception)
            {
                
            }
        }

    }
}

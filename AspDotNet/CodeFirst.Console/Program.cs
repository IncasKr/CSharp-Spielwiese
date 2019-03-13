using CodeFirst.DAL;
using System;

namespace CodeFirst.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("List of harleys");
            ShowVehicles();
            System.Console.WriteLine("Fin");
            System.Console.ReadLine();

        }

        private static void ShowVehicles()
        {
            HarleyProvider harleyProvider = new HarleyProvider();
            foreach (var harley in harleyProvider.GetHarleys())
            {
                System.Console.WriteLine($"\t{harley}");
            }
        }
    }
}

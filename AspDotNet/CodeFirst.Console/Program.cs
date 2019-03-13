using CodeFirst.DAL;
using System;

namespace CodeFirst.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("List of harleys");
            ShowVehicles();
            Console.WriteLine("Fin");
            Console.ReadLine();

        }

        private static void ShowVehicles()
        {
            HarleyProvider harleyProvider = new HarleyProvider();
            foreach (var harley in harleyProvider.GetHarleys())
            {
                Console.WriteLine($"\t{harley}");
            }
        }
    }
}

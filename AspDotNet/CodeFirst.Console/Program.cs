using CodeFirst.DAL;
using System;

namespace CodeFirst.Console
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static void AfficheAllVehicule()
        {
            HarleyProvider harleyProvider = new HarleyProvider();
            System.Console.WriteLine("List of harleys");
            foreach (var harley in harleyProvider.GetAllHarleys())
            {
                System.Console.WriteLine($"\t{harley}");
            }
        }
    }
}

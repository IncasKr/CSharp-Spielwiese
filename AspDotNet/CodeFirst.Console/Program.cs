using CodeFirst.DAL;
using System;

namespace CodeFirst.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("List of harleys");
            AfficheAllVehicule();
            System.Console.WriteLine("Fin");
            System.Console.ReadLine();

        }

        private static void AfficheAllVehicule()
        {
            HarleyProvider harleyProvider = new HarleyProvider();
            foreach (var harley in harleyProvider.GetAllHarleys())
            {
                System.Console.WriteLine($"\t{harley}");
            }
        }
    }
}

using CodeFirst.DAL;
using CodeFirst.DTO;
using System;

namespace CodeFirst.Cons
{
    class Program
    {
        private static HarleyProvider harleyProvider;
        private static GarageContext garage;
        static void Main(string[] args)
        {
            garage = new GarageContext();
            harleyProvider = new HarleyProvider(garage);
            FillHarleys();
            ShowVehicles();
            UpdatingColor("Blue");
            ShowVehicles();
            Console.WriteLine("Fin");
            Console.ReadLine();

        }

        private static void ShowVehicles()
        {
            Console.WriteLine("List of harleys");
            foreach (var harley in harleyProvider.GetHarleys())
            {
                Console.WriteLine($"\t| {harley.ID} | {harley.Model} | {harley.Color} | {harley.Capacity} | {harley.Power} |");
            }
        }

        /// <summary>
        /// Fills up the harleys into the garage.
        /// </summary>
        static void FillHarleys()
        {
            Console.WriteLine("===== Filling up the harleys into the garage... =====");

            harleyProvider.Create(new EntityHarley
            {
                Color = "Black",
                Model = "883 Iron",
                Capacity = 12,
                Power = 220,
                SeatNumber = 2,
                WeelsNumber = 2
            });
            harleyProvider.Create(new EntityHarley
            {
                Color = "Red",
                Model = "1200 Nighster",
                Capacity = 12,
                Power = 150,
                SeatNumber = 1,
                WeelsNumber = 2
            });
            harleyProvider.Create(new EntityHarley
            {
                Color = "Gray",
                Model = "1200 Forty height",
                Capacity = 12,
                Power = 100,
                SeatNumber = 2,
                WeelsNumber = 2
            });
            harleyProvider.Create(new EntityHarley
            {
                Color = "Black",
                Model = "Fat boy",
                Capacity = 25,
                Power = 200,
                SeatNumber = 1,
                WeelsNumber = 2
            });
            Console.WriteLine("======================");
        }

        static void UpdatingColor(string color)
        {
            Console.WriteLine("===== Updating the color of all harleys =====");
            foreach (EntityHarley harley in harleyProvider.GetHarleys())
            {
                harley.Color = color;
                harleyProvider.Update(harley);
            }
            Console.WriteLine("======================");
        }
    }
}

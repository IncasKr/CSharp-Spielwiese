using CodeFirst.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CodeFirst.DAL
{
    internal class GarageContextInitializer : DropCreateDatabaseIfModelChanges<GarageContext>
    {
        protected override void Seed(GarageContext context)
        {
            Console.WriteLine("===== Fill up new data starting... =====");
            List<EntityHarley> listeHarley = new List<EntityHarley>
            {
                new EntityHarley { Color = "Black", Model = "Fatbob", Capacity = 15, Power = 150, SeatNumber = 1, WeelsNumber = 2, CustomPainting = 0 },
                new EntityHarley { Color = "White", Model = "Road King", Capacity = 30, Power = 100, SeatNumber = 1, WeelsNumber = 2, CustomPainting = 0 },
                new EntityHarley { Color = "Black", Model = "883 Iron", Capacity = 12, Power = 170, SeatNumber = 1, WeelsNumber = 2, CustomPainting = 0 }
            };

            List<EntityFerrari> listeFerrari = new List<EntityFerrari>
            {
                new EntityFerrari { Color = "Red", Model = "Enzo", Capacity = 60, Power = 250, SeatNumber = 3, WeelsNumber = 4 },
                new EntityFerrari { Color = "Blue", Model = "California", Capacity = 50, Power = 365, SeatNumber = 2, WeelsNumber = 4 }
            };

            var garage = new EntityGarage();
            listeHarley.ForEach(entity => garage.Vehicles.Add(entity));
            context.Garages.Add(garage);
            listeFerrari.ForEach(entity => context.Ferraris.Add(entity));
            Console.WriteLine("===== Fill up new data end. =====");
        }
    }
}

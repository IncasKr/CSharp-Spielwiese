namespace CodeFirst.DAL.Migrations
{
    using CodeFirst.DTO;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    
    internal sealed class Configuration : DbMigrationsConfiguration<CodeFirst.DAL.GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CodeFirst.DAL.GarageContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            /*Console.WriteLine("===== Fill up new data starting... =====");
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

            var garage = new EntityGarage("Krefeld");
            listeHarley.ForEach(entity => garage.Vehicles.Add(entity));
            context.Garages.Add(garage);
            listeFerrari.ForEach(entity => context.Ferraris.Add(entity));
            Console.WriteLine("===== Fill up new data end. =====");*/
        }
    }
}
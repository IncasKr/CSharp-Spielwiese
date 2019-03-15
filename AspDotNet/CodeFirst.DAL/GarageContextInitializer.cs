using CodeFirst.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.DAL
{
    internal class GarageContextInitializer : DropCreateDatabaseIfModelChanges<GarageContext>
    {
        protected override void Seed(GarageContext context)
        {
            Console.WriteLine("===== Fill up new data starting... =====");
            List<EntityHarley> listeHarley = new List<EntityHarley>
            {
                new EntityHarley { Color = "Black", Model = "Fatbob", Capacity = 15, Power = 150 },
                new EntityHarley { Color = "White", Model = "Road King", Capacity = 30 },
                new EntityHarley { Color = "Black", Model = "883 Iron", Capacity = 12 }
            };

            List<EntityFerrari> listeFerrari = new List<EntityFerrari>
            {
                new EntityFerrari { Color = "Red", Model = "Enzo" },
                new EntityFerrari { Color = "Blue", Model = "California" }
            };

            listeHarley.ForEach(entity => context.Harleys.Add(entity));
            listeFerrari.ForEach(entity => context.Ferraris.Add(entity));
            Console.WriteLine("===== Fill up new data end. =====");
        }
    }
}

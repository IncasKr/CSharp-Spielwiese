using CodeFirst.DTO;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace CodeFirst.DAL
{
    public class GarageContext : DbContext
    {
        public DbSet<EntityHarley> Harleys { get; set; }

        public DbSet<EntityFerrari> Ferraris { get; set; }

        public DbSet<EntityGarage> Garages { get; set; }

        public GarageContext() : base("ndDBTest")
        {
            Database.SetInitializer<GarageContext>(new GarageContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

        public ObjectContext GetObjectContext()
        {
            return (this as IObjectContextAdapter).ObjectContext;
        }        
    }
}

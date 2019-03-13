using CodeFirst.DTO;
using System.Data.Entity;

namespace CodeFirst.DAL
{
    public class GarageContext : DbContext
    {
        public DbSet<EntityHarley> Harleys { get; set; }
    }
}

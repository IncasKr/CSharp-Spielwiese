﻿using CodeFirst.DTO;
using System.Data.Entity;

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
    }
}

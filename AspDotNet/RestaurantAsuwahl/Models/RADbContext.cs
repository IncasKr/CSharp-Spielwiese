using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantAsuwahl.Models
{
    public class RADbContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
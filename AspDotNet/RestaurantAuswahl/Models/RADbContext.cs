using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Represents the database context of this project.
    /// </summary>
    public class RADbContext : DbContext
    {
        /// <summary>
        /// Get or set the surveys from or to the database.
        /// </summary>
        public DbSet<Survey> Surveys { get; set; }

        /// <summary>
        /// Get or set the restaurants from or to the database.
        /// </summary>
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
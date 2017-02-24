using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Lists the different registered Restaurants.
    /// </summary>
    public class Dal : IDal
    {
        private RADbContext db;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Dal()
        {
            db = new RADbContext();
        }

        /// <summary>
        /// Get the list of all restaurants.
        /// </summary>
        /// <returns>Returns the list of restaurants.</returns>
        public List<Restaurant> GetAllRestaurants()
        {
            return db.Restaurants.ToList();
        }

        /// <summary>
        /// Dispose the data context.
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
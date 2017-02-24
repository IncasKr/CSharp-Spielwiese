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
        /// Create a new restaurant and save it into the database.
        /// </summary>
        /// <param name="name">name of the restaurant</param>
        /// <param name="telephone">telephone of the restaurant</param>
        public void CreateRestaurant(string name, string telephone)
        {
            db.Restaurants.Add(new Restaurant { Name = name, Telephone = telephone });
            db.SaveChanges();
        }

        /// <summary>
        /// Edite an existing restaurant.
        /// </summary>
        /// <param name="id">ID of the existing restaurant</param>
        /// <param name="name">New name for this restaurant</param>
        /// <param name="telephone">New telephone number for this restaurant</param>
        public void EditRestaurant(int id, string name, string telephone)
        {
            Restaurant rFound = db.Restaurants.FirstOrDefault(resto => resto.Id == id);
            if (rFound != null)
            {
                rFound.Name = name;
                rFound.Telephone = telephone;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Get the list of all restaurants from the database.
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

        /// <summary>
        /// Determines if this restaurant already exists into the database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RestaurantExists(string name)
        {
            foreach (var item in db.Restaurants.ToList())
            {
                if (item.Name.ToLower().Equals(name.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
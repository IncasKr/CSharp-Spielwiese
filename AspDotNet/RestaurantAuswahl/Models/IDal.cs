using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Data Access Layer (DAL)
    /// </summary>
    public interface IDal : IDisposable
    {
        /// <summary>
        /// Create a new restaurant.
        /// </summary>
        /// <param name="name">Name of the restaurant</param>
        /// <param name="telephone">telephone number of the restaurant</param>
        void CreateRestaurant(string name, string telephone);
        
        /// <summary>
        /// Get all restaurants from data context.
        /// </summary>
        /// <returns>Returns the list of restaurants.</returns>
        List<Restaurant> GetAllRestaurants();

        /// <summary>
        /// Determines if this restaurant already exists in the data context
        /// </summary>
        /// <param name="name">name of the restaurant</param>
        /// <returns></returns>
        bool RestaurantExists(string name);
    }
}

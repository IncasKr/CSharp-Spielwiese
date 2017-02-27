using System;
using System.Collections.Generic;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Data Access Layer (DAL)
    /// </summary>
    public interface IDal : IDisposable
    {
        int AddUser(string name, string password);

        void AddVote(int idSurvey, int idRestaurant, int idUser);

        bool AlreadyVoted(int idSurvey, string idUser);

        User Authenticate(string name, string password);

        /// <summary>
        /// Create a new restaurant.
        /// </summary>
        /// <param name="name">Name of the restaurant</param>
        /// <param name="telephone">telephone number of the restaurant</param>
        void CreateRestaurant(string name, string telephone);

        int CreateSurvey();

        /// <summary>
        /// Edite an existing restaurant.
        /// </summary>
        /// <param name="id">ID of the existing restaurant</param>
        /// <param name="name">New name for this restaurant</param>
        /// <param name="telephone">New telephone number for this restaurant</param>
        void EditRestaurant(int id, string name, string telephone);

        /// <summary>
        /// Get all restaurants from data context.
        /// </summary>
        /// <returns>Returns the list of restaurants.</returns>
        List<Restaurant> GetAllRestaurants();

        List<Results> GetResults(int idSurvey);

        User GetUser(int id);

        User GetUser(string id);

        /// <summary>
        /// Determines if this restaurant already exists in the data context
        /// </summary>
        /// <param name="name">name of the restaurant</param>
        /// <returns></returns>
        bool RestaurantExists(string name);
    }
}

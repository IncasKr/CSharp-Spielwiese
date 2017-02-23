using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Represents the user table.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Get or set the identification of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get or set the first name of the user.
        /// </summary>
        public string FirstName { get; set; }
    }
}
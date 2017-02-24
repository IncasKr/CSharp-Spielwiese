using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Represents the restaurant table.
    /// </summary>
    [Table("Restaurant")]
    public class Restaurant
    {
        /// <summary>
        /// Get or set the identification of the restaurant.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get or set the name of the restaurant.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the telephone number of the restaurant.
        /// </summary>
        public string Telephone { get; set; }
    }
}
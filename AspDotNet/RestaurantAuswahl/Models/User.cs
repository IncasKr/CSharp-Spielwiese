using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Represents the user table.
    /// </summary>
    [Table("User")]
    public class User
    {
        /// <summary>
        /// Get or set the identification of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get or set the first name of the user.
        /// </summary>
        [Required, MaxLength(25)]
        public string FirstName { get; set; }
    }
}
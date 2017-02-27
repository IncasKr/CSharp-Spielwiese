using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the telephone number of the restaurant.
        /// </summary>
        public string Telephone { get; set; }
    }
}
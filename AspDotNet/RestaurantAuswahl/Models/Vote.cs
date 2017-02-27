using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Represents the voting table.
    /// </summary>
    [Table("Vote")]
    public class Vote
    {
        /// <summary>
        /// Get or set the identification of the vote.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get or set the restaurant voted.
        /// </summary>
        [Required]
        public virtual Restaurant Restaurant { get; set; }

        /// <summary>
        /// Get or set the user that initializes the vote.
        /// </summary>
        [Required]
        public virtual User User { get; set; }
    }
}
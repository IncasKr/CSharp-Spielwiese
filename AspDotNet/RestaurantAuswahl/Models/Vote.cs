using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        public virtual Restaurant Restaurant { get; set; }

        /// <summary>
        /// Get or set the user that initializes the vote.
        /// </summary>
        public virtual User User { get; set; }
    }
}
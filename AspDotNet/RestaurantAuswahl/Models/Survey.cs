using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Represents the survey table.
    /// </summary>
    [Table("Survey")]
    public class Survey
    {
        /// <summary>
        /// Get or set the identification of the survey.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get or set the date that occurring the survey.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Get or set the list of votes.
        /// </summary>
        [Required]
        public virtual List<Vote> Votes { get; set; }
    }
}
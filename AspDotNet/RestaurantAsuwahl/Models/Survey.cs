using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantAsuwahl.Models
{
    /// <summary>
    /// Represents the survey table.
    /// </summary>
    public class Survey
    {
        /// <summary>
        /// Get or set the identification of the survey.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get or set the date that occurring the survey.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Get or set the list of votes.
        /// </summary>
        public virtual List<Vote> Votes { get; set; }
    }
}
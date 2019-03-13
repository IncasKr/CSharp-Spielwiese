using System.Collections.Generic;

namespace eBiB.Models
{
    /// <summary>
    /// Author list definition class
    /// </summary>
    public class Authors
    {
        /// <summary>
        /// Generation of the authors list
        /// </summary>
        private List<Author> list = new List<Author>
        {
            new Author(1, "Martin"),
            new Author(3, "Vernandez"),
            new Author(2, "David")
        };

        /// <summary>
        /// Gets the list of existing authors
        /// </summary>
        /// <returns>The list of authors</returns>
        public List<Author> GetAuthors()
        {
            list.Sort(delegate (Author a, Author b)
            {
                if (a.Name == null && b.Name == null) return 0;
                else if (a.Name == null) return -1;
                else if (b.Name == null) return 1;
                else return a.Name.CompareTo(b.Name);
            });
            return list;
        }          
    }
}
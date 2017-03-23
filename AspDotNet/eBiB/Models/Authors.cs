using System.Collections.Generic;

namespace eBiB.Models
{
    public class Authors
    {
        public List<Author> GetAuthors()
        {
            return new List<Author>
            {
                new Author(1, "Martin"),
                new Author(3, "Vernandez"),
                new Author(2, "David")
            };
        }
    }
}
﻿using System.Collections.Generic;

namespace eBiB.Models
{
    public class Authors
    {
        private List<Author> list = new List<Author>
        {
            new Author(1, "Martin"),
            new Author(3, "Vernandez"),
            new Author(2, "David")
        };

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
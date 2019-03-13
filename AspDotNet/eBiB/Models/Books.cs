using System.Collections.Generic;

namespace eBiB.Models
{
    /// <summary>
    /// Book list definition class
    /// </summary>
    public class Books
    {
        /// <summary>
        /// Gets the list of existing books
        /// </summary>
        /// <returns>The list of books</returns>
        public List<Book> GetBooks()
        {
            var list = new List<Book>
            {
                new Book(3, "Mathe für Informatiker", 3),
                new Book(1, "Einführung in .NET", 1, "gladis@ndsoft.de"),
                new Book(5, "Biotop", 3),
                new Book(4, "Einführung in Raspberry Pi", 1, "gladis@ndsoft.de"),
                new Book(2, "Einführung in ASP .NET", 2, "sergio@ndsoft.de")
            };
            list.Sort(delegate (Book a, Book b)
            {
                if (a.Title == null && b.Title == null) return 0;
                else if (a.Title == null) return -1;
                else if (b.Title == null) return 1;
                else return a.Title.CompareTo(b.Title);
            });
            return list;
        }
    }
}
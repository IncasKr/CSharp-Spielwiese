﻿using System.Collections.Generic;

namespace eBiB.Models
{
    public class Books
    {
        public List<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book(3, "Mathe für Informatiker", 3),
                new Book(1, "Einführung in .NET", 1, "gladis@incas.de"),
                new Book(5, "Biotop", 3),
                new Book(4, "Einführung in Raspberry Pi", 1, "gladis@incas.de"),
                new Book(2, "Einführung in ASP .NET", 2, "sergio@incas.de")
            };
        }
    }
}
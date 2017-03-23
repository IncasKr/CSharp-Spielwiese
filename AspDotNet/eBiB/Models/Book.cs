using System;

namespace eBiB.Models
{
    public class Book
    {
        public int ID { get; private set; }

        public string Title { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public int AuthorID { get; private set; }

        public string ClientID { get; set; }

        public Book(int id, string title, int owner, string clientID = ""):this(id, title, owner, DateTime.Now, clientID)
        { }

        public Book(int id, string title, int owner, DateTime releaseDate, string clientID = "")
        {
            ID = id;
            Title = title;
            AuthorID = owner;
            ReleaseDate = releaseDate;
            ClientID = clientID;
        }
    }
}
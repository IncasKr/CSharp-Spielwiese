using System;

namespace eBiB.Models
{
    /// <summary>
    /// Book definition class
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The book's identifier
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// The title of the book
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The date of publication
        /// </summary>
        public DateTime ReleaseDate { get; private set; }

        /// <summary>
        /// The ID of the book owner
        /// </summary>
        public int AuthorID { get; private set; }

        /// <summary>
        /// The borrower identifier of the book
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// The book builder has the current date
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="owner"></param>
        /// <param name="clientID"></param>
        public Book(int id, string title, int owner, string clientID = ""):this(id, title, owner, DateTime.Now, clientID)
        { }

        /// <summary>
        /// The book builder has a given date
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="owner"></param>
        /// <param name="releaseDate"></param>
        /// <param name="clientID"></param>
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
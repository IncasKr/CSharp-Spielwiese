namespace eBiB.Models
{
    /// <summary>
    /// Author definition class
    /// </summary>
    public class Author
    {
        /// <summary>
        /// The author's identifier
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// The name of the author
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The constructor of the author
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="name">The name</param>
        public Author(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
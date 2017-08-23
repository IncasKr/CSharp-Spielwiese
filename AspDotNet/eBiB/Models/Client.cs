namespace eBiB.Models
{
    /// <summary>
    /// Customer definition class
    /// </summary>
    public class Client
    {
        /// <summary>
        /// The customer's identifier
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The constructor of the customer
        /// </summary>
        /// <param name="email">The identifier</param>
        /// <param name="name">The name</param>
        public Client(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}
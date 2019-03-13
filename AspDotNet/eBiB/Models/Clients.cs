using System.Collections.Generic;

namespace eBiB.Models
{
    /// <summary>
    /// Customer list definition class
    /// </summary>
    public class Clients
    {
        /// <summary>
        /// Generation of the customers list
        /// </summary>
        private List<Client> list = new List<Client>
        {
            new Client("gladis@ndsoft.de", "Gladis"),
            new Client("sergio@ndsoft.de", "Sergio")
        };

        /// <summary>
        /// Gets the list of existing customers
        /// </summary>
        /// <returns>The list of customers</returns>
        public List<Client> GetClients()
        {
            list.Sort(delegate (Client a, Client b)
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
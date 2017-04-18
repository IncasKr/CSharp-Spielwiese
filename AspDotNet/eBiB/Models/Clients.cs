using System.Collections.Generic;

namespace eBiB.Models
{
    public class Clients
    {
        public List<Client> GetClients()
        {
            var list = new List<Client>
            {
                new Client("gladis@incas.de", "Gladis"),                
                new Client("sergio@incas.de", "Sergio")
            };
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
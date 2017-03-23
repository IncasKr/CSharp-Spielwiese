using System.Collections.Generic;

namespace eBiB.Models
{
    public class Clients
    {
        public List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client("gladis@incas.de", "Gladis"),                
                new Client("sergio@incas.de", "Sergio")
            };
        }
    }
}
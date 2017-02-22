using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Models
{
    public class Clients
    {
        public List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client { Name = "Gladis", Age = 14 },
                new Client { Name = "Sergio", Age = 9 },
                new Client { Name = "Marco", Age = 10 }
            };
        }
    }
}
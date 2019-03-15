using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.DTO
{
    public class EntityGarage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EntityVehicle> Vehicles { get; set; }

        public EntityGarage(string name)
        {
            Name = name;
            Vehicles = new List<EntityVehicle>();
        }
    }
}

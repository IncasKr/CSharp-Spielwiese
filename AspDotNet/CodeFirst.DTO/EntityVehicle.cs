using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.DTO
{
    public abstract class EntityVehicle : IVehicle
    {
        public abstract string Model { get; set; }
        public abstract string Color { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.DTO
{
    public abstract class EntityCar : EntityVehicle
    {
        public override int Capacity { get; set; }
        public override int Power { get; set; }
    }
}

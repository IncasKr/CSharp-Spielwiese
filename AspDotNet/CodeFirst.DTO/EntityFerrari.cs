using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.DTO
{
    public class EntityFerrari : EntityCar
    {
        public int ID { get; set; }
        public override string Color { get; set; }
        public int Capacity { get; set; }
        public int Power { get; set; }
        public override string Model { get; set; }
    }
}
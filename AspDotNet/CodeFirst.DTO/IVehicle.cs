using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.DTO
{
    public interface IVehicle
    {
        string Model { get; set; }
        string Color { get; set; }
    }
}

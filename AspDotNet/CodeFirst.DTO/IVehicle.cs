using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.DTO
{
    public interface IVehicle
    {
        int Capacity { get; set; }
        string Color { get; set; }
        string Model { get; set; }
        int Power { get; set; }
        int SeatNumber { get; set; }
        int WeelsNumber { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.DTO
{
    public abstract class EntityVehicle : IVehicle
    {
        public int Capacity { get; set; }
        public string Color { get; set; }
        public int ID { get; set; }
        public string Model { get; set; }
        public int Power { get; set; }
        public int SeatNumber { get; set; }
        public int WeelsNumber { get; set; }
        public virtual EntityGarage Garage { get; set; }
    }
}

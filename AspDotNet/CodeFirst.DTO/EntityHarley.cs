﻿namespace CodeFirst.DTO
{
    public class EntityHarley : EntityMotorBike
    {
        public int ID { get; set; }
        public override string Color { get; set; }
        public int Capacity { get; set; }
        public int Power { get; set; }
        public override string Model { get; set; }
    }
}

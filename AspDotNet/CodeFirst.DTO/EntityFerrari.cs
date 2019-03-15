using System;
using System.ComponentModel;

namespace CodeFirst.DTO
{
    public class EntityFerrari : EntityCar
    {
        [DefaultValue("false")]
        public bool Turbo { get; set; }
    }
}
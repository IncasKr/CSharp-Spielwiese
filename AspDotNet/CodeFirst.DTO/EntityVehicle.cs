using System.ComponentModel.DataAnnotations;

namespace CodeFirst.DTO
{
    public abstract class EntityVehicle : IVehicle
    {
        public int Capacity { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field can not be null or empty.")]
        public string Color { get; set; }

        [Key]
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field can not be null or empty.")]
        public string Model { get; set; }
        public int Power { get; set; }

        [Range(1, 8, ErrorMessage = "The seat number is between 1 and 8.")]
        public int SeatNumber { get; set; }

        [Range(1, 6, ErrorMessage = "The seat number is between 1 and 6.")]
        public int WeelsNumber { get; set; }
        public virtual EntityGarage Garage { get; set; }
    }
}

namespace VehicleInfoSystem.Models.Vehicles.Motors
{
    using System.ComponentModel.DataAnnotations;

    public class Car : Motor
    {
        [Required, Range(1, 4)]
        public int NumberOfDoors { get; set; }

        [Required]
        public bool HasInsurance { get; set; }
    }
}

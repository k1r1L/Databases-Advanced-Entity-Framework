namespace VehicleInfoSystem.Models.Vehicles.Motors
{
    using System.ComponentModel.DataAnnotations;

    public class Plane : Motor
    {
        [Required]
        public string AirlineOwner { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public int PassengersCapacity { get; set; }
    }
}

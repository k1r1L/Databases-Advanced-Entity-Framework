namespace VehicleInfoSystem.Models.Vehicles.Motors.Ships
{
    using System.ComponentModel.DataAnnotations;

    public class Cruise : Ship
    {
        [Required]
        public int PassengersCapacity { get; set; }
    }
}

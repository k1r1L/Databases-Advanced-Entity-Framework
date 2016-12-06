namespace VehicleInfoSystem.Models.Vehicles.Motors.Ships
{
    using System.ComponentModel.DataAnnotations;

    public class Cargo : Ship
    {
        [Required]
        public int MaxLoadKilograms { get; set; }
    }
}

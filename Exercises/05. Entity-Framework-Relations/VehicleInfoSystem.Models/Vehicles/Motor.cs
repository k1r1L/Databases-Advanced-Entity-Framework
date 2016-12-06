namespace VehicleInfoSystem.Models.Vehicles
{
    using System.ComponentModel.DataAnnotations;

    public abstract class Motor : Vehicle
    {
        [Required, Range(1, 8)]
        public int NumberOfEngines { get; set; }

        [Required]
        public EngineType EngineType { get; set; }

        [Required]
        public decimal TankCapacity { get; set; }
    }
}

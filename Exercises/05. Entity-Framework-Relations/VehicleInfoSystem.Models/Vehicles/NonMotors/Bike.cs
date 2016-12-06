namespace VehicleInfoSystem.Models.Vehicles.NonMotors
{
    using System.ComponentModel.DataAnnotations;

    public class Bike : NonMotor
    {
        [Required, Range(1, 7)]
        public int ShiftsCount { get; set; }

        [Required]
        public string Color { get; set; }
    }
}

namespace VehicleInfoSystem.Models.Carriages
{
    using System.ComponentModel.DataAnnotations;
    public class Passenger : Carriage
    {
        [Required]
        public int StandingPassengersCapacity { get; set; }
    }
}

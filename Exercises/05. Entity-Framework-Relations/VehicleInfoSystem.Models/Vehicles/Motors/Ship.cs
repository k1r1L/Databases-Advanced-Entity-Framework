namespace VehicleInfoSystem.Models.Vehicles.Motors
{
    using System.ComponentModel.DataAnnotations;

    public abstract class Ship : Motor
    {
        [Required]
        public string Nationality { get; set; }

        [Required, StringLength(50)]
        public string CaptainName { get; set; }

        [Required]
        public int SizeOfShipCrew { get; set; }
    }
}

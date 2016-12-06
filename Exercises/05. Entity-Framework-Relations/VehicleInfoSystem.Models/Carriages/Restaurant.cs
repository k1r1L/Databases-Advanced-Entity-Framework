namespace VehicleInfoSystem.Models.Carriages
{
    using System.ComponentModel.DataAnnotations;
    public class Restaurant : Carriage
    {
        [Required]
        public int TablesCount { get; set; }
    }
}

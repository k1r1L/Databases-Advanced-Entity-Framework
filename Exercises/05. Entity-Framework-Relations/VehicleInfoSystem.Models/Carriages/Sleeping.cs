namespace VehicleInfoSystem.Models.Carriages
{
    using System.ComponentModel.DataAnnotations;
    public class Sleeping : Carriage
    {
        [Required]
        public int BedsCount { get; set; }
    }
}

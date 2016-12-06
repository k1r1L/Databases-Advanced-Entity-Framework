namespace VehicleInfoSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Vehicles.Motors;

    public class Locomotive
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public decimal Power { get; set; }

        public virtual Train Train { get; set; }

        [ForeignKey("Train")]
        public int TrainId { get; set; }
    }
}

namespace VehicleInfoSystem.Models.Carriages
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Vehicles.Motors;
    public abstract class Carriage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PassengersSeatsCapacity { get; set; }

        public virtual Train Train { get; set; }

        [ForeignKey("Train")]
        public int TrainId { get; set; }
    }
}

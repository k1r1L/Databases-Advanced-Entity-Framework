using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInfoSystem.Models.Carriages;

namespace VehicleInfoSystem.Models.Vehicles.Motors
{
    public class Train : Motor
    {
        public Train()
        {
            this.Carriages = new HashSet<Carriage>();
        }

        public virtual Locomotive Locomotive { get; set; }

        [ForeignKey("Locomotive")]
        public int LocomotiveId { get; set; }

        public ICollection<Carriage> Carriages { get; set; }

        [Required]
        public int NumberOfCarriages { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassDefect.Models
{
    [Table("Anomalies")]
    public class Anomaly
    {
        public Anomaly()
        {
            this.Victims = new HashSet<Person>();
        }

        [Key]
        public int Id { get; set; }

        public virtual Planet OriginPlanet { get; set; }

        public virtual Planet TeleportPlanet { get; set; }

        [ForeignKey("OriginPlanet")]
        public int OriginPlanetId { get; set; }

        [ForeignKey("TeleportPlanet")]
        public int TeleportPlanetId { get; set; }

        public virtual ICollection<Person> Victims { get; set; }
    }
}

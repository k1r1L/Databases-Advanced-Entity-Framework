using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassDefect.Models
{
    public class Planet
    {
        public Planet()
        {
            this.People = new HashSet<Person>();
            this.Anomalies = new HashSet<Anomaly>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Star Sun { get; set; }

        [ForeignKey("Sun")]
        public int SunId { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        [ForeignKey("SolarSystem")]
        public int SolarSystemId { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<Anomaly> Anomalies { get; set; }
    }
}

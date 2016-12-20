namespace WeddingsPlanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Venue
    {
        public Venue()
        {
            this.Weddings = new HashSet<Wedding>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Town { get; set; }

        public virtual ICollection<Wedding> Weddings { get; set; }
    }
}

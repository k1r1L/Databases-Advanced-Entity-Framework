namespace FootballBookmarkerSystem.Models.Classes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Country
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
            this.Continents = new HashSet<Continent>();
        }

        [Key, RegularExpression("^[A-Z]{3}$")]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Continent> Continents { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}

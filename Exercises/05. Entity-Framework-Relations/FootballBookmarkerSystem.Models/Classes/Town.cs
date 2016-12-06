namespace FootballBookmarkerSystem.Models.Classes
{
    using FootballBookmarkerSystem.Classes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Town
    {
        public Town()
        {
            this.Teams = new HashSet<Team>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(60)]
        public string Name { get; set; }

        public virtual Country Country { get; set; }

        [ForeignKey("Country"), RegularExpression("^[A-Z]{3}$")]
        public string CountryId { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}

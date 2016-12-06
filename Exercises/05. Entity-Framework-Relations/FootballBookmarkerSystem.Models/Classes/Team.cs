namespace FootballBookmarkerSystem.Classes
{
    using Models.Classes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] Logo { get; set; }

        [Required, RegularExpression("^[A-Z]{3}$")]
        public string Initials { get; set; }

        public virtual Color PrimaryKitColor { get; set; }

        [ForeignKey("PrimaryKitColor")]
        public int PrimaryKitColorId { get; set; }

        public virtual Color SecondaryKitColor { get; set; }

        [ForeignKey("SecondaryKitColor")]
        public int SecondaryKitColorId { get; set; }

        public virtual Town Town { get; set; }

        [ForeignKey("Town")]
        public int TownId { get; set; }

        [Required]
        public decimal Budget { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}

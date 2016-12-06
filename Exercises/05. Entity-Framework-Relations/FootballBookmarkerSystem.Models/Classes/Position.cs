namespace FootballBookmarkerSystem.Models.Classes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Position
    {
        public Position()
        {
            this.Players = new HashSet<Player>();
        }

        [Key, RegularExpression("^[A-Z]{2}$")]
        public int Id { get; set; }

        [Required]
        public string PositionDescription { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}

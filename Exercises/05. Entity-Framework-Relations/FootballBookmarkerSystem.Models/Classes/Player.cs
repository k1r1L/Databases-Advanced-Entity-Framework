namespace FootballBookmarkerSystem.Models.Classes
{
    using FootballBookmarkerSystem.Classes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Player
    {
        public Player()
        {
            this.PlayerStatistics = new HashSet<PlayerStatistic>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Range(1, 99)]
        public int SquadNumber { get; set; }

        public virtual Team Team { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public virtual Position Position { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }

        public virtual ICollection<PlayerStatistic> PlayerStatistics { get; set; }

        [Required]
        public bool IsInjured { get; set; }
    }
}

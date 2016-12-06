namespace FootballBookmarkerSystem.Models.Classes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PlayerStatistic
    {
        [Key]
        [Column(Order = 0)]
        public int PlayerId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int GameId { get; set; }

        public virtual Player Player { get; set; }

        public virtual Game Game { get; set; }

        [Required]
        public int ScoredGoals { get; set; }

        [Required]
        public int PlayerAssists { get; set; }

        [Required]
        public int PlayedMinutes { get; set; }
    }
}

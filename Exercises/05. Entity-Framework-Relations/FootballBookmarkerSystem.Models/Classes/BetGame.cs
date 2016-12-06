namespace FootballBookmarkerSystem.Models.Classes
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BetGame
    {
        [Key]
        [Column(Order = 0)]
        public int BetId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int GameId { get; set; }

        public virtual Bet Bet { get; set; }

        public virtual Game Game { get; set; }

        [Required]
        public ResultPrediction ResultPrediction { get; set; }
    }
}

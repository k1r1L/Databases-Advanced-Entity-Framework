namespace FootballBookmarkerSystem.Models.Classes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Bet
    {
        public Bet()
        {
            this.BetGames = new HashSet<BetGame>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal BetMoney { get; set; }

        [Required]
        public DateTime TimeOfBet { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<BetGame> BetGames { get; set; }
    }
}

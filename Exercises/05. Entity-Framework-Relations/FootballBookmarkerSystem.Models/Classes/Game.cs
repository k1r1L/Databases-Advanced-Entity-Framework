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

    public class Game
    {
        public Game()
        {
            this.PlayerStatistics = new HashSet<PlayerStatistic>();
            this.BetGames = new HashSet<BetGame>();
        }

        [Key]
        public int Id { get; set; }

        public virtual Team HomeTeam { get; set; }

        [ForeignKey("HomeTeam")]
        public int HomeTeamId { get; set; }

        public virtual Team AwayTeam { get; set; }

        [ForeignKey("AwayTeam")]
        public int AwayTeamId { get; set; }

        [Required]
        public int HomeGoals { get; set; }

        [Required]
        public int AwayGoals { get; set; }

        [Required]
        public DateTime GameDateTime { get; set; }

        [Required]
        public decimal HomeTeamWinBetRate { get; set; }

        [Required]
        public decimal AwayTeamWinBetRate { get; set; }

        [Required]
        public decimal DrawGameBetRate { get; set; }

        public virtual Round Round { get; set; }

        [ForeignKey("Round")]
        public int RoundId { get; set; }

        public virtual Competition Competition { get; set; }

        [ForeignKey("Competition")]
        public int CompetitionId { get; set; }

        public virtual ICollection<PlayerStatistic> PlayerStatistics { get; set; }

        public virtual ICollection<BetGame> BetGames { get; set; }
    }
}

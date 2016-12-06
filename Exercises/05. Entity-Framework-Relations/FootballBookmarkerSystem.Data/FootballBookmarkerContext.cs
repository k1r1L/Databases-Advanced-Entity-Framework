namespace FootballBookmarkerSystem.Data
{
    using Classes;
    using Models.Classes;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FootballBookmarkerContext : DbContext
    {
        public FootballBookmarkerContext()
            : base("name=FootballBookmarkerContext")
        {
            Database.SetInitializer(
                new DropCreateDatabaseAlways<FootballBookmarkerContext>());
        }

        public IDbSet<Bet> Bets { get; set; }

        public IDbSet<BetGame> BetGames { get; set; }

        public IDbSet<Color> Colors { get; set; }

        public IDbSet<Competition> Competitions { get; set; }

        public IDbSet<Continent> Continents { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Game> Games { get; set; }

        public IDbSet<Player> Players { get; set; }

        public IDbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public IDbSet<Position> Positions { get; set; }

        public IDbSet<Round> Rounds { get; set; }

        public IDbSet<Team> Teams { get; set; }

        public IDbSet<Town> Towns { get; set; }

        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Team relationships
            modelBuilder.Entity<Team>()
                .HasRequired(t => t.PrimaryKitColor)
                .WithMany(c => c.Teams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasRequired(t => t.SecondaryKitColor)
                .WithMany()
                .HasForeignKey(t => t.SecondaryKitColorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasRequired(t => t.Town)
                .WithMany(t => t.Teams)
                .HasForeignKey(t => t.TownId)
                .WillCascadeOnDelete(false);

            // Town relationships
            modelBuilder.Entity<Town>()
                .HasRequired(t => t.Country)
                .WithMany(c => c.Towns)
                .HasForeignKey(t => t.CountryId)
                .WillCascadeOnDelete(false);

            // Country-Continent relationship
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Continents)
                .WithMany(c => c.Countries)
                .Map(cc =>
                {
                    cc.MapLeftKey("CountryId");
                    cc.MapRightKey("ContinentId");
                    cc.ToTable("CountriesContinents");
                });

            //Player relationships
            modelBuilder.Entity<Player>()
                .HasRequired(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<Player>()
                .HasRequired(p => p.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.PositionId);


            //Player-Games many-to-many
            modelBuilder.Entity<PlayerStatistic>()
                .HasRequired(ps => ps.Player)
                .WithMany(p => p.PlayerStatistics);

            modelBuilder.Entity<PlayerStatistic>()
                .HasRequired(ps => ps.Game)
                .WithMany(g => g.PlayerStatistics);

            //Game relationships
            modelBuilder.Entity<Game>()
                .HasRequired(g => g.HomeTeam)
                .WithMany(t => t.Games)
                .HasForeignKey(g => g.HomeTeamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasRequired(g => g.AwayTeam)
                .WithMany()
                .HasForeignKey(g => g.AwayTeamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasRequired(g => g.Round)
                .WithMany(r => r.Games)
                .HasForeignKey(g => g.RoundId);
            //.WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasRequired(g => g.Competition)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CompetitionId);
                //.WillCascadeOnDelete(false);

            // Games-Bets many-to-many
            modelBuilder.Entity<BetGame>()
                .HasRequired(bg => bg.Bet)
                .WithMany(b => b.BetGames);

            modelBuilder.Entity<BetGame>()
                .HasRequired(bg => bg.Game)
                .WithMany(g => g.BetGames);

            //Bet relationships
            modelBuilder.Entity<Bet>()
                .HasRequired(b => b.User)
                .WithMany(u => u.Bets);

            base.OnModelCreating(modelBuilder);

        }
    }
}
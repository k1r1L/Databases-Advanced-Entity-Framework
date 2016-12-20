namespace WeddingsPlanner.Data
{
    using Models;
    using Models.Presents;
    using System.Data.Entity;

    public class WeddingsPlannerContext : DbContext
    {
        public WeddingsPlannerContext()
            : base("name=WeddingsPlannerContext")
        {
        }

        public virtual DbSet<Agency> Agencies { get; set; }

        public virtual DbSet<Venue> Venues { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Wedding> Weddings { get; set; }

        public virtual DbSet<Invitation> Invitations { get; set; }

        public virtual DbSet<Present> Presents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wedding>()
                .HasMany(weding => weding.Venues)
                .WithMany(venue => venue.Weddings)
                .Map(map => 
                {
                    map.MapLeftKey("WeddingId");
                    map.MapRightKey("VenueId");
                    map.ToTable("WeddingVenues");
                });

            modelBuilder.Entity<Wedding>()
                .HasRequired(wed => wed.Bride)
                .WithMany(p => p.BrideWeddings)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wedding>()
                .HasRequired(wed => wed.Bridegroom)
                .WithMany(p => p.BrideGroomWeddings)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Gift>().ToTable("Gifts");
            modelBuilder.Entity<Cash>().ToTable("Money");

            base.OnModelCreating(modelBuilder);
        }
    }
}
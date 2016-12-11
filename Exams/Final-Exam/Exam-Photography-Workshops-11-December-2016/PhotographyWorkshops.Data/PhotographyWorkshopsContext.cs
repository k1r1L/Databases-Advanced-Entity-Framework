namespace PhotographyWorkshops.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhotographyWorkshopsContext : DbContext
    {
        public PhotographyWorkshopsContext()
            : base("name=PhotographyWorkshopsContext")
        {
        }

        public DbSet<Lens> Lenses { get; set; }

        public DbSet<Camera> Cameras { get; set; }

        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<Photographer> Photographers { get; set; }

        public DbSet<Workshop> Workshops { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photographer>()
                .HasRequired(photographer => photographer.PrimaryCamera)
                .WithMany(camera => camera.PrimaryPhotographers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
              .HasRequired(photographer => photographer.SecondaryCamera)
              .WithMany(camera => camera.SecondaryPhotographers)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Workshop>()
                .HasRequired(workshop => workshop.Trainer)
                .WithMany(trainer => trainer.TrainerWorkshops)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Workshop>()
                .HasMany(workshop => workshop.Participants)
                .WithMany(participant => participant.ParicipantWorkshops)
                .Map(map =>
                {
                    map.MapLeftKey("WorkshopId");
                    map.MapRightKey("ParticipantId");
                    map.ToTable("WorkshopParticipants");
                });

            // Adding precision to MaxAperture
            modelBuilder.Entity<Lens>()
                .Property(lens => lens.MaxAperture)
                .HasPrecision(16, 1);

            // TPT Strategy
            modelBuilder.Entity<DslrCamera>().ToTable("DslrCameras");
            modelBuilder.Entity<MirrorlessCamera>().ToTable("MirrorlessCameras");


            base.OnModelCreating(modelBuilder);
        }
    }
}
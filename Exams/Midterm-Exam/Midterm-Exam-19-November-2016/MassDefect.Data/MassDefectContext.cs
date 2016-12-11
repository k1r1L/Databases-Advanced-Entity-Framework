namespace MassDefect.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MassDefectContext : DbContext
    {
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
        }

        public virtual IDbSet<SolarSystem> SolarSystems { get; set; }

        public virtual IDbSet<Star> Stars { get; set; }

        public virtual IDbSet<Planet> Planets { get; set; }

        public virtual IDbSet<Person> People { get; set; }

        public virtual IDbSet<Anomaly> Anomalies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>()
                .HasMany(a => a.Victims)
                .WithMany(p => p.Anomalies)
                .Map(m =>
                {
                    m.MapLeftKey("AnomalyId");
                    m.MapRightKey("PersonId");
                    m.ToTable("AnomalyVictims");
                });

            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.OriginPlanet)
                .WithMany(p => p.Anomalies)
                .HasForeignKey(a => a.OriginPlanetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.TeleportPlanet)
                .WithMany()
                .HasForeignKey(a => a.TeleportPlanetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Star>()
                .HasRequired(s => s.SolarSystem)
                .WithMany(s => s.Stars)
                .HasForeignKey(s => s.SolarSystemId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
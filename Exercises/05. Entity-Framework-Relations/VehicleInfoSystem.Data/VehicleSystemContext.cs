namespace VehicleInfoSystem.Data
{
    using Models;
    using Models.Carriages;
    using Models.Vehicles;
    using Models.Vehicles.Motors;
    using Models.Vehicles.Motors.Ships;
    using Models.Vehicles.NonMotors;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class VehicleSystemContext : DbContext
    {
        public VehicleSystemContext()
            : base("name=VehicleSystemContext")
        {
        }

        public IDbSet<Vehicle> Vehicles { get; set; }

        public IDbSet<Carriage> Carriages { get; set; }

        public IDbSet<Locomotive> Locomotives { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Train>()
                .HasRequired(t => t.Locomotive)
                .WithRequiredPrincipal(l => l.Train);

            modelBuilder.Entity<Carriage>()
                .HasRequired(c => c.Train)
                .WithMany(t => t.Carriages);

            modelBuilder.Entity<Motor>().ToTable("MotorVehicles");
            modelBuilder.Entity<NonMotor>().ToTable("NonMotorVehicles");
            modelBuilder.Entity<Bike>().ToTable("Bikes");
            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Plane>().ToTable("Plains");
            modelBuilder.Entity<Train>().ToTable("Trains");
            modelBuilder.Entity<Ship>().ToTable("Ships");
            modelBuilder.Entity<Cargo>().ToTable("CargoShips");
            modelBuilder.Entity<Cruise>().ToTable("CruiseShips");

            base.OnModelCreating(modelBuilder);
        }
    }
}
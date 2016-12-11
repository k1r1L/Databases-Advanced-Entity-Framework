namespace CarDealer.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CarDealerContext : DbContext
    {
        public CarDealerContext()
            : base("name=CarDealerContext")
        {
        }

        public virtual IDbSet<Car> Cars { get; set; }

        public virtual IDbSet<Customer> Customers { get; set; }

        public virtual IDbSet<Part> Parts { get; set; }

        public virtual IDbSet<Sale> Sales { get; set; }

        public virtual IDbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Part>()
                .HasMany(p => p.Cars)
                .WithMany(c => c.Parts)
                .Map(m =>
                {
                    m.MapLeftKey("PartId");
                    m.MapRightKey("CarId");
                    m.ToTable("PartsCars");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
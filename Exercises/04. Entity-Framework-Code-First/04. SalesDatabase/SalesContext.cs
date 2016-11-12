namespace _04.SalesDatabase
{
    using Migrations;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SalesContext : DbContext
    {
        
        public SalesContext()
            : base("name=SalesContext")
        {
            Database.SetInitializer
                (new DropCreateDatabaseAlways<SalesContext>());
        }

        public IDbSet<Sale> Sales { get; set; }

        public IDbSet<Customer> Customers { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<StoreLocation> StoreLocations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
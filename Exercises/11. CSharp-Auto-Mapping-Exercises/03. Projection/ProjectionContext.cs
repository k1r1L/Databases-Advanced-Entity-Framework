namespace _03.Projection
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProjectionContext : DbContext
    {
        public ProjectionContext()
            : base("name=ProjectionContext")
        {
        }

        public IDbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.Manager);

            base.OnModelCreating(modelBuilder);
        }
    }
}
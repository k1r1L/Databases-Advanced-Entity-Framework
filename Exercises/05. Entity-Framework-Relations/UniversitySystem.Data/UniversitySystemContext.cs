namespace UniversitySystem.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UniversitySystemContext : DbContext
    {
        public UniversitySystemContext()
            : base("name=UniversitySystemContext")
        {
        }

        public IDbSet<Person> People { get; set; }

        public IDbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");

            base.OnModelCreating(modelBuilder);
        }
    }
}
namespace StudentSystem.Data
{
    using Migrations;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
            : base("name=StudentSystemContext")
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<StudentSystemContext>());
        }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Homework> Homeworks { get; set; }

        public IDbSet<Resource> Resources { get; set; }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<License> Licenses { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Picture> Pictures { get; set; }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany();

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Users)
                .WithMany(u => u.Albums)
                .Map(au =>
                {
                    au.MapLeftKey("AlbumId");
                    au.MapRightKey("UserId");
                    au.ToTable("AlbumUsers");
                });

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Pictures)
                .WithMany(p => p.Albums)
                .Map(ap =>
                {
                    ap.MapLeftKey("AlbumId");
                    ap.MapRightKey("PictureId");
                    ap.ToTable("AlbumPictures");
                });

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Tags)
                .WithMany(t => t.Albums)
                .Map(at =>
                {
                    at.MapLeftKey("AlbumId");
                    at.MapRightKey("TagId");
                    at.ToTable("AlbumTags");
                });

            modelBuilder.Entity<AlbumUser>()
                .HasRequired(a => a.Album)
                .WithMany(a => a.AlbumUsers);


            modelBuilder.Entity<AlbumUser>()
                .HasRequired(a => a.User)
                .WithMany(a => a.AlbumUsers);

            base.OnModelCreating(modelBuilder);
        }
    }
}
namespace StudentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class User
    {
        public User()
        {
            this.Friends = new HashSet<User>();
            this.Albums = new HashSet<Album>();
            this.AlbumUsers = new HashSet<AlbumUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MinLength(4), MaxLength(35)]
        public string Username { get; set; }

        [Required, MinLength(5), MaxLength(50)]
        public string Password { get; set; }

        public virtual ICollection<User> Friends { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<AlbumUser> AlbumUsers { get; set; }
    }
}

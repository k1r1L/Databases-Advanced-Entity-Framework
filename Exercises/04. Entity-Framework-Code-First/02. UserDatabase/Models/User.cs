namespace _02.UserDatabase.Models
{
    using Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class User
    {
        
        private string password;

        [Key]
        public int UserId { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        [Required, MinLength(4), MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [Password(6, 50, ContainsUppercase = false, ContainsLowercase = true, ContainsSpecialSymbol = true, ContainsDigit = true)]
        public string Password { get; set; }

        [Required]
        [Email]
        public string Email { get; set; }

        [MaxLength(1024 * 1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }
    }
}

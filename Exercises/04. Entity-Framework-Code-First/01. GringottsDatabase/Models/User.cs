namespace _01.GringottsDatabase.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(Constants.UsernameStringMinLength), MaxLength(Constants.UsernameStringMaxLength)]
        public string Username { get; set; }

        [Required]
        [MinLength(Constants.PasswordMinLength)]
        [MaxLength(Constants.PasswordMaxLength)]
        [RegularExpression(@"^[A-Z]+[a-z]+[0-9]+[!@#$%^&*()_<>?]+$")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^(?<user>([A-Za-z\d]+)([._-][A-Za-z\d]+)*)@(?<host>([A-Za-z\d]+)([.][A-Za-z\d]+)+)$")]
        public string Email { get; set; }

        [MaxLength(Constants.ProfilePictureSize)]
        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        [Range(Constants.UserMinAge, Constants.UserMaxAge)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }
    }
}

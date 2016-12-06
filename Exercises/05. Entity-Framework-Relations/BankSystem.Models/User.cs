namespace BankSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BankSystem.Models.Contracts;
    using Attributes;

    public class User : IUser
    {
        public User()
        {

        }

        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.BankAccounts = new HashSet<BankingAccount>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Username]
        public string Username { get; set; }

        [Required, Password]
        public string Password { get; set; }

        [Required, Email]
        public string Email { get; set; }

        public ICollection<BankingAccount> BankAccounts { get; set; }
    }
}

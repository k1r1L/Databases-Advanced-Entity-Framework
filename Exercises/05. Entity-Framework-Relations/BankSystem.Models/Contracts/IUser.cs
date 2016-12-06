namespace BankSystem.Models.Contracts
{
    using System.Collections.Generic;

    public interface IUser
    {
        int Id { get; set; }

        string Username { get; set; }

        string Password { get; set; }

        string Email { get; set; }

        ICollection<BankingAccount> BankAccounts { get; set; }
    }
}

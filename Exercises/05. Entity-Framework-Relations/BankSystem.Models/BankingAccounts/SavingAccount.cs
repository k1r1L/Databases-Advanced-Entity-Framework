namespace BankSystem.Models.BankingAccounts
{
    using Contracts;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SavingsAccount : BankingAccount, ISavingAccount
    {
        public SavingsAccount()
        {

        }

        public SavingsAccount(string accountNumber, decimal balance, decimal interestRate) 
            : base(accountNumber, balance)
        {
            this.InterestRate = interestRate;
        }

        [Required]
        public decimal InterestRate { get; set; }

        public void AddInterest()
        {
            this.Balance += this.Balance * this.InterestRate;
        }
    }
}

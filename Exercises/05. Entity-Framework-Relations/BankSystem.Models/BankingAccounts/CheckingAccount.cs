namespace BankSystem.Models.BankingAccounts
{
    using Contracts;
    using System.ComponentModel.DataAnnotations;

    public class CheckingAccount : BankingAccount, ICheckingAccount
    {
        public CheckingAccount()
        {

        }
        public CheckingAccount(string accountNumber, decimal balance, decimal fee) 
            : base(accountNumber, balance)
        {
            this.Fee = fee;
        }

        [Required]
        public decimal Fee { get; set; }

        public void DeductFee()
        {
            this.Balance -= this.Fee;
        }
    }
}

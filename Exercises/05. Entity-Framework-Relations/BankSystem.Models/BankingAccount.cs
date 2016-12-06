namespace BankSystem.Models
{
    using Contracts;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BankingAccount : IBankingAccount
    {
        private decimal balance;
        public BankingAccount()
        {

        }

        public BankingAccount(string accountNumber, decimal balance)
        {
            this.AccountNumber = accountNumber;
            this.Balance = balance;
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(10)]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Balance
        {
            get { return this.balance; }
            set
            {
                if (value < 0)
                {
                    this.balance = 0;
                    return;
                }

                this.balance = value;
            }
        }

        public void DepositMoney(decimal money)
        {
            if (money < 0)
            {
                throw new InvalidOperationException("Cannot deposit negative amount of money!");
            }

            this.Balance += money;
        }

        public void WithdrawMoney(decimal money)
        {
            if (money < 0)
            {
                throw new InvalidOperationException("Cannot withdraw negative amount of money!");
            }

            this.Balance -= money;
        }

        public virtual User Owner { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

    }
}

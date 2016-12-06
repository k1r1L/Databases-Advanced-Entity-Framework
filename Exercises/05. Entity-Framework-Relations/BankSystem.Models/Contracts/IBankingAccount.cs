namespace BankSystem.Models.Contracts
{
    public interface IBankingAccount
    {
        int Id { get; set; }

        string AccountNumber { get; set; }

        decimal Balance { get; set; }

        void DepositMoney(decimal money);

        void WithdrawMoney(decimal money);

    }
}

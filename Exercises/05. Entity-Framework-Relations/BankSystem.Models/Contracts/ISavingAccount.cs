namespace BankSystem.Models.Contracts
{
    public interface ISavingAccount
    {
        decimal InterestRate { get; set; }

        void AddInterest();
    }
}

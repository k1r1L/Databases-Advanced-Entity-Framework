namespace BankSystem.Models.Contracts
{
    public interface ICheckingAccount
    {
        decimal Fee { get; set; }

        void DeductFee();
    }
}

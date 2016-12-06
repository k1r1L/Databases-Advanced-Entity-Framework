namespace BankSystem.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string password = value.ToString();
            bool containsLower = password.Any(symbol => char.IsLower(symbol));
            bool containsUpper = password.Any(symbol => char.IsUpper(symbol));
            bool containsDigit = password.Any(symbol => char.IsDigit(symbol));
            bool isMoreThanSixSymbols = password.Length > 6;

            return containsLower && containsUpper && containsLower && isMoreThanSixSymbols;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Incorrect password";
        }
    }
}

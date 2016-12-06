namespace BankSystem.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class UsernameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string pattern = @"^([A-Za-z][A-Za-z\d]*){3}$";
            string username = value.ToString();

            return Regex.IsMatch(username, pattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return "Incorrect username";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsValid(value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Incorrect username");
        }
    }
}

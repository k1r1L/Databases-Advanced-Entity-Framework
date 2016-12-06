namespace BankSystem.Models.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string email = value.ToString();
            string pattern = @"^(?<user>[A-Za-z\d]+([._-][A-Za-z\d]+)*)@(?<host>([\-]*[A-Za-z]+)+(\.([\-]*[A-Za-z]+)*)+)$";
            Regex emailMatcher = new Regex(pattern);

            return emailMatcher.IsMatch(email);
        }

        public override string FormatErrorMessage(string name)
        {
            return "Incorrect email";
        }
    }
}

namespace _02.UserDatabase.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PasswordAttribute : ValidationAttribute
    {
        private char[] symbolsForPassword = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '<', '>', '?' };
        public PasswordAttribute(int minPasswordLength, int maxPasswordLength) 
        {
            this.MinPasswordLength = minPasswordLength;
            this.MaxPasswordLength = maxPasswordLength;
        }

        public int MinPasswordLength { get; set; }

        public int MaxPasswordLength { get; set; }

        public bool ContainsLowercase { get; set; }

        public bool ContainsUppercase { get; set; }

        public bool ContainsDigit { get; set; }

        public bool ContainsSpecialSymbol { get; set; }

        public override bool IsValid(object value)
        {
            string password = value.ToString();
            bool isValid = false;
            bool validPasswordLength = password.Length >= this.MinPasswordLength 
                && password.Length <= this.MaxPasswordLength;

            if (this.ContainsLowercase)
            {
                bool hasLower = password.Any(s => char.IsLower(s));
                isValid = hasLower;
                if (isValid == false)
                {
                    return false;
                }
            }

            if (this.ContainsUppercase)
            {
                bool hasUpper = password.Any(s => char.IsUpper(s));
                isValid = hasUpper;
                if (isValid == false)
                {
                    return false;
                }
            }

            if (this.ContainsDigit)
            {
                bool hasDigit = password.Any(s => char.IsDigit(s));
                isValid = hasDigit;
                if (isValid == false)
                {
                    return false;
                }
            }

            if (this.ContainsSpecialSymbol)
            {
                bool hasSpecialSymbol = password.Any(s => symbolsForPassword.Contains(s));
                isValid = hasSpecialSymbol;
                if (isValid == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

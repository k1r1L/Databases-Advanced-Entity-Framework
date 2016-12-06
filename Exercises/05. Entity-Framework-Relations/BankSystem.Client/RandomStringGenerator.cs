namespace BankSystem.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class RandomStringGenerator
    {
        public static string GenerateRandomAccountNumber()
        {
            Guid guid = Guid.NewGuid();
            string guidString = Convert.ToBase64String(guid.ToByteArray());
            guidString = Regex.Replace(guidString, @"[^A-Za-z\d]", "");
            string accountNumber = guidString.Substring(0, 10).ToUpper();

            return accountNumber;
        }
    }
}

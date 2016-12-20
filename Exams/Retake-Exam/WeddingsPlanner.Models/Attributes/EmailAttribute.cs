namespace WeddingsPlanner.Models.Attributes
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
            if (value == null)
            {
                return true;
            }

            string email = value.ToString();

            if (Regex.IsMatch(email, @"^[a-zA-Z0-9]+@[a-z]+\.[a-z]+$") || email == null)
            {
                return true;
            }

            return false;
        }
    }
}

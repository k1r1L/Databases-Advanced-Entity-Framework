namespace _02.UserDatabase.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string email = value.ToString();
            string pattern = @"^(?<user>[A-Za-z\d]+([._-][A-Za-z\d]+)*)@(?<host>[\w]+(\.[\w]+)+)$";
            Regex emailMatcher = new Regex(pattern);

            return emailMatcher.IsMatch(email);
        }
    }
}

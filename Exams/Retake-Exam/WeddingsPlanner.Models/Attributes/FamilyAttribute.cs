namespace WeddingsPlanner.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class FamilyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string family = value.ToString();
            if (family == "Bride" || family == "Bridegroom")
            {
                return true;
            }

            return false;
        }
    }
}

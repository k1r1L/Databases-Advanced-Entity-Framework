namespace PhotographyWorkshops.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class MinIsoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int minIso = (int)value;

            if (minIso < 100)
            {
                return false;
            }

            return true;
        }
    }
}

namespace WeddingsPlanner.Models.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GenderAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string gender = value.ToString();

            if (gender == "Male" || gender == "Female" || gender == "NotSpecified")
            {
                return true;
            }

            return false;
        }
    }
}

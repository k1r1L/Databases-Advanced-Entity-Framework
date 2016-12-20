namespace WeddingsPlanner.Models.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GiftSizeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string giftSize = value.ToString();

            if (giftSize == "Small" || giftSize == "Medium" || giftSize == "Large" || giftSize == "NotSpecified")
            {
                return true;
            }

            return false;
        }
    }
}

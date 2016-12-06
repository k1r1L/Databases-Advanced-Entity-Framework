namespace BillPaymentSystem.Models.Models.BillingDetails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CreditCard : BillingDetail
    {
        [Required]
        public string CardType { get; set; }

        [Required, Range(1, 12)]
        public int ExpirationMonth { get; set; }

        [Required, Range(2016, 2030)]
        public int ExpirationYear { get; set; }
    }
}

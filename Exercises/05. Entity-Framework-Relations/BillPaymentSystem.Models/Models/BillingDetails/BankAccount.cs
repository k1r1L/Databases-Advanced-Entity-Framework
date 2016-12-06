namespace BillPaymentSystem.Models.Models.BillingDetails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BankAccount : BillingDetail
    {
        [Required]
        public string BankName { get; set; }

        [Required, MinLength(8), MaxLength(11)]
        public string SwiftCode { get; set; }
    }
}

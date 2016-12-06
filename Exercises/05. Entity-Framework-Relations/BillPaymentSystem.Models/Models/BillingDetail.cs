namespace BillPaymentSystem.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class BillingDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        public virtual User Owner { get; set; }

        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
    }
}

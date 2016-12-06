namespace BillPaymentSystem.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class User
    {
        public User()
        {
            this.BillingDetails = new HashSet<BillingDetail>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(5), MaxLength(30)]
        public string Password { get; set; }

        public virtual ICollection<BillingDetail> BillingDetails { get; set; }
    }
}

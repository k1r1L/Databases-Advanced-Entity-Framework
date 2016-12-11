namespace CarDealer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Sale
    {
        private Customer customer;

        public int Id { get; set; }

        [Required]
        public decimal Discount { get; set; }

        public int CarId { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer
        {
            get { return this.customer; }
            set
            {
                if (value.IsYoungDriver)
                {
                    this.Discount += 0.05m;
                }

                this.customer = value;
            }
        }
    }
}

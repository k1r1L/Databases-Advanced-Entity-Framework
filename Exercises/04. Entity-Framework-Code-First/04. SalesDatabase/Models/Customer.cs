namespace _04.SalesDatabase.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Customer
    {
        public Customer()
        {
            this.SalesOfProduct = new HashSet<Sale>();
        }

        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [StringLength(20)]
        [Column(TypeName = "VARCHAR")]
        public string CreditCardNumber { get; set; }

        public ICollection<Sale> SalesOfProduct { get; set; }
    }
}

namespace _04.SalesDatabase.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Product
    {
        public Product()
        {
            this.SalesForProduct = new HashSet<Sale>();
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<Sale> SalesForProduct { get; set; }
    }
}

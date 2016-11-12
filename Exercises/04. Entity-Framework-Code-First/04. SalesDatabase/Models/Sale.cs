namespace _04.SalesDatabase.Models
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
        [Key]
        public int SaleId { get; set; }

        public Product Product { get; set; }

        public Customer Customer { get; set; }

        public StoreLocation StoreLocation { get; set; }

        public DateTime? Date { get; set; }

        public decimal PriceOfSale { get; set; }
    }
}

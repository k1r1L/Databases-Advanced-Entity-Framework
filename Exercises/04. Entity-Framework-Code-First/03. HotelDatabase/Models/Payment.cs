using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.HotelDatabase.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        public DateTime? FirstDateOccupied { get; set; }

        public DateTime? LastDateOccupied { get; set; }

        [Required]
        public int TotalDays { get; set; }

        [Required]
        [Range(10, 100)]
        public decimal AmountCharged { get; set; }

        public decimal TaxRate { get; set; }

        public decimal TaxAmount { get; set; }

        [Required]
        public decimal PaymentTotal { get; set; }

        public string Notes { get; set; }
    }
}

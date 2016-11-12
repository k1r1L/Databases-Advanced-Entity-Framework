using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.HotelDatabase.Models
{
    public class Customer
    {
        [Key]
        public string AccountNumber { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string PhoneNumber { get; set; }

        public string EmergencyName { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string EmergencyNumber { get; set; }

        public string Notes { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.HotelDatabase.Models
{
    public class BedType
    {
        [Key]
        [Required]
        public string Type { get; set; }

        public string Notes { get; set; }
    }
}

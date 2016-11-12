using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.HotelDatabase.Models
{
    public class RoomStatus
    {
        [Key]
        [Required]
        public string Status { get; set; }

        public string Notes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.HotelDatabase.Models
{
    public class Occupancy
    {
        [Key]
        public int OccupancyId { get; set; }

        [Required]
        public DateTime DateOccupied { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Range(0.1, 5.00)]
        public decimal RateApplied { get; set; }

        public decimal PhoneCharge { get; set; }

        public string Notes { get; set; }
    }
}

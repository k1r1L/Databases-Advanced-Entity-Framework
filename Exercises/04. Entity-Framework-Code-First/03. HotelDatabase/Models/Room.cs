using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.HotelDatabase.Models
{
    public class Room
    {
        [Key]
        public int RoomNumber { get; set; }

        public RoomType RoomType { get; set; }

        public BedType BedType { get; set; }

        [Range(0.1, 5.00)]
        public decimal Rate { get; set; }

        public RoomStatus RoomStatus { get; set; }

        public string Notes { get; set; }
    }
}

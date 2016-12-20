namespace WeddingsPlanner.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WeddingDto
    {
        public string Bride { get; set; }

        public string Bridegroom { get; set; }

        public DateTime? Date { get; set; }

        public string Agency { get; set; }

        public ICollection<GuestDto> Guests { get; set; }
    }
}

namespace WeddingsPlanner.Models.Presents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Cash : Present
    {
        [Required]
        public int Amount { get; set; }
    }
}

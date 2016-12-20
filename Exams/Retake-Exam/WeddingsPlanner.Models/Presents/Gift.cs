namespace WeddingsPlanner.Models.Presents
{
    using Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Gift : Present
    {
        [Required]
        public string Name { get; set; }

        [GiftSize]
        public string Size { get; set; }
    }
}

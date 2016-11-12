namespace _04.SalesDatabase.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StoreLocation
    {
        public StoreLocation()
        {
            this.SalesForLocation = new HashSet<Sale>();
        }

        [Key]
        public int StoreLocationId { get; set; }

        [Required]
        public string LocationName { get; set; }

        public ICollection<Sale> SalesForLocation { get; set; }
    }
}

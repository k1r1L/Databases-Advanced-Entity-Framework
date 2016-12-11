namespace CarDealer.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Part
    {
        public Part()
        {
            this.Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [JsonIgnore]
        public int SupplierId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Car> Cars { get; set; }

        [JsonIgnore, ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

    }
}

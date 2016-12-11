namespace CarDealer.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    public class Car
    {
        private decimal price;
        public Car()
        {
            this.Sales = new HashSet<Sale>();
            this.Parts = new HashSet<Part>();
        }

        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public decimal Price { get; set; }

        [Required]
        public long TravelledDistance { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sale> Sales { get; set; }

        [JsonIgnore]
        public virtual ICollection<Part> Parts { get; set; }

        public decimal CalculatePrice()
        {
            decimal sum = this.Parts.Sum(p => p.Price);

            return sum;
        }
    }
}

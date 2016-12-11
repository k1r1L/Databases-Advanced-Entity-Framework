﻿namespace ProductsShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public User()
        {
            this.ProductsBought = new HashSet<Product>();
            this.ProductsSold = new HashSet<Product>();
            this.Friends = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? Age { get; set; }

        [InverseProperty("Seller")]
        public virtual ICollection<Product> ProductsSold { get; set; }

        [InverseProperty("Buyer")]
        public virtual ICollection<Product> ProductsBought { get; set; }

        public virtual ICollection<User> Friends { get; set; }
    }
}

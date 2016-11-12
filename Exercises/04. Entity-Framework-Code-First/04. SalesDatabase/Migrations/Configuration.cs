namespace _04.SalesDatabase.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_04.SalesDatabase.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_04.SalesDatabase.SalesContext";
        }

        protected override void Seed(SalesContext db)
        {
            if (db.Sales.Any())
            {
                return;
            }

            Customer pesho = new Customer()
            {
                Name = "Pesho",
                Email = "petar@abv.bg",
                CreditCardNumber = "123534645",
            };

            StoreLocation ruskiPametnik = new StoreLocation()
            {
                LocationName = "Ruski Pametnik"
            };

            Product treva = new Product()
            {
                Name = "Blaze it",
                Quantity = 420,
                Price = 500m,
            };

            Sale buyNow = new Sale()
            {
                Product = treva,
                StoreLocation = ruskiPametnik,
                Customer = pesho,
                Date = DateTime.Now
            };

            pesho.SalesOfProduct.Add(buyNow);
            treva.SalesForProduct.Add(buyNow);
            ruskiPametnik.SalesForLocation.Add(buyNow);


            db.Customers.Add(pesho);
            db.Products.Add(treva);
            db.StoreLocations.Add(ruskiPametnik);
            db.Sales.Add(buyNow);


            db.SaveChanges();
        }
    }
}

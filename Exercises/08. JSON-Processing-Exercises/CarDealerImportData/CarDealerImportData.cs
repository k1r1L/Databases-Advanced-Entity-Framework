namespace CarDealerImportData
{
    using CarDealer.Data;
    using CarDealer.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;


    public class CarDealerImportData
    {
        private const string RootDirectory = "../../";
        private const string SuppliersJsonDirectory = RootDirectory + "/data/suppliers.json";
        private const string PartsJsonDirectory = RootDirectory + "/data/parts.json";
        private const string CarsJsonDirectory = RootDirectory + "/data/cars.json";
        private const string CustomersJsonDirectory = RootDirectory + "/data/customers.json";

        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            context.Database.Initialize(true);
            ImportSuppliers(context);
            ImportParts(context);
            ImportCars(context);
            ImportCustomers(context);
            ImportSales(context);
        }

        private static void ImportSales(CarDealerContext context)
        {
            decimal[] discounts = new decimal[] { 0, 0.05m, 0.1m, 0.15m, 0.2m, 0.3m, 0.4m, 0.5m };
            Random rnd = new Random();
            List<Car> cars = context.Cars.ToList();
            List<Customer> customers = context.Customers.ToList();
            for (int i = 0; i < 500; i++)
            {
                decimal rndDiscount = discounts[rnd.Next(0, discounts.Length)];
                Car rndCar = cars[rnd.Next(0, cars.Count)];
                Customer rndCustomer = customers[rnd.Next(0, customers.Count)];

                context.Sales.Add(new Sale()
                {
                    Discount = rndDiscount,
                    Car = rndCar,
                    Customer = rndCustomer
                });
            }

            context.SaveChanges();
        }

        private static void ImportCustomers(CarDealerContext context)
        {
            string customersJson = File.ReadAllText(CustomersJsonDirectory);
            JsonConvert.DeserializeObject<IEnumerable<Customer>>(customersJson)
                .ToList()
                .ForEach(customer => context.Customers.Add(customer));

            context.SaveChanges();
        }

        private static void ImportCars(CarDealerContext context)
        {
            string carsJson = File.ReadAllText(CarsJsonDirectory);
            IEnumerable<Car> cars = JsonConvert.DeserializeObject<IEnumerable<Car>>(carsJson);
            int partsCount = context.Parts.Count();
            foreach (Car car in cars)
            {
                Random rnd = new Random();
                int randomPartsCount = rnd.Next(10, 20);
                for (int i = 0; i < randomPartsCount; i++)
                {
                    int partIndex = rnd.Next(1, partsCount);
                    car.Parts.Add(context.Parts.Find(partIndex));
                }

                car.Price = car.CalculatePrice();
                context.Cars.Add(car);
            }

            context.SaveChanges();
        }

        private static void ImportParts(CarDealerContext context)
        {
            string partsJson = File.ReadAllText(PartsJsonDirectory);
            IEnumerable<Part> parts = JsonConvert.DeserializeObject<IEnumerable<Part>>(partsJson);

            Random rnd = new Random();
            foreach (Part part in parts)
            {
                int randomSupplierId = rnd.Next(1, context.Suppliers.Count());
                Supplier rndSupplier = context.Suppliers.Find(randomSupplierId);
                part.Supplier = rndSupplier;

                context.Parts.Add(part);
            }

            context.SaveChanges();
        }

        private static void ImportSuppliers(CarDealerContext context)
        {
            string suppliersJson = File.ReadAllText(SuppliersJsonDirectory);
            JsonConvert
                .DeserializeObject<IEnumerable<Supplier>>(suppliersJson)
                .ToList()
                .ForEach(s => context.Suppliers.Add(s));

            context.SaveChanges();
        }
    }
}

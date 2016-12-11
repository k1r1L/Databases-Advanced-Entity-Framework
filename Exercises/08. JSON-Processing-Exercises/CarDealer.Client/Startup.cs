namespace CarDealer.Client
{
    using CarDealer.Data;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public const string RootDirectory = "../../";

        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            context.Database.Initialize(true);
            //OrderedCustomers(context);
            //CarsFromMakeToyota(context);
            //LocalSuppliers(context);
            //CarsWithTheirListOfParts(context);
            //TotalSalesByCustomer(context);
            //SalesWithAppliedDiscount(context);
        }

        private static void SalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new
                {
                    car = new
                    {
                        make = s.Car.Make,
                        model = s.Car.Model,
                        travelledDistance = s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    discount = s.Discount,
                    price = s.Car.Price,
                    priceWithDiscount = s.Car.Price - s.Car.Price * s.Discount
                });

            SerializeEntity(sales, RootDirectory + "exported/sales-discounts.json");
        }

        private static void TotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price))
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.spentMoney);

            SerializeEntity(customers, RootDirectory + "exported/customers-total-sales.json");
        }

        private static void CarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        make = c.Make,
                        model = c.Model,
                        travelledDistance = c.TravelledDistance
                    },
                    parts = c.Parts.Select(p => new
                    {
                        name = p.Name,
                        price = p.Price
                    })
                });

            SerializeEntity(cars, RootDirectory + "exported/cars-and-parts.json");
        }

        private static void LocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    partsCount = s.Parts.Count
                });

            SerializeEntity(suppliers, RootDirectory + "exported/local-suppliers.json");
        }

        private static void CarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance);

            SerializeEntity(cars, RootDirectory + "/exported/toyota-cars.json");
        }

        private static void OrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => !c.IsYoungDriver)
                .Select(c => new
                {
                    id = c.Id,
                    name = c.Name,
                    birthDate = c.BirthDate,
                    isYoungDriver = c.IsYoungDriver,
                    sales = c.Sales.Select(s => new
                    {
                        discount = s.Discount,
                        customer = s.Customer.Name
                    })
                });

            SerializeEntity(customers, RootDirectory + "/exported/ordered-customers.json");
        }

        private static void SerializeEntity<T>(T entity, string path)
        {
            string json = JsonConvert.SerializeObject(entity, Formatting.Indented);

            File.WriteAllText(path, json);
        }
    }
}

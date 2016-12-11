namespace CarDealerXmlImport
{
    using CarDealerData;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class CarDealerXmlImport
    {
        private const string RootDirectory = "../../";
        private const string SuppliersXmlDirectory = RootDirectory + "/datasets/suppliers.xml";
        private const string PartsXmlDirectory = RootDirectory + "/datasets/parts.xml";
        private const string CarsXmlDirectory = RootDirectory + "/datasets/cars.xml";
        private const string CustomersXmlDirectory = RootDirectory + "/datasets/customers.xml";

        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            //ImportSuppliers(context);
            //ImportParts(context);
            //ImportCars(context);
            //ImportCustomers(context);
            //ImportSales(context);
        }

        private static void ImportSales(CarDealerContext context)
        {
            decimal[] discounts = new decimal[] { 0m, 0.05m, 0.1m, 0.15m, 0.2m, 0.3m, 0.4m, 0.5m };
            Random rnd = new Random();
            List<Car> cars = context.Cars.ToList();
            List<Customer> customers = context.Customers.ToList();

            for (int i = 0; i < 677; i++)
            {
                decimal rndDiscount = discounts[rnd.Next(0, discounts.Length)];
                Car rndCar = cars[rnd.Next(0, cars.Count)];
                Customer rndCustomer = customers[rnd.Next(0, customers.Count)];
                if (rndCustomer.IsYoungDriver)
                {
                    rndDiscount += 0.05m;
                }

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
            XElement root = XElement.Load(CustomersXmlDirectory);
            foreach (XElement customerElement in root.Elements())
            {
                string name = customerElement.Attribute("name").Value;
                DateTime birthDate = DateTime.Parse(customerElement.Element("birth-date").Value);
                bool isYoungDriver = bool.Parse(customerElement.Element("is-young-driver").Value);

                context.Customers.Add(new Customer()
                {
                    Name = name,
                    BirthDate = birthDate,
                    IsYoungDriver = isYoungDriver
                });
            }

            context.SaveChanges();
        }

        private static void ImportCars(CarDealerContext context)
        {
            XElement root = XElement.Load(CarsXmlDirectory);

            Random rnd = new Random();
            List<Part> parts = context.Parts.ToList();
            foreach (XElement carElement in root.Elements())
            {
                string make = carElement.Element("make").Value;
                string model = carElement.Element("model").Value;
                long travelledDistrance = long.Parse(carElement.Element("travelled-distance").Value);
                Car newCar = new Car()
                {
                    Make = make,
                    Model = model,
                    TravelledDistance = travelledDistrance
                };
                int partsCount = rnd.Next(10, 20);
                for (int i = 0; i < partsCount; i++)
                {
                    newCar.Parts.Add(parts[rnd.Next(0, parts.Count)]);
                }

                context.Cars.Add(newCar);
            }

            context.SaveChanges();
        }

        private static void ImportParts(CarDealerContext context)
        {
            XElement root = XElement.Load(PartsXmlDirectory);

            Random rnd = new Random();
            List<Supplier> suppliers = context.Suppliers.ToList();
            foreach (XElement partElement in root.Elements())
            {
                string name = partElement.Attribute("name").Value;
                decimal price = decimal.Parse(partElement.Attribute("price").Value);
                int quantity = int.Parse(partElement.Attribute("quantity").Value);
                Supplier rndSupplier = suppliers[rnd.Next(0, suppliers.Count)];

                context.Parts.Add(new Part()
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Supplier = rndSupplier
                });
            }

            context.SaveChanges();
        }

        private static void ImportSuppliers(CarDealerContext context)
        {
            XElement root = XElement.Load(SuppliersXmlDirectory);

            foreach (XElement supplierElement in root.Elements())
            {
                string supplierName = supplierElement.Attribute("name").Value;
                bool isImporter = bool.Parse(supplierElement.Attribute("is-importer").Value);

                context.Suppliers.Add(new Supplier()
                {
                    Name = supplierName,
                    IsImporter = isImporter
                });
            }

            context.SaveChanges();
        }
    }
}

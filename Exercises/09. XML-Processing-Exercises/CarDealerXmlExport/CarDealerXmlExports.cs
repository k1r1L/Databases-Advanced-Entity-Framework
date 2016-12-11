namespace CarDealerXmlExport
{
    using System.Linq;
    using System.Xml.Linq;
    using CarDealerData;

    public class CarDealerXmlExports
    {
        private const string RootDirectory = "../../exports/";
        private const string OrderedCustomersXmlDirectory = RootDirectory + "ordered-customers.xml";
        private const string ToyotaCarsXmlDirectory = RootDirectory + "toyota-cars.xml";
        private const string LocalSuppliersXmlDirectory = RootDirectory + "local-suppliers.xml";
        private const string CarsAndPartsXmlDirectory = RootDirectory + "cars-and-parts.xml";
        private const string CustomersTotalSalesXmlDirectory = RootDirectory + "customers-total-sales.xml";
        private const string SalesDiscountsXmlDirectory = RootDirectory + "sales-discounts.xml";


        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            // 01. OrderedCustomersExport(context);
            // 02. CarsFromModelToyotaExport(context);
            // 03. LocalSuppliersExport(context);
            // 04. CarsAndTheirPartsExport(context);
            // 05. TotalSalesByCustomer(context);
            // 06. SalesWithAppliedDiscount(context);
        }

        private static void SalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(sale => new
                {
                    car = new
                    {
                        make = sale.Car.Make,
                        model = sale.Car.Model,
                        travelledDistance = sale.Car.TravelledDistance
                    },
                    customerName = sale.Customer.Name,
                    discount = sale.Discount,
                    price = sale.Car.Price
                });

            XDocument document = new XDocument();
            XElement root = new XElement("sales");
            foreach (var sale in sales)
            {
                XElement saleElement = new XElement("sale");
                XElement carElement = new XElement("car");
                carElement.SetAttributeValue("make", sale.car.make);
                carElement.SetAttributeValue("model", sale.car.model);
                carElement.SetAttributeValue("travelled-distance", sale.car.travelledDistance);
                saleElement.Add(carElement);
                saleElement.Add(new XElement("customer-name", sale.customerName));
                saleElement.Add(new XElement("discount", sale.discount));
                saleElement.Add(new XElement("price", sale.price));
                saleElement.Add(new XElement("price-with-discount", CalculatePriceWithDiscount(sale.price, sale.discount)));
                root.Add(saleElement);
            }

            document.Add(root);
            document.Save(SalesDiscountsXmlDirectory);
        }

        private static decimal CalculatePriceWithDiscount(decimal price, decimal discount)
        {
            decimal priceToSubtract = price * discount;
            decimal priceWithDiscount = price - priceToSubtract;

            return priceWithDiscount;
        }

        private static void TotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Purchases.Count > 0)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Purchases.Count,
                    spentMoney = c.Purchases.Sum(p => p.Car.Price)
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars);

            XDocument document = new XDocument();
            XElement root = new XElement("customers");
            foreach (var customer in customers)
            {
                XElement customerElement = new XElement("customer");
                customerElement.SetAttributeValue("full-name", customer.fullName);
                customerElement.SetAttributeValue("bought-cars", customer.boughtCars);
                customerElement.SetAttributeValue("spent-money", customer.spentMoney);
                root.Add(customerElement);
            }

            document.Add(root);
            document.Save(CustomersTotalSalesXmlDirectory);
        }

        private static void CarsAndTheirPartsExport(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(car => new
                {
                    make = car.Make,
                    model = car.Model,
                    travelledDistance = car.TravelledDistance,
                    parts = car.Parts
                    .Select(part => new
                    {
                        name = part.Name,
                        price = part.Price
                    })
                });

            XDocument document = new XDocument();
            XElement root = new XElement("cars");
            foreach (var car in cars)
            {
                XElement carElement = new XElement("car");
                carElement.SetAttributeValue("make", car.make);
                carElement.SetAttributeValue("model", car.model);
                carElement.SetAttributeValue("travelled-distance", car.travelledDistance);
                XElement partsRootEl = new XElement("parts");
                foreach (var part in car.parts)
                {
                    XElement partEl = new XElement("part");
                    partEl.SetAttributeValue("name", part.name);
                    partEl.SetAttributeValue("price", part.price);
                    partsRootEl.Add(partEl);
                }
                carElement.Add(partsRootEl);
                root.Add(carElement);
            }

            document.Add(root);
            document.Save(CarsAndPartsXmlDirectory);
        }

        private static void LocalSuppliersExport(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    partsCount = s.Parts.Count
                });

            XDocument document = new XDocument();
            XElement root = new XElement("suppliers");
            foreach (var supplier in suppliers)
            {
                XElement supplierElement = new XElement("supplier");
                supplierElement.SetAttributeValue("id", supplier.id);
                supplierElement.SetAttributeValue("name", supplier.name);
                supplierElement.SetAttributeValue("parts-count", supplier.partsCount);
                root.Add(supplierElement);
            }

            document.Add(root);
            document.Save(LocalSuppliersXmlDirectory);
        }

        private static void CarsFromModelToyotaExport(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance);

            XDocument document = new XDocument();
            XElement root = new XElement("cars");
            foreach (Car car in cars)
            {
                XElement carElement = new XElement("car");
                carElement.SetAttributeValue("id", car.Id);
                carElement.SetAttributeValue("make", car.Make);
                carElement.SetAttributeValue("model", car.Model);
                carElement.SetAttributeValue("travelled-distance", car.TravelledDistance);
                root.Add(carElement);
            }

            document.Add(root);
            document.Save(ToyotaCarsXmlDirectory);
        }

        private static void OrderedCustomersExport(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver);

            XDocument doc = new XDocument();
            XElement root = new XElement("customers");
            foreach (Customer customer in customers)
            {
                XElement customerElement = new XElement("customer");
                customerElement.Add(new XElement("id", customer.Id));
                customerElement.Add(new XElement("name", customer.Name));
                customerElement.Add(new XElement("birth-date", customer.BirthDate));
                customerElement.Add(new XElement("is-young-driver", customer.IsYoungDriver));
                root.Add(customerElement);
            }

            doc.Add(root);
            doc.Save(OrderedCustomersXmlDirectory);
        }
    }
}

namespace ProductsShopXmlExport
{
    using ProductsShopData;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    public class ProductsShopXmlExport
    {
        private const string RootDirectory = "../../";
        private const string ProductsInRangeDirectory = RootDirectory + "/exports/products-in-range.xml";
        private const string UsersSoldProductsDirectory = RootDirectory + "/exports/users-sold-products.xml";
        private const string CategoriesByProductsDirectory = RootDirectory + "/exports/categories-by-products.xml";
        private const string UsersAndProductsDirectory = RootDirectory + "/exports/users-and-products.xml";

        public static void Main(string[] args)
        {
            ProductsShopContext context = new ProductsShopContext();
            //ProductsInRange(context);
            //SuccessfullySoldProducts(context);
            //CategoriesByProductCount(context);
            UsersAndProducts(context);
        }

        private static void UsersAndProducts(ProductsShopContext context)
        {
            var users = context.Users
                .Where(user => user.ProductsSold.Count > 0)
                .OrderByDescending(user => user.ProductsSold.Count)
                .ThenBy(user => user.LastName)
                .Select(user => new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    age = user.Age,
                    products = user.ProductsSold
                    .Select(product => new
                    {
                        name = product.Name,
                        price = product.Price
                    })
                });

            XElement root = new XElement("users");
            root.SetAttributeValue("count", users.Count());

            foreach (var user in users)
            {
                XElement userElement = new XElement("user");
                userElement.SetAttributeValue("first-name", user.firstName);
                userElement.SetAttributeValue("last-name", user.lastName);
                userElement.SetAttributeValue("age", user.age);

                XElement soldProductsElement = new XElement("sold-products");
                soldProductsElement.SetAttributeValue("count", user.products.Count());
                foreach (var product in user.products)
                {
                    XElement productElement = new XElement("product");
                    productElement.SetAttributeValue("name", product.name);
                    productElement.SetAttributeValue("price", product.price);
                    soldProductsElement.Add(productElement);
                }

                userElement.Add(soldProductsElement);
                root.Add(userElement);
            }

            root.Save(UsersAndProductsDirectory);
        }

        private static void CategoriesByProductCount(ProductsShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.Products.Count)
                .Select(category => new
                {
                    name = category.Name,
                    productsCount = category.Products.Count,
                    averagePrice = category.Products.Average(p => p.Price),
                    totalRevenue = category.Products.Sum(p => p.Price)
                });

            XElement root = new XElement("categories");
            foreach (var category in categories)
            {
                XElement categoryElement = new XElement("category");
                categoryElement.SetAttributeValue("name", category.name);
                categoryElement.Add(new XElement("products-count", category.productsCount));
                categoryElement.Add(new XElement("average-price", category.averagePrice));
                categoryElement.Add(new XElement("total-revenue", category.totalRevenue));
                root.Add(categoryElement);
            }

            root.Save(CategoriesByProductsDirectory);
        }

        private static void SuccessfullySoldProducts(ProductsShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count(p => p.Seller != null) > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(user => new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    products = user.ProductsSold.
                    Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Seller.FirstName,
                        buyerLastName = p.Seller.LastName
                    })
                });

            XElement root = new XElement("users");
            foreach (var user in users)
            {
                XElement userElement = new XElement("user");
                userElement.SetAttributeValue("first-name", user.firstName);
                userElement.SetAttributeValue("last-name", user.lastName);
                XElement soldProductsElement = new XElement("sold-products");
                foreach (var product in user.products)
                {
                    XElement productElement = new XElement("product");
                    productElement.Add(new XElement("name", product.name));
                    productElement.Add(new XElement("price", product.price));
                    if (product.buyerFirstName != null)
                    {
                        productElement.Add(new XElement("buyer-first-name", product.buyerFirstName));
                    }
                    productElement.Add(new XElement("buyer-last-name", product.buyerLastName));
                    soldProductsElement.Add(productElement);
                }
                userElement.Add(soldProductsElement);
                root.Add(userElement);
            }

            root.Save(UsersSoldProductsDirectory);
        }

        private static void ProductsInRange(ProductsShopContext context)
        {
            var products = context.Products
                .Where(p => p.Buyer == null && p.Price > 500 && p.Price < 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    sellerFullName = (p.Seller.FirstName + " " + p.Seller.LastName).Trim()
                });

            XElement root = new XElement("products");

            foreach (var product in products)
            {
                XElement productElement = new XElement("product");
                productElement.SetAttributeValue("name", product.name);
                productElement.SetAttributeValue("price", product.price);
                productElement.SetAttributeValue("seller", product.sellerFullName);

                root.Add(productElement);
            }

            root.Save(ProductsInRangeDirectory);
        }
    }
}

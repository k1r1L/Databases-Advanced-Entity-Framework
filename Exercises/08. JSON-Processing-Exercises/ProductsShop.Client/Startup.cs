namespace ProductsShop.Client
{
    using System.IO;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;
    
    public class Startup
    {
        private const string ExportDirectory = "../../export";
        public static void Main(string[] args)
        {
            ProductsShopContext context = new ProductsShopContext();
            context.Database.Initialize(true);
            //ProductsInRange(context);
            //SuccessfullySoldProducts(context);
            //CategoriesByProductsCount(context);
            //UsersAndProducts(context);
        }

        private static void UsersAndProducts(ProductsShopContext context)
        {
            var users = new
            {
                usersCount = context.Users.Count(),
                users = context.Users
                .Where(user => user.ProductsSold.Count > 0)
                .OrderByDescending(user => user.ProductsSold.Count)
                .ThenBy(user => user.LastName)
                .Select(user => new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    age = user.Age,
                    soldProducts = new
                    {
                        count = user.ProductsSold.Count,
                        products = user.ProductsSold
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price
                        })
                    }
                })
            };

            string usersJson = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(ExportDirectory + "/users-and-products.json", usersJson);
        }

        private static void CategoriesByProductsCount(ProductsShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.Products.Count)
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.Products.Count,
                    averagePrice = c.Products.Average(p => p.Price),
                    totalRevenue = c.Products.Sum(p => p.Price)
                });

            string categoriesJson = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText(ExportDirectory + "/categories-by-products.json", categoriesJson);
        }

        private static void SuccessfullySoldProducts(ProductsShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0 && u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(user => new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    soldProducts = user.ProductsSold
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName
                    })
                });

            string usersJson = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(ExportDirectory + "/users-sold-products.json", usersJson);
        }

        private static void ProductsInRange(ProductsShopContext context)
        {
            var products = context.Products
                .Where(p => p.Buyer == null && p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = (p.Seller.FirstName + " " + p.Seller.LastName).Trim()
                });

            string productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(ExportDirectory + "/products-in-range.json", productsJson);
        }
    }
}

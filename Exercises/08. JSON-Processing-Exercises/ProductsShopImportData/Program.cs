using Newtonsoft.Json;
using ProductsShop.Data;
using ProductsShop.Models;
using ProductsShop.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsShopImportData
{
    class Program
    {
        public const string RootDirectory = "../../";
        public const string UsersJsonDirectory = RootDirectory + "data/users.json";
        public const string ProductsJsonDirectory = RootDirectory + "data/products.json";
        public const string CategoriesJsonDirectory = RootDirectory + "data/categories.json";

        static void Main(string[] args)
        {
            ProductsShopContext context = new ProductsShopContext();
            SeedUsers(context);
            SeedProducts(context);
            SeedCategories(context);
            SeedCategoriesProducts(context);
            SeedUsersFriends(context);
        }


        private static void SeedUsersFriends(ProductsShopContext context)
        {
            Random rnd = new Random();
            foreach (User user in context.Users)
            {
                for (int i = 0; i < 5; i++)
                {
                    int friendId = rnd.Next(1, context.Users.Count());
                    User rndFriend = context.Users.Find(friendId);
                    user.Friends.Add(rndFriend);
                }
            }

            context.SaveChanges();
        }

        private static void SeedCategoriesProducts(ProductsShopContext context)
        {
            Random rnd = new Random();
            foreach (Category category in context.Categories)
            {
                int randomNumOfProducs = rnd.Next(47, 155);
                for (int i = 0; i < randomNumOfProducs; i++)
                {
                    int prodcutId = rnd.Next(1, context.Products.Count());
                    Product rndProduct = context.Products.Find(prodcutId);
                    category.Products.Add(rndProduct);
                }
            }

            context.SaveChanges();
        }

        private static void SeedCategories(ProductsShopContext context)
        {
            string categoriesJson = File.ReadAllText(CategoriesJsonDirectory);
            IEnumerable<CategoryDTO> categories = JsonConvert
                .DeserializeObject<IEnumerable<CategoryDTO>>(categoriesJson);
            foreach (CategoryDTO category in categories)
            {
                context.Categories.Add(new Category()
                {
                    Name = category.Name
                });
            }

            context.SaveChanges();
        }

        private static void SeedProducts(ProductsShopContext context)
        {
            string productsJson = File.ReadAllText(ProductsJsonDirectory);
            IEnumerable<ProductDTO> products = JsonConvert
                .DeserializeObject<IEnumerable<ProductDTO>>(productsJson);
            Random rnd = new Random();
            int iteration = 1;
            foreach (ProductDTO product in products)
            {
                int buyerId = rnd.Next(1, context.Users.Count());
                User randomBuyer = context.Users.Find(buyerId);
                int sellerId = rnd.Next(1, context.Users.Count());
                User randomSeller = context.Users.Find(sellerId);

                if (iteration % 3 == 0)
                {
                    context.Products.Add(new Product()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Buyer = null,
                        Seller = randomSeller
                    });

                }
                else
                {

                    context.Products.Add(new Product()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Buyer = randomBuyer,
                        Seller = randomSeller
                    });
                }

                iteration++;
            }

            context.SaveChanges();
        }

        private static void SeedUsers(ProductsShopContext context)
        {
            string usersJson = File.ReadAllText(UsersJsonDirectory);
            IEnumerable<UserDTO> users = JsonConvert
                            .DeserializeObject<IEnumerable<UserDTO>>(usersJson);
            foreach (UserDTO user in users)
            {
                context.Users.Add(new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age
                });
            }

            context.SaveChanges();
        }
    }
}

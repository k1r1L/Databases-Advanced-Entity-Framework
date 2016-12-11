namespace ProductsShopXmlImport
{
    using ProductsShopData;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    public class ProductsShopXmlImport
    {
        private const string RootDirectory = "../../";
        private const string UsersXmlDirecotry = RootDirectory + "datasets/users.xml";
        private const string ProductsXmlDirecotry = RootDirectory + "datasets/products.xml";
        private const string CategoriesXmlDirecotry = RootDirectory + "datasets/categories.xml";


        public static void Main(string[] args)
        {
            ProductsShopContext context = new ProductsShopContext();
            //ImportUsers(context);
            //ImportProducts(context);
            //ImportCategories(context);
        }

        private static void ImportCategories(ProductsShopContext context)
        {
            XDocument doc = XDocument.Load(CategoriesXmlDirecotry);
            XElement root = doc.Root;

            Random rnd = new Random();
            List<Product> products = context.Products.ToList();
            foreach (XElement categoryElement in root.Elements())
            {
                int numOfProducts = rnd.Next(40, 60);
                string name = categoryElement.Element("name").Value;
                Category category = new Category() { Name = name };

                for (int i = 0; i < numOfProducts; i++)
                {
                    category.Products.Add(products[rnd.Next(0, products.Count)]);
                }

                context.Categories.Add(category);
            }

            context.SaveChanges();
        }

        private static void ImportProducts(ProductsShopContext context)
        {
            XDocument doc = XDocument.Load(ProductsXmlDirecotry);
            XElement root = doc.Root;
            List<User> users = context.Users.ToList();
            Random rnd = new Random();

            int iterator = 1;
            foreach (XElement productElement in root.Elements())
            {
                string productName = productElement.Element("name").Value;
                decimal productPrice = decimal.Parse(productElement.Element("price").Value);
                User seller = users[rnd.Next(0, users.Count)];
                User buyer = null;

                if (iterator % 3 != 0)
                {
                    buyer = users[rnd.Next(0, users.Count)];
                }

                context.Products.Add(new Product()
                {
                    Name = productName,
                    Price = productPrice,
                    Seller = seller,
                    Buyer = buyer
                });
                iterator++;
            }

            context.SaveChanges();
        }

        private static void ImportUsers(ProductsShopContext context)
        {
            XDocument xDoc = XDocument.Load(UsersXmlDirecotry);

            XElement root = xDoc.Root;

            foreach (XElement userElement in root.Elements())
            {
                string firstName = userElement.Attribute("first-name")?.Value;
                string lastName = userElement.Attribute("last-name").Value;
                int? age = null;

                if (userElement.Attribute("age") != null)
                {
                    age = int.Parse(userElement.Attribute("age").Value);
                }

                context.Users.Add(new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                });
            }

            context.SaveChanges();
        }
    }
}

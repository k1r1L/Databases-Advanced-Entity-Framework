namespace _02.UserDatabase
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            UserContext db = new UserContext();

            try
            {

                Town pleven = new Town()
                {
                    Name = "Pleven",
                    CountryName = "Bulgaria"
                };

                Town sofia = new Town()
                {
                    Name = "Sofia",
                    CountryName = "Bulgaria"
                };

                User kiril = new User()
                {
                    FirstName = "Kiril",
                    LastName = "Kirilov",
                    Username = "Kiril98",
                    Password = "k1r1l!",
                    Email = "kiril@gmail.com",
                    RegisteredOn = DateTime.Now,
                    LastTimeLoggedIn = DateTime.Now,
                    Age = 18,
                    IsDeleted = false,
                    ProfilePicture = File.ReadAllBytes("C:\\Users\\v\\Downloads\\1080.png")
                };

                db.Towns.Add(sofia);
                db.Towns.Add(pleven);
                db.Users.Add(kiril);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    foreach (var error in validationError.ValidationErrors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                throw;
            }
        }
    }
}

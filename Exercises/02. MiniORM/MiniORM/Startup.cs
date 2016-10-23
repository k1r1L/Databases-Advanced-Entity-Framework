namespace MiniORM
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Entities;

    public class Startup
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            ConnectionStringBuilder connectionStringBuilder = new ConnectionStringBuilder("MiniORM");
            IDbContext entityManager = new EntityManager(connectionStringBuilder.ConnectionString, true);

            //User kiril = new User("kircata", "1098", 18, DateTime.Now);
            //User vankata = new User("RoYaL", "ceca", 24, DateTime.Now);
            //User jicata = new User("Jicata", "noobNaCs", 24, DateTime.Now);
            //User kosio = new User("koceto", "azsumkoceto", 18, DateTime.Now);
            //User gesh = new User("Gesh40", "gesha", 19, DateTime.Now);
            //entityManager.Persist(kiril);
            //entityManager.Persist(vankata);
            //entityManager.Persist(jicata);
            //entityManager.Persist(kosio);
            //entityManager.Persist(gesh);

            Console.WriteLine("----------------------");
            Console.WriteLine("User with id 3");
            User userWithId3 = entityManager.FindById<User>(3);
            Console.WriteLine($"{userWithId3.Username} - {userWithId3.Age}");

            Console.WriteLine("----------------------");
            Console.WriteLine("First User in the table");
            User firstUser = entityManager.FindFirst<User>();
            Console.WriteLine($"{firstUser.Username} - {firstUser.Age}");

            Console.WriteLine("----------------------");
            Console.WriteLine("First User in the table by criteria");
            User userByCriteria = entityManager.FindFirst<User>();
            Console.WriteLine($"{userByCriteria.Username} - {userByCriteria.Age}");

            Console.WriteLine("----------------------");
            Console.WriteLine("All Users by criteria");
            List<User> users = entityManager.FindAll<User>("WHERE LEFT([Username], 1) = 'k'").ToList();
            users.ForEach(u => Console.WriteLine($"{u.Username} - {u.Age}"));

            Console.WriteLine("----------------------");
            Console.WriteLine("All Users in database");
            List<User> allUsers = entityManager.FindAll<User>().ToList();
            allUsers.ForEach(u => Console.WriteLine($"{u.Username} - {u.Age}"));

            entityManager.Delete<User>(userWithId3);
        }
    }
}

namespace _02.UserDatabase
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserContext : DbContext
    {
        public UserContext()
            : base("name=UserContext")
        {
        }
        public IDbSet<Town> Towns { get; set; }

        public IDbSet<User> Users { get; set; }
    }
}
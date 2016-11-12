namespace _01.GringottsDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;

    public class Startup
    {
        public static void Main()
        {
            GringottsContext context = new GringottsContext();
            context.Database.Initialize(true);
            context.SaveChanges();
        }
    }
}

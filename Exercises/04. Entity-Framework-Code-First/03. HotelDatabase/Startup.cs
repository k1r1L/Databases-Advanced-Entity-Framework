namespace _03.HotelDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            HotelContext context = new HotelContext();
            context.Database.Initialize(true);
            context.SaveChanges(); 
        }
    }
}

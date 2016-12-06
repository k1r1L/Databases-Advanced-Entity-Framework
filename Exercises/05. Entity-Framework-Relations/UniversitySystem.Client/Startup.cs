namespace UniversitySystem.Client
{
    using Data;
    using BillPaymentSystem.Models.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main(string[] args)
        {
            UniversitySystemContext db = new UniversitySystemContext();
            db.Database.Initialize(true);
            db.SaveChanges();
        }
    }
}

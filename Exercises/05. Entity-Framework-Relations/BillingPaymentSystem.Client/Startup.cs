namespace BillingPaymentSystem.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BillPaymentSystem.Data;
    using BillPaymentSystem.Models.Models.BillingDetails;

    public class Startup
    {
        public static void Main()
        {
            BillPaymentSystemContext db = new BillPaymentSystemContext();
            db.Database.Initialize(true);

            db.SaveChanges();
        }
    }
}

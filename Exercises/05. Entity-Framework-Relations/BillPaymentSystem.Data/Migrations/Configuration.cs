namespace BillPaymentSystem.Data.Migrations
{
    using Models.Models;
    using Models.Models.BillingDetails;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BillPaymentSystem.Data.BillPaymentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BillPaymentSystem.Data.BillPaymentSystemContext";
        }

        protected override void Seed(BillPaymentSystem.Data.BillPaymentSystemContext context)
        {
            BillingDetail creditCard = new CreditCard()
            {
                Number = "0895964686",
                CardType = "Visa",
                ExpirationMonth = 11,
                ExpirationYear = 2017,
            };

            BillingDetail secondCreditCard = new CreditCard()
            {
                Number = "89758623784",
                CardType = "Master Card",
                ExpirationMonth = 08,
                ExpirationYear = 2018,
            };

            BillingDetail bankAccount = new BankAccount()
            {
                Number = "84378345672",
                BankName = "Banka DSK",
                SwiftCode = "9832342234"
            };

            HashSet<BillingDetail> billingDetails = new HashSet<BillingDetail>()
            {
                creditCard,
                bankAccount,
                secondCreditCard
            };

            context.Users.AddOrUpdate(u => u.Email, new User()
            {
                FirstName = "Kiril",
                LastName = "Kirilov",
                Email = "kircata@abv.bg",
                Password = "kircAta!!98!!"
            });

            context.SaveChanges();

            User kiril = context.Users.First();
            foreach (BillingDetail billingDetail in billingDetails)
            {
                if (!kiril.BillingDetails.Any(bd => bd.Number == billingDetail.Number))
                {
                    kiril.BillingDetails.Add(billingDetail);
                }
            }

            context.SaveChanges();
        }
    }
}


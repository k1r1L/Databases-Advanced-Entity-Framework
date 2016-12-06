namespace BillPaymentSystem.Data
{
    using Models.Models;
    using Models.Models.BillingDetails;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class BillPaymentSystemContext : DbContext
    {
        public BillPaymentSystemContext()
            : base("name=BillPaymentSystemContext")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BillPaymentSystemContext>());
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<BillingDetail> BillingDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillingDetail>()
                .HasRequired(bd => bd.Owner)
                .WithMany(o => o.BillingDetails)
                .HasForeignKey(bd => bd.OwnerId);

            // TPH Strategy
            //modelBuilder.Entity<BillingDetail>()
            //    .Map<CreditCard>(bd => bd.Requires("BillingDetailType").HasValue("CreditCard"))
            //    .Map<BankAccount>(bd => bd.Requires("BillingDetailType").HasValue("BankAccount"));

            //TPT Strategy
            modelBuilder.Entity<CreditCard>().ToTable("CreditCards");
            modelBuilder.Entity<BankAccount>().ToTable("BankAccounts");

            //TPC Strategy
            //modelBuilder.Entity<CreditCard>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("CreditCards");
            //});

            //modelBuilder.Entity<BankAccount>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("BankAccounts");
            //});

            //modelBuilder.Entity<BillingDetail>()
            //    .Property(p => p.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            base.OnModelCreating(modelBuilder);
        }
    }
}
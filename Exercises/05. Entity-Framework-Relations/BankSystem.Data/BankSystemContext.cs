namespace BankSystem.Data
{
    using Models;
    using Models.BankingAccounts;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BankSystemContext : DbContext
    {
        public BankSystemContext()
            : base("name=BankSystemContext")
        {
            //Database.SetInitializer(
            //    new DropCreateDatabaseAlways<BankSystemContext>());
        }

        public IDbSet<BankingAccount> BankingAccounts { get; set; }

        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckingAccount>().ToTable("CheckingAccounts");
            modelBuilder.Entity<SavingsAccount>().ToTable("SavingAccounts");

            modelBuilder.Entity<BankingAccount>()
                .HasRequired(b => b.Owner)
                .WithMany(o => o.BankAccounts);

            base.OnModelCreating(modelBuilder);
        }
    }
}
namespace _01.GringottsDatabase
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;
    using Migrations;
    public class GringottsContext : DbContext
    {
        public GringottsContext()
            : base("name=GringottsContext")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<GringottsContext, Configuration>());
        }

        public DbSet<WizardDeposit> WizardDeposits { get; set; }
    }
}
namespace WeddingsPlanner.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WeddingsPlanner.Data.Contracts;
    using WeddingsPlanner.Models;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly WeddingsPlannerContext context;
        private IRepository<Agency> agencies;
        private IRepository<Venue> venues;
        private IRepository<Person> people;
        private IRepository<Wedding> weddings;
        private IRepository<Invitation> invitations;
        private IRepository<Present> presents;
        public UnitOfWork()
        {
            this.context = new WeddingsPlannerContext();
            this.agencies = new Repository<Agency>(this.context.Agencies);
            this.venues = new Repository<Venue>(this.context.Venues);
            this.people = new Repository<Person>(this.context.People);
            this.weddings = new Repository<Wedding>(this.context.Weddings);
            this.invitations = new Repository<Invitation>(this.context.Invitations);
            this.presents = new Repository<Present>(this.context.Presents);
        }
        public IRepository<Agency> Agencies => this.agencies;

        public IRepository<Venue> Venues => this.venues;

        public IRepository<Person> People => this.people;

        public IRepository<Wedding> Weddings => this.weddings;

        public IRepository<Invitation> Invitations => this.invitations;

        public IRepository<Present> Presents => this.presents;

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void InitializeDatabase()
        {
            this.context.Database.Initialize(true);
        }
    }
}

namespace WeddingsPlanner.Data.Contracts
{
    using WeddingsPlanner.Models;
    public interface IUnitOfWork
    {
        IRepository<Agency> Agencies { get; }

        IRepository<Venue> Venues { get; }

        IRepository<Person> People { get; }

        IRepository<Wedding> Weddings { get; }

        IRepository<Invitation> Invitations { get; }

        IRepository<Present> Presents { get; }

        void Commit();

        void InitializeDatabase();

    }
}

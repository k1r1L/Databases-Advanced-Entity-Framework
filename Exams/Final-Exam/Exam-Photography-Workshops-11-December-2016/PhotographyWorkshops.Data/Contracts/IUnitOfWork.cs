namespace PhotographyWorkshops.Data.Contracts
{
    using PhotographyWorkshops.Models;

    public interface IUnitOfWork
    {
        IRepository<Lens> Lenses { get; }

        IRepository<Camera> Cameras { get; }

        IRepository<Accessory> Accessories { get; }

        IRepository<Photographer> Photographers { get; }

        IRepository<Workshop> Workshops { get; }

        void Commit();

        void InitializeDatabase();
    }
}

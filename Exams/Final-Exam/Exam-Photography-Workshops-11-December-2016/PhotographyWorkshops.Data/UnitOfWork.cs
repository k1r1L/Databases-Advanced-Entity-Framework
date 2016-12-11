namespace PhotographyWorkshops.Data
{
    using Contracts;
    using Models;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhotographyWorkshopsContext context;
        private IRepository<Lens> lenses;
        private IRepository<Camera> cameras;
        private IRepository<Accessory> accessories;
        private IRepository<Photographer> photographers;
        private IRepository<Workshop> workshops;

        public UnitOfWork()
        {
            this.context = new PhotographyWorkshopsContext();
            this.lenses = new Repository<Lens>(this.context.Lenses);
            this.cameras = new Repository<Camera>(this.context.Cameras);
            this.accessories = new Repository<Accessory>(this.context.Accessories);
            this.photographers = new Repository<Photographer>(this.context.Photographers);
            this.workshops = new Repository<Workshop>(this.context.Workshops);

        }
        public IRepository<Lens> Lenses => this.lenses;

        public IRepository<Camera> Cameras => this.cameras;

        public IRepository<Accessory> Accessories => this.accessories;

        public IRepository<Photographer> Photographers => this.photographers;

        public IRepository<Workshop> Workshops => this.workshops;


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

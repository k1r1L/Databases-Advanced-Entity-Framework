namespace PhotographyWorkshops.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using Contracts;

    public class Repository<TEntity> :
        IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> set;

        public Repository(DbSet<TEntity> set)
        {
            this.set = set;
        }

        public void Add(TEntity entity)
        {
            this.set.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.set.AddRange(entities);
        }

        public bool Any(Expression<Func<TEntity, bool>> expression)
        {
            return this.set.Any(expression);
        }

        public int Count()
        {
            return this.set.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            return this.set.Count(expression);
        }

        public TEntity Find(int id)
        {
            return this.set.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.set;
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return this.set.Where(expression);
        }

        public TEntity GetFirst()
        {
            return this.set.First();
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> expression)
        {
            return this.set.First(expression);
        }

        public void Remove(TEntity entity)
        {
            this.set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.set.RemoveRange(entities);
        }
    }
}

namespace WeddingsPlanner.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
         where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Find(int id);

        TEntity GetFirst();

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> @expression);

        TEntity GetFirst(Expression<Func<TEntity, bool>> @expression);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> @expression);

        int Count();

        int Count(Expression<Func<TEntity, bool>> expression);

        bool Any(Expression<Func<TEntity, bool>> expression);
    }
}

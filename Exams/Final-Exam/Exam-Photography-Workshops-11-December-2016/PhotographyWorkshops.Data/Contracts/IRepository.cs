namespace PhotographyWorkshops.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Find(int id);

        TEntity GetFirst();

        TEntity GetFirst(Expression<Func<TEntity, bool>> @expression);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> @expression);

        int Count();

        int Count(Expression<Func<TEntity, bool>> expression);

        bool Any(Expression<Func<TEntity, bool>> expression);
    }
}

namespace MiniORM
{
    using System.Collections.Generic;

    public interface IDbContext 
    {
        /// <summary>
        /// Inserts or Update an entity depending if
        /// it is attached to the context
        /// </summary>
        /// <param name="entity">The entity to persist</param>
        /// <returns>Wheter the entity was persist in the database</returns>
        bool Persist(object entity);

        /// <summary>
        /// Returns entity with data from the dabase
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="id">The criteria to search by (in this case the id)</param>
        /// <returns>The searched entity (or null if no such is found)</returns>
        T FindById<T>(int id);

        /// <summary>
        /// Returns collection of all entity objects of type T
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The entity collection</returns>
        IEnumerable<T> FindAll<T>();

        /// <summary>
        /// Returns collection of all entity objects of type T 
        /// matching the criteria given in “where”
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="where">The SQL query criteria</param>
        /// <returns>The entity collection</returns>
        IEnumerable<T> FindAll<T>(string where);

        /// <summary>
        /// Returns the first entity object of type T
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The searched entity</returns>
        T FindFirst<T>();

        /// <summary>
        /// Returns the first entity object of type T 
        /// matching the criteria given in “where”
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="where">The SQL query criteria</param>
        /// <returns></returns>
        T FindFirst<T>(string where);

        /// <summary>
        /// Deletes given object from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>succesfull removal of object</returns>
        void Delete<T>(object entity);

        /// <summary>
        /// Deletes object of type T with given Id from the database 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns>succesfull removal of object</returns>
        void DeleteById<T>(int id);
    }
}

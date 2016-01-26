namespace AMSLLC.Listener.Persistence.Listener
{
    using System.Linq;

    /// <summary>
    /// Db Repository interface
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    public interface IDbRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Enables query execution
        /// </summary>
        /// <returns>IQueryable&lt;TEntity&gt;.</returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Set values of the DB entity
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        void SetValues(
            TEntity target,
            object source);

        /// <summary>
        /// Delete the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(
            TEntity entity);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(
            TEntity entity);
    }
}

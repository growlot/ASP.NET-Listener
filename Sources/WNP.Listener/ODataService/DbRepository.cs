// <copyright file="DbRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using System.Data.Entity;
    using System.Linq;
    using Persistence.Listener;

    /// <summary>
    /// Common DB repository implementation
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    public class DbRepository<TEntity> : IDbRepository<TEntity>
            where TEntity : class
    {
        private readonly DbContext ctx;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public DbRepository(DbContext ctx)
        {
            this.ctx = ctx;
        }

        /// <summary>
        /// Enables query execution
        /// </summary>
        /// <returns>IQueryable&lt;TEntity&gt;.</returns>
        public virtual IQueryable<TEntity> AsQueryable()
        {
            return this.ctx.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// Set values of the DB entity
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        public virtual void SetValues(
            TEntity target,
            object source)
        {
            this.ctx.Entry(target).CurrentValues.SetValues(source);
        }

        /// <summary>
        /// Delete the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(
            TEntity entity)
        {
            this.ctx.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(
            TEntity entity)
        {
            this.ctx.Entry(entity).State = EntityState.Added;
        }
    }
}
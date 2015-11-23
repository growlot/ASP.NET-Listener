// <copyright file="EventPesistenceHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService;
    using Core;
    using Repository.WNP;

    /// <summary>
    /// Base calss for handlers persisting domain events.
    /// </summary>
    public abstract class EventPesistenceHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventPesistenceHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public EventPesistenceHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
        {
            this.UnitOfWork = (IWNPUnitOfWork)requestScope.UnitOfWork;
            this.User = requestScope.User;
            this.Owner = requestScope.OperatingCompany;
            this.TimeProvider = timeProvider;
        }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        protected IWNPUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// The user who initiated the event.
        /// </summary>
        protected string User { get; private set; }

        /// <summary>
        /// Gets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        protected int Owner { get; private set; }

        /// <summary>
        /// The time provider.
        /// </summary>
        protected IDateTimeProvider TimeProvider { get; private set; }

        /// <summary>
        /// Updates the entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="columns">The columns that should be updated.</param>
        /// <returns>The empty task.</returns>
        protected Task UpdateAsync<TEntity>(TEntity entity, ICollection<string> columns)
        {
            if (entity is ITrackModification)
            {
                ((ITrackModification)entity).ModBy = this.User;
                ((ITrackModification)entity).ModDate = this.TimeProvider.Now();
            }

            columns.Add("mod_by");
            columns.Add("mod_date");

            ((WNPUnitOfWork)this.UnitOfWork).DbContext.Update(entity, columns);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Inserts the entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The empty task.</returns>
        protected Task InsertAsync<TEntity>(TEntity entity)
        {
            return this.InsertAsync<TEntity>(entity, this.TimeProvider.Now());
        }

        /// <summary>
        /// Inserts the entity asynchronously and specifies create time.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="createDate">The create date.</param>
        /// <returns>The empty task.</returns>
        protected Task InsertAsync<TEntity>(TEntity entity, DateTime createDate)
        {
            if (entity is ITrackCreation)
            {
                ((ITrackCreation)entity).CreateBy = this.User;
                ((ITrackCreation)entity).CreateDate = createDate;
            }

            if (entity is IUseId)
            {
                TableNameAttribute tableNameAttribute = (TableNameAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(TableNameAttribute));
                if (tableNameAttribute == null)
                {
                    throw new InvalidOperationException("Can not determine table name of specified entity.");
                }

                ((WNPUnitOfWork)this.UnitOfWork).DbContext.Insert(tableNameAttribute.Value, "id", true, entity);
            }
            else
            {
                ((WNPUnitOfWork)this.UnitOfWork).DbContext.Insert(entity);
            }

            return Task.CompletedTask;
        }
    }
}

﻿// <copyright file="EventPesistenceHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="user">The user who initiated the event.</param>
        /// <param name="timeProvider">The time provider.</param>
        public EventPesistenceHandler(IWNPUnitOfWork unitOfWork, string user, IDateTimeProvider timeProvider)
        {
            this.UnitOfWork = unitOfWork;
            this.User = user;
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
        protected Task UpdateAsync<TEntity>(TEntity entity, IEnumerable<string> columns)
        {
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
            ((WNPUnitOfWork)this.UnitOfWork).DbContext.Insert(entity);

            return Task.CompletedTask;
        }
    }
}
// <copyright file="SiteDetailsUpdatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using Domain;
    using Domain.WNP.OwnerAggregate;
    using Metadata;
    using Repository.WNP;
    using WNP;

    /// <summary>
    /// Persists <see cref="SiteDetailsUpdated"/>
    /// </summary>
    public class SiteDetailsUpdatedEventHandler : EventPesistenceHandler, IDomainEventHandler<SiteDetailsUpdated>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteDetailsUpdatedEventHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="user">The user who initiated the event.</param>
        /// <param name="timeProvider">The time provider.</param>
        public SiteDetailsUpdatedEventHandler(IWNPUnitOfWork unitOfWork, string user, IDateTimeProvider timeProvider)
            : base(unitOfWork, user, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(SiteDetailsUpdated domainEvent)
        {
            var site = new SiteEntity()
            {
                Owner = domainEvent.Owner,
                Site = domainEvent.SiteId,
                ModBy = this.User,
                ModDate = this.TimeProvider.Now(),
                SiteDescription = domainEvent.Description,
                PremiseNo = domainEvent.PremiseNumber
            };

            var columnList = new List<string>();
            columnList.Add(DBMetadata.Site.ModBy);
            columnList.Add(DBMetadata.Site.ModDate);
            columnList.Add(DBMetadata.Site.SiteDescription);
            columnList.Add(DBMetadata.Site.PremiseNo);

            return this.UpdateAsync(site, columnList);
        }
    }
}

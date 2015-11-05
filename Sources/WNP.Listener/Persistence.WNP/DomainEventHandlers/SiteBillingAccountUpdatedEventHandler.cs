// <copyright file="SiteBillingAccountUpdatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using Domain;
    using Domain.WNP.SiteAggregate;
    using Metadata;
    using Repository.WNP;
    using WNP;

    /// <summary>
    /// Persists <see cref="SiteBillingAccountUpdated"/>
    /// </summary>
    public class SiteBillingAccountUpdatedEventHandler : EventPesistenceHandler, IDomainEventHandler<SiteBillingAccountUpdated>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteBillingAccountUpdatedEventHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="user">The user who initiated the event.</param>
        /// <param name="timeProvider">The time provider.</param>
        public SiteBillingAccountUpdatedEventHandler(IWNPUnitOfWork unitOfWork, string user, IDateTimeProvider timeProvider)
            : base(unitOfWork, user, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(SiteBillingAccountUpdated domainEvent)
        {
            var site = new SiteEntity()
            {
                Owner = domainEvent.Owner,
                Site = domainEvent.SiteId,
                ModBy = this.User,
                ModDate = this.TimeProvider.Now(),
                AccountName = domainEvent.AccountName,
                AccountNo = domainEvent.AccountNumber
            };

            var columnList = new List<string>();
            columnList.Add(DBMetadata.Site.ModBy);
            columnList.Add(DBMetadata.Site.ModDate);
            columnList.Add(DBMetadata.Site.AccountName);
            columnList.Add(DBMetadata.Site.AccountNo);

            return this.UpdateAsync(site, columnList);
        }
    }
}

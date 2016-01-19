// <copyright file="SiteBillingAccountUpdatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService;
    using Core;
    using Domain;
    using Domain.WNP.SiteAggregate;
    using Metadata;
    using WNP;

    /// <summary>
    /// Persists <see cref="SiteBillingAccountUpdated"/>
    /// </summary>
    public class SiteBillingAccountUpdatedEventHandler : EventPesistenceHandler, IDomainEventHandler<SiteBillingAccountUpdated>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteBillingAccountUpdatedEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public SiteBillingAccountUpdatedEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(SiteBillingAccountUpdated domainEvent)
        {
            var site = new SiteEntity()
            {
                Owner = this.Owner,
                Site = domainEvent.SiteId,
                AccountName = domainEvent.AccountName,
                AccountNo = domainEvent.AccountNumber
            };

            var columnList = new List<string>();
            columnList.Add(DBMetadata.Site.AccountName);
            columnList.Add(DBMetadata.Site.AccountNo);

            // return this.UpdateAsync(site, columnList);
            return ((WNPUnitOfWork)this.UnitOfWork).DbContext.UpdateAsync<SiteEntity>(
                $@"
SET 
{DBMetadata.Site.AccountName} = @0,
{DBMetadata.Site.AccountNo} = @1,
{DBMetadata.Site.ModBy} = @2,
{DBMetadata.Site.ModDate} = @3
WHERE
{DBMetadata.Site.Site} = @4
",
                domainEvent.AccountName,
                domainEvent.AccountNumber,
                this.User,
                this.TimeProvider.Now(),
                domainEvent.SiteId);
        }
    }
}

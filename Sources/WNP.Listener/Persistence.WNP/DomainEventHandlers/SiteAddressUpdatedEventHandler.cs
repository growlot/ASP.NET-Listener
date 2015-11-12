// <copyright file="SiteAddressUpdatedEventHandler.cs" company="Advanced Metering Services LLC">
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
    /// Persists <see cref="SiteAddressUpdated"/>
    /// </summary>
    public class SiteAddressUpdatedEventHandler : EventPesistenceHandler, IDomainEventHandler<SiteAddressUpdated>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteAddressUpdatedEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public SiteAddressUpdatedEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(SiteAddressUpdated domainEvent)
        {
            var site = new SiteEntity()
            {
                Owner = this.Owner,
                Site = domainEvent.SiteId,
                SiteAddress = domainEvent.Address1,
                SiteAddress2 = domainEvent.Address2,
                SiteCity = domainEvent.City,
                SiteCountry = domainEvent.Country,
                SiteState = domainEvent.State,
                SiteZipcode = domainEvent.Zip
            };

            var columnList = new List<string>();
            columnList.Add(DBMetadata.Site.SiteAddress);
            columnList.Add(DBMetadata.Site.SiteAddress2);
            columnList.Add(DBMetadata.Site.SiteCountry);
            columnList.Add(DBMetadata.Site.SiteState);
            columnList.Add(DBMetadata.Site.SiteCity);
            columnList.Add(DBMetadata.Site.SiteZipcode);

            return this.UpdateAsync(site, columnList);
        }
    }
}

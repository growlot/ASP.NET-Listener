// <copyright file="SiteDetailsUpdatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService;
    using Core;
    using Domain;
    using Domain.WNP.OwnerAggregate;
    using Metadata;
    using WNP;

    /// <summary>
    /// Persists <see cref="SiteDetailsUpdated"/>
    /// </summary>
    public class SiteDetailsUpdatedEventHandler : EventPesistenceHandler, IDomainEventHandler<SiteDetailsUpdated>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteDetailsUpdatedEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public SiteDetailsUpdatedEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(SiteDetailsUpdated domainEvent)
        {
            var site = new SiteEntity()
            {
                Owner = this.Owner,
                Site = domainEvent.SiteId,
                SiteDescription = domainEvent.Description,
                PremiseNo = domainEvent.PremiseNumber
            };

            var columnList = new List<string>();
            columnList.Add(DBMetadata.Site.SiteDescription);
            columnList.Add(DBMetadata.Site.PremiseNo);

            return this.UpdateAsync(site, columnList);
        }
    }
}

// <copyright file="SiteCreatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Threading.Tasks;
    using ApplicationService;
    using Core;
    using Domain;
    using Domain.WNP.SiteAggregate;
    using WNP;

    /// <summary>
    /// Persists <see cref="SiteCreatedEvent"/>
    /// </summary>
    public class SiteCreatedEventHandler : EventPesistenceHandler, IDomainEventHandler<SiteCreatedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteCreatedEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public SiteCreatedEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(SiteCreatedEvent domainEvent)
        {
            var site = new SiteEntity()
            {
                Owner = this.Owner,
                AccountName = domainEvent.BillingAccountName,
                AccountNo = domainEvent.BillingAccountNumber,
                PremiseNo = domainEvent.PremiseNumber,
                SiteAddress = domainEvent.Address1,
                SiteAddress2 = domainEvent.Address2,
                SiteCity = domainEvent.City,
                SiteCountry = domainEvent.Country,
                SiteDescription = domainEvent.Description,
                SiteState = domainEvent.State,
                SiteZipcode = domainEvent.Zip,
                InterconnectUtility = domainEvent.InterconnectUtilityName,
                IsInterconnect = domainEvent.IsInterconnect ? "Y" : "N"
            };

            return this.InsertAsync(site);
        }
    }
}

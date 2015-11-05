// <copyright file="SiteCreatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Threading.Tasks;
    using Core;
    using Domain;
    using Domain.WNP.SiteAggregate;
    using Repository.WNP;
    using WNP;

    /// <summary>
    /// Persists <see cref="SiteCreatedEvent"/>
    /// </summary>
    public class SiteCreatedEventHandler : EventPesistenceHandler, IDomainEventHandler<SiteCreatedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteCreatedEventHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="user">The user who initiated the event.</param>
        /// <param name="timeProvider">The time provider.</param>
        public SiteCreatedEventHandler(IWNPUnitOfWork unitOfWork, string user, IDateTimeProvider timeProvider)
            : base(unitOfWork, user, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(SiteCreatedEvent domainEvent)
        {
            var site = new SiteEntity()
            {
                AccountName = domainEvent.BillingAccountName,
                AccountNo = domainEvent.BillingAccountNumber,
                CreateBy = this.User,
                CreateDate = this.TimeProvider.Now(),
                Owner = domainEvent.Owner,
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

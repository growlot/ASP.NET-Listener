// <copyright file="SiteCreatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.DomainEventHandlers
{
    using System.Threading.Tasks;
    using Core;
    using Domain;
    using Domain.WNP.OwnerAggregate;
    using Repository.WNP;
    using WNP;
    using WNP.Metadata;

    /// <summary>
    /// Persists <see cref="SiteCreatedEvent"/>
    /// </summary>
    public class SiteCreatedEventHandler : IDomainEventHandler<SiteCreatedEvent>
    {
        private IWNPUnitOfWork unitOfWork;
        private string user;
        private IDateTimeProvider timeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteCreatedEventHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="user">The user who initiated the event.</param>
        /// <param name="timeProvider">The time provider.</param>
        public SiteCreatedEventHandler(IWNPUnitOfWork unitOfWork, string user, IDateTimeProvider timeProvider)
        {
            this.unitOfWork = unitOfWork;
            this.user = user;
            this.timeProvider = timeProvider;
        }

        /// <inheritdoc/>
        public Task HandleAsync(SiteCreatedEvent domainEvent)
        {
            var site = new SiteEntity()
            {
                AccountName = domainEvent.BillingAccountName,
                AccountNo = domainEvent.BillingAccountNumber,
                CreateBy = this.user,
                CreateDate = this.timeProvider.Now(),
                Owner = domainEvent.Owner,
                PremiseNo = domainEvent.PremiseNumber,
                SiteAddress = domainEvent.Address1,
                SiteAddress2 = domainEvent.Address2,
                SiteCity = domainEvent.City,
                SiteCountry = domainEvent.Country,
                SiteDescription = domainEvent.Description,
                SiteState = domainEvent.State,
                SiteZipcode = domainEvent.Zip
            };

            ((WNPUnitOfWork)this.unitOfWork).DbContext.Insert(site);

            return Task.CompletedTask;
        }
    }
}

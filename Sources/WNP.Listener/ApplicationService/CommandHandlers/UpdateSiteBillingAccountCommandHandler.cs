//-----------------------------------------------------------------------
// <copyright file="UpdateSiteBillingAccountCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Domain.WNP.SiteAggregate;
    using Repository.WNP;

    /// <summary>
    /// Handles <see cref="UpdateSiteAddressCommand"/>.
    /// </summary>
    public class UpdateSiteBillingAccountCommandHandler : CommandHandlerBase, ICommandHandler<UpdateSiteBillingAccountCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSiteBillingAccountCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public UpdateSiteBillingAccountCommandHandler(IWNPUnitOfWork unitOfWork, IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(UpdateSiteBillingAccountCommand command)
        {
            IMemento siteMemento = await this.UnitOfWork.SitesRepository.GetSite(command.SiteId);
            Site site = new Site();
            ((IOriginator)site).SetMemento(siteMemento);

            BillingAccount account = null;
            account = new BillingAccount(command.BillingAccountName, command.BillingAccountNumber);
            site.UpdateBillingAccount(account);

            await this.PublishEvents(site);
        }
    }
}

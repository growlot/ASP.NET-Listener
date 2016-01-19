//-----------------------------------------------------------------------
// <copyright file="CreateSiteCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Domain.WNP.OwnerAggregate;
    using Domain.WNP.SiteAggregate;
    using Repository.WNP;

    /// <summary>
    /// Handles Site creation command.
    /// </summary>
    public class CreateSiteCommandHandler : CommandHandlerBase, ICommandHandler<CreateSiteCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSiteCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public CreateSiteCommandHandler(IWNPUnitOfWork unitOfWork, IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(CreateSiteCommand command)
        {
            IMemento ownerMemento = await this.UnitOfWork.SitesRepository.GetOwnerWithCollidingSitesAsync(command.Owner, command.PremiseNumber, command.Description);
            Owner owner = new Owner();
            ((IOriginator)owner).SetMemento(ownerMemento);

            BillingAccount account = null;
            if (command.BillingAccountName != null
                || command.BillingAccountNumber != null)
            {
                account = new BillingAccount(command.BillingAccountName, command.BillingAccountNumber);
            }

            PhysicalAddress address = null;
            if (!string.IsNullOrWhiteSpace(command.Address1))
            {
                address = new PhysicalAddressBuilder()
                    .CreatePhysicalAddress(command.Address1)
                    .WithAddressLine2(command.Address2)
                    .WithCity(command.City)
                    .WithCountry(command.Country)
                    .WithState(command.State)
                    .WithZipCode(command.Zip);
            }

            var interconnectInfo = new InterconnectSite(command.IsInterconnect, command.InterconnectUtilityName);

            var site = owner.AddSite(account, address, command.Description, command.PremiseNumber, interconnectInfo);

            await this.PublishEvents(site);
        }
    }
}

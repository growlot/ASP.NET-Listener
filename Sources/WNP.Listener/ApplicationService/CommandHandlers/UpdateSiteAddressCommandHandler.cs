//-----------------------------------------------------------------------
// <copyright file="UpdateSiteAddressCommandHandler.cs" company="Advanced Metering Services LLC">
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
    public class UpdateSiteAddressCommandHandler : CommandHandlerBase, ICommandHandler<UpdateSiteAddressCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSiteAddressCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public UpdateSiteAddressCommandHandler(IWNPUnitOfWork unitOfWork, IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(UpdateSiteAddressCommand command)
        {
            IMemento siteMemento = await this.UnitOfWork.SitesRepository.GetSite(command.Owner, command.SiteId);
            Site site = new Site();
            ((IOriginator)site).SetMemento(siteMemento);

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

            site.UpdateAddress(address);

            await this.PublishEvents(site);
        }
    }
}

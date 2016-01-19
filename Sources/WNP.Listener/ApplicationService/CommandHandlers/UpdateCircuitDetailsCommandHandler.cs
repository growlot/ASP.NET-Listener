//-----------------------------------------------------------------------
// <copyright file="UpdateCircuitDetailsCommandHandler.cs" company="Advanced Metering Services LLC">
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
    /// Handles Site creation command.
    /// </summary>
    public class UpdateCircuitDetailsCommandHandler : CommandHandlerBase, ICommandHandler<UpdateCircuitDetailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCircuitDetailsCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public UpdateCircuitDetailsCommandHandler(IWNPUnitOfWork unitOfWork, IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(UpdateCircuitDetailsCommand command)
        {
            IMemento siteMemento = await this.UnitOfWork.SitesRepository.GetSiteAsync(command.SiteId);
            Site site = new Site();
            ((IOriginator)site).SetMemento(siteMemento);

            site.UpdateCircuitDetails(
                circuitId: command.CircuitId,
                circuitDescription: command.CircuitDescription,
                enclosureType: command.EnclosureType,
                hasBracket: command.HasBracket,
                installDate: command.InstallDate,
                meterPoint: command.MeterPoint,
                servicePoint: command.ServicePoint);

            await this.PublishEvents(site);
        }
    }
}

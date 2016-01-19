//-----------------------------------------------------------------------
// <copyright file="CreateCircuitCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Domain.WNP.SiteAggregate;
    using Domain.WNP.SiteAggregate.CircuitChild;
    using Repository.WNP;

    /// <summary>
    /// Handles Circuit creation command.
    /// </summary>
    public class CreateCircuitCommandHandler : CommandHandlerBase, ICommandHandler<CreateCircuitCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCircuitCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public CreateCircuitCommandHandler(IWNPUnitOfWork unitOfWork, IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(CreateCircuitCommand command)
        {
            IMemento siteMemento = await this.UnitOfWork.SitesRepository.GetSiteAsync(command.SiteId);
            Site site = new Site();
            ((IOriginator)site).SetMemento(siteMemento);

            GeoLocation location = null;
            if (command.Latitude.HasValue
                && command.Longitude.HasValue)
            {
                location = new GeoLocation(command.Longitude.Value, command.Latitude.Value);
            }

            var wiring = new ServiceWiring(
                command.WireLocation,
                command.WireSize,
                command.WireType,
                command.NumberOfConductorsPerPhase);

            var service = new ElectricService(
                command.ServiceLocation,
                command.ServiceVoltage,
                command.ServiceAmperage,
                command.ServicePhases,
                command.ServiceWires,
                wiring);

            site.AddCircuit(
                command.Description,
                command.MeterPoint,
                command.ServicePoint,
                command.HasBracket,
                location,
                service,
                command.EnclosureType,
                command.InstallDate);

            await this.PublishEvents(site);
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="UninstallMeterCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Domain.WNP.SiteAggregate;
    using Domain.WNP.SiteAggregate.CircuitChild.Equipment;
    using Repository.WNP;

    /// <summary>
    /// Handles <see cref="UninstallMeterCommand"/>.
    /// </summary>
    public class UninstallMeterCommandHandler : CommandHandlerBase, ICommandHandler<UninstallMeterCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UninstallMeterCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public UninstallMeterCommandHandler(IWNPUnitOfWork unitOfWork, IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(UninstallMeterCommand command)
        {
            IMemento siteMemento = await this.UnitOfWork.SitesRepository.GetSite(command.SiteId);
            var site = new Site();
            ((IOriginator)site).SetMemento(siteMemento);

            var uninstallServiceOrder = new ServiceOrder(command.UninstallServiceOrderStarted, command.UninstallServiceOrderCompleted);

            site.UninstallMeter(
                circuitId: command.CircuitId,
                meterId: command.EquipmentNumber,
                uninstallDate: command.UninstallDate,
                uninstallReason: command.UninstallReason,
                uninstallUser: command.UninstallUser,
                uninstallServiceOrder: uninstallServiceOrder);

            await this.PublishEvents(site);
        }
    }
}

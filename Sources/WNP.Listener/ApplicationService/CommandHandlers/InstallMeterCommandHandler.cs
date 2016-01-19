//-----------------------------------------------------------------------
// <copyright file="InstallMeterCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System;
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Domain.WNP.SiteAggregate;
    using Domain.WNP.SiteAggregate.CircuitChild.Equipment;
    using Repository.WNP;

    /// <summary>
    /// Handles <see cref="InstallMeterCommand"/>.
    /// </summary>
    public class InstallMeterCommandHandler : CommandHandlerBase, ICommandHandler<InstallMeterCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstallMeterCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public InstallMeterCommandHandler(IWNPUnitOfWork unitOfWork, IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(InstallMeterCommand command)
        {
            IMemento siteMemento = await this.UnitOfWork.SitesRepository.GetSiteAsync(command.SiteId);
            var site = new Site();
            ((IOriginator)site).SetMemento(siteMemento);

            IMemento meterMemento = await this.UnitOfWork.SitesRepository.GetMeterAsync(command.EquipmentNumber);
            if (meterMemento == null)
            {
                throw new InvalidOperationException("Can not install meter to circuit, because specified meter was not found.");
            }

            var meter = new CircuitMeter();
            ((IOriginator)meter).SetMemento(meterMemento);

            var installServiceOrder = new ServiceOrder(command.InstallServiceOrderStarted, command.InstallServiceOrderCompleted);

            site.InstallMeter(
                circuitId: command.CircuitId,
                meter: meter,
                installDate: command.InstallDate,
                installUser: command.InstallUser,
                installServiceOrder: installServiceOrder);

            await this.PublishEvents(site);
        }
    }
}

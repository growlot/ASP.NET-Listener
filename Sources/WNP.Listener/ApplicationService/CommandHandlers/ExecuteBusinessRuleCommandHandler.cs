//-----------------------------------------------------------------------
// <copyright file="ExecuteBusinessRuleCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Domain.WNP.WorkstationAggregate;
    using Repository.WNP;

    /// <summary>
    /// Handles <see cref="ExecuteBusinessRuleCommand"/>
    /// </summary>
    public class ExecuteBusinessRuleCommandHandler : CommandHandlerBase, ICommandHandler<ExecuteBusinessRuleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteBusinessRuleCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public ExecuteBusinessRuleCommandHandler(IWNPUnitOfWork unitOfWork, IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(ExecuteBusinessRuleCommand command)
        {
            IMemento workstationMemento = await this.UnitOfWork.WorkstationRepository.GetWorkstation(command.Workstation);
            var workstation = new Workstation();
            ((IOriginator)workstation).SetMemento(workstationMemento);

            IMemento equipmentStateMemento = await this.UnitOfWork.WorkstationRepository.GetEquipmentState(command.EquipmentType, command.EquipmentNumber);
            var equipmentState = new EquipmentState();
            ((IOriginator)equipmentState).SetMemento(equipmentStateMemento);

            Location location = null;
            if (command.Location != null)
            {
                var locationMemento = await this.UnitOfWork.WorkstationRepository.GetLocation(command.Location);
                location = new Location();
                ((IOriginator)location).SetMemento(locationMemento);
            }

            workstation.PerformBusinessAction(
                equipmentState,
                command.ActionName,
                command.BoxNumber,
                command.PalletNumber,
                command.ShelfId,
                command.IssuedTo,
                command.VehicleNumber,
                location);

            await this.PublishEvents(workstation);
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="AddElectricMeterReadingCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Domain.WNP.ElectricMeterAggregate;
    using Repository.WNP;

    /// <summary>
    /// Handles Circuit creation command.
    /// </summary>
    public class AddElectricMeterReadingCommandHandler : CommandHandlerBase, ICommandHandler<AddElectricMeterReadingCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddElectricMeterReadingCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public AddElectricMeterReadingCommandHandler(IWNPUnitOfWork unitOfWork, IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(AddElectricMeterReadingCommand command)
        {
            IMemento electricMeterMemento = await this.UnitOfWork.EquipmentRepository.GetElectricMeter(command.EquipmentNumber);
            ElectricMeter meter = new ElectricMeter();
            ((IOriginator)meter).SetMemento(electricMeterMemento);

            ElectricMeterReading reading = new ElectricMeterReading(
                annunciator: command.Annunciator,
                occasion: command.Occasion,
                label: command.Label,
                value: command.Value,
                source: command.Source,
                date: command.Date,
                user: command.User);

            meter.AddReading(reading);

            await this.PublishEvents(meter);
        }
    }
}

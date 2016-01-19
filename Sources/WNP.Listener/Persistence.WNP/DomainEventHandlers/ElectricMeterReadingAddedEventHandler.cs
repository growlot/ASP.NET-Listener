// <copyright file="ElectricMeterReadingAddedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Threading.Tasks;
    using ApplicationService;
    using Core;
    using Domain;
    using Domain.WNP.ElectricMeterAggregate;
    using Metadata;
    using WNP;

    /// <summary>
    /// Persists <see cref="ElectricMeterReadingAddedEvent"/>
    /// </summary>
    public class ElectricMeterReadingAddedEventHandler : EventPesistenceHandler, IDomainEventHandler<ElectricMeterReadingAddedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElectricMeterReadingAddedEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public ElectricMeterReadingAddedEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(ElectricMeterReadingAddedEvent domainEvent)
        {
            var readIndex = await ((WNPUnitOfWork)this.UnitOfWork).DbContext.FirstOrDefaultAsync<int?>(
                $@"
SELECT MAX({DBMetadata.Reading.ReadIndex})
FROM {DBMetadata.Reading.FullTableName} 
WHERE {DBMetadata.Reading.Owner} = @0 and {DBMetadata.Reading.EqpNo} = @1",
                this.Owner,
                domainEvent.EquipmentNumber);

            var reading = new ReadingEntity()
            {
                Owner = this.Owner,
                EqpNo = domainEvent.EquipmentNumber,
                ReadIndex = readIndex.HasValue ? readIndex.Value + 1 : 0,
                Annunciator = domainEvent.Annunciator,
                EventFlag = domainEvent.Occasion == null ? null : domainEvent.Occasion.ToString(),
                ReadLabel = domainEvent.Label,
                Reading = domainEvent.Value,
                ReadSrc = domainEvent.Source,
                ReadDate = domainEvent.Date,
                ReadBy = domainEvent.User,
            };

            await this.InsertAsync(reading);
        }
    }
}

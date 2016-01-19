// <copyright file="MeterBillingInformationUpdatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService;
    using Core;
    using Domain;
    using Domain.WNP.SiteAggregate.CircuitChild.Equipment;
    using Metadata;
    using WNP;

    /// <summary>
    /// Persists <see cref="MeterBillingInformationUpdatedEvent"/>
    /// </summary>
    public class MeterBillingInformationUpdatedEventHandler : EventPesistenceHandler, IDomainEventHandler<MeterBillingInformationUpdatedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeterBillingInformationUpdatedEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public MeterBillingInformationUpdatedEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(MeterBillingInformationUpdatedEvent domainEvent)
        {
            var meter = new EqpMeterEntity()
            {
                Owner = this.Owner,
                EqpNo = domainEvent.EquipmentNumber,
                EnergyMult = domainEvent.BillingMultiplier,
                PrimaryKh = domainEvent.BillingKh
            };

            var columnList = new List<string>();
            columnList.Add(DBMetadata.EqpMeter.EnergyMult);
            columnList.Add(DBMetadata.EqpMeter.PrimaryKh);

            return ((WNPUnitOfWork)this.UnitOfWork).DbContext.UpdateAsync<EqpMeterEntity>(
                $@"
SET 
{DBMetadata.EqpMeter.EnergyMult} = @0,
{DBMetadata.EqpMeter.PrimaryKh} = @1
WHERE
{DBMetadata.EqpMeter.Owner} = @2
and {DBMetadata.EqpMeter.EqpNo} = @3
",
                domainEvent.BillingMultiplier,
                domainEvent.BillingKh,
                this.Owner,
                domainEvent.EquipmentNumber);
        }
    }
}

// <copyright file="EquipmentStateChangedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService;
    using Core;
    using Domain;
    using Domain.WNP.WorkstationAggregate;
    using Metadata;
    using Utilities;
    using WNP;

    /// <summary>
    /// Persists <see cref="EquipmentStateChangedEvent"/>
    /// </summary>
    public class EquipmentStateChangedEventHandler : EventPesistenceHandler, IDomainEventHandler<EquipmentStateChangedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentStateChangedEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public EquipmentStateChangedEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(EquipmentStateChangedEvent domainEvent)
        {
            IList<Task> result = new List<Task>();
            var currentTime = this.TimeProvider.Now();

            switch (domainEvent.EquipmentType)
            {
                case "EM":
                    // a workaround because PetaPoco doesn't support composite keys.
                    var updateTask = ((WNPUnitOfWork)this.UnitOfWork).DbContext.UpdateAsync<EqpMeterEntity>(
                        $@"
SET 
{DBMetadata.EqpMeter.EqpStatus} = @0,
{DBMetadata.EqpMeter.ShopStatus} = @1,
{DBMetadata.EqpMeter.Location} = @2,
{DBMetadata.EqpMeter.TestProgram} = @3,
{DBMetadata.EqpMeter.ShopCycle} = @4,
{DBMetadata.EqpMeter.BoxNo} = @5,
{DBMetadata.EqpMeter.PalletNo} = @6,
{DBMetadata.EqpMeter.Shelf} = @7,
{DBMetadata.EqpMeter.ReceivedBy} = @8,
{DBMetadata.EqpMeter.VehicleId} = @9,
{DBMetadata.EqpMeter.ModBy} = @10,
{DBMetadata.EqpMeter.ModDate} = @11
WHERE
{DBMetadata.EqpMeter.Owner} = @12
and {DBMetadata.EqpMeter.EqpNo} = @13
",
                        domainEvent.EquipmentStatus,
                        domainEvent.DetailedStatus,
                        domainEvent.Location,
                        domainEvent.Workflow,
                        domainEvent.ShopCycle,
                        domainEvent.BoxNumber,
                        domainEvent.PalletNumber,
                        domainEvent.ShelfId,
                        domainEvent.IssuedTo,
                        domainEvent.Vehicle,
                        this.User,
                        currentTime,
                        this.Owner,
                        domainEvent.EquipmentNumber);
                    result.Add(updateTask);

                    break;
                default:
                    throw new InvalidOperationException(StringUtilities.Invariant($"Can not persist the equipment state change information, because equipment type {domainEvent.EquipmentType} is not supported."));
            }

            var trackingHistory = new TrackingEntity()
            {
                Owner = this.Owner,
                EqpType = domainEvent.EquipmentType,
                EqpNo = domainEvent.EquipmentNumber,
                Workstation = domainEvent.Workstation,
                TestProgram = domainEvent.Workflow,
                Location = domainEvent.Location,
                EqpStatus = domainEvent.EquipmentStatus,
                ShopStatus = domainEvent.DetailedStatus,
                ShopCycle = domainEvent.ShopCycle,
                ReceivedBy = domainEvent.IssuedTo,
                VehicleId = domainEvent.Vehicle
            };

            result.Add(this.InsertAsync(trackingHistory, currentTime));

            await Task.WhenAll(result);
        }
    }
}

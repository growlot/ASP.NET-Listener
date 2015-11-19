// <copyright file="EquipmentInstalledInCircuitEventHandler.cs" company="Advanced Metering Services LLC">
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
    using Domain.WNP.SiteAggregate.CircuitChild;
    using Metadata;
    using Utilities;
    using WNP;

    /// <summary>
    /// Persists <see cref="CircuitCreatedEvent"/>
    /// </summary>
    public class EquipmentInstalledInCircuitEventHandler : EventPesistenceHandler, IDomainEventHandler<EquipmentInstalledInCircuitEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentInstalledInCircuitEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public EquipmentInstalledInCircuitEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(EquipmentInstalledInCircuitEvent domainEvent)
        {
            IList<Task> result = new List<Task>();

            switch (domainEvent.EquipmentType)
            {
                case "EM":
                    var meter = new EqpMeterEntity()
                    {
                        Owner = this.Owner,
                        EqpNo = domainEvent.EquipmentNumber,
                        Site = domainEvent.SiteId,
                        Circuit = domainEvent.CircuitId
                    };
                    var meterColumns = new List<string>();
                    meterColumns.Add(DBMetadata.EqpMeter.Site);
                    meterColumns.Add(DBMetadata.EqpMeter.Circuit);

                    ((WNPUnitOfWork)this.UnitOfWork).DbContext.Update<EqpMeterEntity>(
                        $@"
SET 
{DBMetadata.EqpMeter.Site} = @0,
{DBMetadata.EqpMeter.Circuit} = @1,
{DBMetadata.EqpMeter.ModBy} = @2,
{DBMetadata.EqpMeter.ModDate} = @3
WHERE
{DBMetadata.EqpMeter.Owner} = @4
and {DBMetadata.EqpMeter.EqpNo} = @5
",
                        domainEvent.SiteId,
                        domainEvent.CircuitId,
                        this.User,
                        this.TimeProvider.Now(),
                        this.Owner,
                        domainEvent.EquipmentNumber);

                    // result.Add(this.UpdateAsync(meter, meterColumns));
                    break;
                default:
                    throw new InvalidOperationException(StringUtilities.Invariant($"Can not persist the equipment installation information, because equipment type {domainEvent} is not supported."));
            }

            var siteEntity = ((WNPUnitOfWork)this.UnitOfWork).DbContext.First<SiteEntity>(
                $@"
SELECT {DBMetadata.Site.AccountName}, {DBMetadata.Site.PremiseNo}
FROM {DBMetadata.Site.FullTableName} 
WHERE {DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.Site} = @1",
                this.Owner,
                domainEvent.SiteId);

            var installCount = ((WNPUnitOfWork)this.UnitOfWork).DbContext.First<int?>(
                $@"
SELECT max({DBMetadata.SiteInstallHistory.InstallCount})
FROM {DBMetadata.SiteInstallHistory.FullTableName} 
WHERE {DBMetadata.SiteInstallHistory.Owner} = @0 and {DBMetadata.SiteInstallHistory.EqpType} = @1 and {DBMetadata.SiteInstallHistory.EqpNo} = @2",
                this.Owner,
                domainEvent.EquipmentType,
                domainEvent.EquipmentNumber);

            var installHistory = new SiteInstallHistoryEntity()
            {
                Owner = this.Owner,
                EqpType = domainEvent.EquipmentType,
                EqpNo = domainEvent.EquipmentNumber,
                InstallCount = installCount.HasValue ? installCount.Value + 1 : 0,
                Site = domainEvent.SiteId,
                Circuit = domainEvent.CircuitId,
                AccountNo = siteEntity.AccountNo,
                PremiseNo = siteEntity.PremiseNo,
                InstallStatus = "I",
                InstallDate = domainEvent.InstallDate,
                InstallBy = domainEvent.InstallUser,
                InstallServiceOrderComplete = domainEvent.InstallOrderCompleted,
                InstallServiceOrderStart = domainEvent.InstallOrderIssued
            };

            result.Add(this.InsertAsync(installHistory));

            await Task.WhenAll(result);
        }
    }
}

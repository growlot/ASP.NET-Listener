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
{DBMetadata.EqpMeter.Circuit} = @1
WHERE
{DBMetadata.EqpMeter.Owner} = @2
and {DBMetadata.EqpMeter.EqpNo} = @3
",
                        domainEvent.SiteId,
                        domainEvent.CircuitId,
                        this.Owner,
                        domainEvent.EquipmentNumber);

                    // result.Add(this.UpdateAsync(meter, meterColumns));
                    break;
                case "CT":
                    var currentTransformer = new EqpCtEntity()
                    {
                        Owner = this.Owner,
                        EqpNo = domainEvent.EquipmentNumber,
                        Site = domainEvent.SiteId,
                        Circuit = domainEvent.CircuitId
                    };
                    var currentTransformerColumns = new List<string>();
                    currentTransformerColumns.Add(DBMetadata.EqpCt.Site);
                    currentTransformerColumns.Add(DBMetadata.EqpCt.Circuit);

                    result.Add(this.UpdateAsync(currentTransformer, currentTransformerColumns));
                    break;
                case "PT":
                    var potentialTransformer = new EqpPtEntity()
                    {
                        Owner = this.Owner,
                        EqpNo = domainEvent.EquipmentNumber,
                        Site = domainEvent.SiteId,
                        Circuit = domainEvent.CircuitId
                    };
                    var potentialTransformerColumns = new List<string>();
                    potentialTransformerColumns.Add(DBMetadata.EqpPt.Site);
                    potentialTransformerColumns.Add(DBMetadata.EqpPt.Circuit);

                    result.Add(this.UpdateAsync(potentialTransformer, potentialTransformerColumns));
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
                InstallStatus = "P",
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

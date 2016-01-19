// <copyright file="EquipmentUninstalledFromCircuitEventHandler.cs" company="Advanced Metering Services LLC">
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
    public class EquipmentUninstalledFromCircuitEventHandler : EventPesistenceHandler, IDomainEventHandler<EquipmentUninstalledFromCircuitEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentUninstalledFromCircuitEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public EquipmentUninstalledFromCircuitEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(EquipmentUninstalledFromCircuitEvent domainEvent)
        {
            IList<Task> result = new List<Task>();

            var installCount = await ((WNPUnitOfWork)this.UnitOfWork).DbContext.FirstOrDefaultAsync<int?>(
                $@"
SELECT {DBMetadata.SiteInstallHistory.InstallCount}
FROM {DBMetadata.SiteInstallHistory.FullTableName} 
WHERE {DBMetadata.SiteInstallHistory.Owner} = @0 and {DBMetadata.SiteInstallHistory.EqpType} = @1 and {DBMetadata.SiteInstallHistory.EqpNo} = @2 and {DBMetadata.SiteInstallHistory.InstallStatus} = @3",
                this.Owner,
                domainEvent.EquipmentType,
                domainEvent.EquipmentNumber,
                "I");

            if (installCount.HasValue)
            {
                // a workaround, because PetaPoco doesn't understand composite keys
                var updateTask = ((WNPUnitOfWork)this.UnitOfWork).DbContext.UpdateAsync<SiteInstallHistoryEntity>(
                    $@"
SET
{DBMetadata.SiteInstallHistory.InstallStatus} = @0,
{DBMetadata.SiteInstallHistory.RemoveDate} = @1,
{DBMetadata.SiteInstallHistory.RemoveBy} = @2,
{DBMetadata.SiteInstallHistory.RemoveReason} = @3,
{DBMetadata.SiteInstallHistory.RemoveServiceOrderComplete} = @4,
{DBMetadata.SiteInstallHistory.RemoveServiceOrderStart} = @5
WHERE
{DBMetadata.SiteInstallHistory.Owner} = @6
and {DBMetadata.SiteInstallHistory.EqpNo} = @7
and {DBMetadata.SiteInstallHistory.EqpType} = @8
and {DBMetadata.SiteInstallHistory.InstallCount} = @9
                    ",
                    "R",
                    domainEvent.UninstallDate,
                    domainEvent.UninstallUser,
                    domainEvent.UninstallReason,
                    domainEvent.UninstallOrderCompleted,
                    domainEvent.UninstallOrderIssued,
                    this.Owner,
                    domainEvent.EquipmentNumber,
                    domainEvent.EquipmentType,
                    installCount.Value);

                result.Add(updateTask);
            }
            else
            {
                int siteId;
                int circuitId;

                switch (domainEvent.EquipmentType)
                {
                    case "EM":
                        var meterEntity = await ((WNPUnitOfWork)this.UnitOfWork).DbContext.FirstAsync<EqpMeterEntity>(
                            $@"
SELECT {DBMetadata.EqpMeter.Site}, {DBMetadata.EqpMeter.Circuit}
FROM {DBMetadata.EqpMeter.FullTableName} 
WHERE {DBMetadata.EqpMeter.Owner} = @0 and {DBMetadata.EqpMeter.EqpNo} = @1",
                            this.Owner,
                            domainEvent.EquipmentNumber);
                        siteId = meterEntity.Site.Value;
                        circuitId = meterEntity.Circuit.Value;
                        break;
                    default:
                        throw new InvalidOperationException(StringUtilities.Invariant($"Can not persist the equipment installation information, because equipment type {domainEvent} is not supported."));
                }

                var siteEntity = await ((WNPUnitOfWork)this.UnitOfWork).DbContext.FirstAsync<SiteEntity>(
                    $@"
SELECT {DBMetadata.Site.AccountName}, {DBMetadata.Site.PremiseNo}
FROM {DBMetadata.Site.FullTableName} 
WHERE {DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.Site} = @1",
                    this.Owner,
                    siteId);

                var installHistory = new SiteInstallHistoryEntity()
                {
                    Owner = this.Owner,
                    EqpType = domainEvent.EquipmentType,
                    EqpNo = domainEvent.EquipmentNumber,
                    InstallCount = installCount.HasValue ? installCount.Value : 0,
                    Site = siteId,
                    Circuit = circuitId,
                    AccountNo = siteEntity.AccountNo,
                    PremiseNo = siteEntity.PremiseNo,
                    InstallStatus = "R",
                    RemoveDate = domainEvent.UninstallDate,
                    RemoveBy = domainEvent.UninstallUser,
                    RemoveReason = domainEvent.UninstallReason,
                    RemoveServiceOrderComplete = domainEvent.UninstallOrderCompleted,
                    RemoveServiceOrderStart = domainEvent.UninstallOrderIssued
                };

                result.Add(this.InsertAsync(installHistory));
            }

            switch (domainEvent.EquipmentType)
            {
                case "EM":
                    ////var meter = new EqpMeterEntity()
                    ////{
                    ////    Owner = this.Owner,
                    ////    EqpNo = domainEvent.EquipmentNumber,
                    ////    Site = null,
                    ////    Circuit = null
                    ////};
                    ////var meterColumns = new List<string>();
                    ////meterColumns.Add(DBMetadata.EqpMeter.Site);
                    ////meterColumns.Add(DBMetadata.EqpMeter.Circuit);
                    //// result.Add(this.UpdateAsync(meter, meterColumns));

                    // a workaround, because PetaPoco doesn't understand composite keys
                    var updateTask = ((WNPUnitOfWork)this.UnitOfWork).DbContext.UpdateAsync<EqpMeterEntity>(
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
                        null,
                        null,
                        this.User,
                        this.TimeProvider.Now(),
                        this.Owner,
                        domainEvent.EquipmentNumber);
                    result.Add(updateTask);

                    break;
                default:
                    throw new InvalidOperationException(StringUtilities.Invariant($"Can not persist the equipment installation information, because equipment type {domainEvent} is not supported."));
            }

            await Task.WhenAll(result);
        }
    }
}

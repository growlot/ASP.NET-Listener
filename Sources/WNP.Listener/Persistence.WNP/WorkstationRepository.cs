﻿// <copyright file="WorkstationRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.WNP.WorkstationAggregate;
    using Metadata;
    using Repository.WNP;
    using Utilities;

    /// <summary>
    /// Implmeent workstation repository interface for WNP database.
    /// </summary>
    public class WorkstationRepository : IWorkstationRepository
    {
        private bool disposedValue = false; // To detect redundant calls
        private WNPDBContext dbContext;
        private int operatingCompany;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkstationRepository" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="operatingCompany">The operating company.</param>
        public WorkstationRepository(WNPDBContext dbContext, int operatingCompany)
        {
            this.dbContext = dbContext;
            this.operatingCompany = operatingCompany;
        }

        /// <inheritdoc/>
        public async Task<IMemento> GetEquipmentState(string equipmentType, string equipmentNumber)
        {
            switch (equipmentType)
            {
                case "EM":
                    var electricMeter = this.dbContext.FirstOrDefault<EqpMeterEntity>(
                        $@"
SELECT *
FROM {DBMetadata.EqpMeter.FullTableName}
WHERE {DBMetadata.EqpMeter.Owner} = @0 and {DBMetadata.EqpMeter.EqpNo} = @1",
                        this.operatingCompany,
                        equipmentNumber);
                    if (electricMeter == null)
                    {
                        return null;
                    }
                    else
                    {
                        var equipmentStateMemento = new EquipmentStateMemento(
                            equipmentNumber: equipmentNumber,
                            equipmentType: equipmentType,
                            workflow: electricMeter.TestProgram,
                            location: await this.GetLocation(electricMeter.Location),
                            equipmentStatus: electricMeter.EqpStatus,
                            detailedStatus: electricMeter.ShopStatus,
                            shopCycle: electricMeter.ShopCycle.HasValue ? electricMeter.ShopCycle.Value : 0,
                            boxNumber: electricMeter.BoxNo,
                            palletNumber: electricMeter.PalletNo,
                            shelfId: electricMeter.Shelf,
                            issuedTo: electricMeter.ReceivedBy,
                            vehicleNumber: electricMeter.VehicleId);
                        return equipmentStateMemento;
                    }

                default:
                    throw new NotSupportedException(StringUtilities.Invariant($"Can not get equipment state, because equipment with type {equipmentType} currently not supported."));
            }
        }

        /// <inheritdoc/>
        public Task<IMemento> GetLocation(string name)
        {
            var location = this.dbContext.First<LocationEntity>(
                $@"
SELECT * 
FROM {DBMetadata.Location.FullTableName} 
WHERE {DBMetadata.Location.Owner} = @0 and {DBMetadata.Location.Location} = @1",
                this.operatingCompany,
                name);

            var locationMemento = new LocationMemento(
                locationName: location.Location,
                locationType: location.AreaName);

            return Task.FromResult((IMemento)locationMemento);
        }

        /// <inheritdoc/>
        public async Task<IMemento> GetWorkstation(string name)
        {
            var workstation = this.dbContext.First<WorkstationEntity>(
                $@"
SELECT * 
FROM {DBMetadata.Workstation.FullTableName} 
WHERE {DBMetadata.Workstation.Owner} = @0 and {DBMetadata.Workstation.Workstation} = @1",
                this.operatingCompany,
                name);

            var workstationMemento = new WorkstationMemento(
                name: workstation.Workstation,
                businessActions: await this.GetBusinessActions(name),
                incomingRules: await this.GetIncomingRules(name));

            return workstationMemento;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }

                this.disposedValue = true;
            }
        }

        private async Task<IEnumerable<IMemento>> GetBusinessActions(string name)
        {
            var businessActions = this.dbContext.Fetch<TrackingOutEntity>(
                $@"
SELECT *
FROM {DBMetadata.TrackingOut.FullTableName}
WHERE {DBMetadata.TrackingOut.Owner} = @0 and {DBMetadata.TrackingOut.Workstation} = @1",
                this.operatingCompany,
                name);

            var result = new List<BusinessActionMemento>();
            foreach (var businessAction in businessActions)
            {
                var businessActionMemento = new BusinessActionMemento(
                    actionName: businessAction.ButtonAction,
                    currentWorkflow: businessAction.TestProgram,
                    newWorkflow: businessAction.ChangeToTestProg,
                    newEquipmentStatus: businessAction.OutEqpStatus,
                    newDetailedStatus: businessAction.OutShopStatus,
                    newLocation: businessAction.LocationType == "S" ? businessAction.OutLocation == "*LEAVE_AS_IS*" ? new LocationMemento(businessAction.OutLocation, null) : await this.GetLocation(businessAction.OutLocation) : null,
                    newLocationType: businessAction.LocationType == "A" ? businessAction.OutLocation : null,
                    incrementCycle: businessAction.IncrementCycle == "Y" ? true : false,
                    clearBox: businessAction.ClearBox == "Y" ? true : false,
                    clearPallet: businessAction.ClearPallet == "Y" ? true : false,
                    clearShelf: businessAction.ClearShelf == "Y" ? true : false,
                    clearIssuedTo: businessAction.ClearReceivedBy == "Y" ? true : false,
                    clearVehicleNumber: businessAction.ClearVehicleNo == "Y" ? true : false);
                result.Add(businessActionMemento);
            }

            return result;
        }

        private async Task<IEnumerable<IMemento>> GetIncomingRules(string name)
        {
            var incomingRules = this.dbContext.Fetch<TrackingInEntity>(
                $@"
SELECT *
FROM {DBMetadata.TrackingIn.FullTableName}
WHERE {DBMetadata.TrackingIn.Owner} = @0 and {DBMetadata.TrackingIn.Workstation} = @1",
                this.operatingCompany,
                name);

            var result = new List<IncomingRuleMemento>();
            foreach (var incomingRule in incomingRules)
            {
                var incomingRuleMemento = new IncomingRuleMemento(
                    workflow: incomingRule.TestProgram,
                    isAllowed: incomingRule.IsAllowed == "Y" ? true : false,
                    equipmentStatus: incomingRule.InEqpStatus,
                    detailedStatus: incomingRule.InShopStatus,
                    location: incomingRule.LocationType == "S" ? await this.GetLocation(incomingRule.InLocation) : null,
                    locationType: incomingRule.LocationType == "A" ? incomingRule.InLocation : null,
                    message: incomingRule.DisplayMessage);
                result.Add(incomingRuleMemento);
            }

            return result;
        }
    }
}

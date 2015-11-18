﻿// <copyright file="SiteRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.WNP.OwnerAggregate;
    using Domain.WNP.SiteAggregate;
    using Domain.WNP.SiteAggregate.CircuitChild;
    using Domain.WNP.SiteAggregate.CircuitChild.Equipment;
    using Metadata;
    using Repository.WNP;
    using Utilities;

    /// <summary>
    /// Repository implementation for working with <see cref="Owner"/> agregate root.
    /// </summary>
    public class SiteRepository : ISiteRepository
    {
        private WNPDBContext dbContext;
        private int operatingCompany;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteRepository" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="operatingCompany">The operating company.</param>
        public SiteRepository(WNPDBContext dbContext, int operatingCompany)
        {
            this.dbContext = dbContext;
            this.operatingCompany = operatingCompany;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IMemento> GetSiteByPremiseNumber(string premiseNumber)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IMemento> GetOwnerWithCollidingSites(string sitePremiseNumber, string siteDescription)
        {
            return this.GetOwnerWithCollidingSites(-1, sitePremiseNumber, siteDescription);
        }

        /// <inheritdoc/>
        public Task<IMemento> GetSite(int siteId)
        {
            var siteEntity = this.dbContext.First<SiteEntity>(
                $@"
SELECT * 
FROM {DBMetadata.Site.FullTableName} 
WHERE {DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.Site} = @1",
                this.operatingCompany,
                siteId);

            var siteMemento = new SiteMemento(
                site: siteEntity.Site.Value,
                description: siteEntity.SiteDescription,
                country: siteEntity.SiteCountry,
                state: siteEntity.SiteState,
                city: siteEntity.SiteCity,
                address1: siteEntity.SiteAddress,
                address2: siteEntity.SiteAddress2,
                zip: siteEntity.SiteZipcode,
                premiseNumber: siteEntity.PremiseNo,
                billingAccountName: siteEntity.AccountName,
                billingAccountNumber: siteEntity.AccountNo,
                interconnectUtilityName: siteEntity.InterconnectUtility,
                isInterconnect: siteEntity.IsInterconnect == "Y" ? true : false,
                circuits: this.GetCircuits(siteEntity.Site.Value));

            return Task.FromResult((IMemento)siteMemento);
        }

        /// <inheritdoc/>
        public Task<IMemento> GetOwnerWithCollidingSites(int siteId, string sitePremiseNumber, string siteDescription)
        {
            var siteMementos = new List<OwnerSiteMemento>();

            var select = Sql.Builder.Select("*")
                .From(DBMetadata.Site.FullTableName);

            if (!string.IsNullOrWhiteSpace(sitePremiseNumber))
            {
                var sitesWithSamePremiseNumber = this.GetSites(select.Where($"{DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.PremiseNo} = @1", this.operatingCompany, sitePremiseNumber));
                siteMementos.AddRange(sitesWithSamePremiseNumber);
            }

            if (!string.IsNullOrWhiteSpace(siteDescription))
            {
                var sitesWithSameDescription = this.GetSites(select.Where($"{DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.SiteDescription} = @1", this.operatingCompany, siteDescription));
                siteMementos.AddRange(sitesWithSameDescription);
            }

            var sitesWithSameId = this.GetSites(select.Where($"{DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.Site} = @1", this.operatingCompany, siteId));
            siteMementos.AddRange(sitesWithSameId);

            var ownerMemento = new OwnerMemento(this.operatingCompany, siteMementos);

            return Task.FromResult((IMemento)ownerMemento);
        }

        /// <inheritdoc/>
        public Task<IMemento> GetMeter(string equipmentNumber)
        {
            var meterEntity = this.dbContext.FirstOrDefault<EqpMeterEntity>(
                $@"
SELECT {DBMetadata.EqpMeter.EqpNo}, {DBMetadata.EqpMeter.Site}, {DBMetadata.EqpMeter.Circuit}, 
       {DBMetadata.EqpMeter.Scalar}, {DBMetadata.EqpMeter.EnergyMult}, {DBMetadata.EqpMeter.Kh}, 
       {DBMetadata.EqpMeter.PrimaryKh}, {DBMetadata.EqpMeter.EqpStatus}
FROM {DBMetadata.EqpMeter.FullTableName} 
WHERE {DBMetadata.EqpMeter.Owner} = @0 and {DBMetadata.EqpMeter.EqpNo} = @1",
                this.operatingCompany,
                equipmentNumber);

            if (meterEntity == null)
            {
                return Task.FromResult<IMemento>(null);
            }

            decimal value;
            if (!decimal.TryParse(meterEntity.Kh, out value))
            {
                throw new InvalidOperationException(StringUtilities.Invariant($"Can not load meter {meterEntity.EqpNo}, because it doesn't have Kh value set or value is not a number."));
            }

            var meterMemento = new CircuitMeterMemento(
                equipmentNumber: meterEntity.EqpNo,
                siteId: meterEntity.Site,
                circuitId: meterEntity.Circuit,
                status: meterEntity.EqpStatus,
                internalMultiplier: meterEntity.Scalar,
                billingMultiplier: meterEntity.EnergyMult,
                kh: value,
                billingKh: meterEntity.PrimaryKh);

            return Task.FromResult((IMemento)meterMemento);
        }

        private List<OwnerSiteMemento> GetSites(Sql select)
        {
            var siteMementos = new List<OwnerSiteMemento>();
            var siteEntities = this.dbContext.Fetch<SiteEntity>(select);

            foreach (var siteEntity in siteEntities)
            {
                var siteMemento = new OwnerSiteMemento(
                    site: siteEntity.Site.Value,
                    description: siteEntity.SiteDescription,
                    premiseNumber: siteEntity.PremiseNo);

                siteMementos.Add(siteMemento);
            }

            return siteMementos;
        }

        private IEnumerable<CircuitMemento> GetCircuits(int siteId)
        {
            var circuitMementos = new List<CircuitMemento>();

            var circuitEntities = this.dbContext.Fetch<CircuitEntity>(
                $@"
SELECT * 
FROM {DBMetadata.Circuit.FullTableName} 
WHERE {DBMetadata.Circuit.Owner} = @0 and {DBMetadata.Circuit.Site} = @1",
                this.operatingCompany,
                siteId);

            foreach (var circuitEntity in circuitEntities)
            {
                int intValue;

                int? servicePhases = null;
                if (int.TryParse(circuitEntity.ServicePhase, out intValue))
                {
                    servicePhases = intValue;
                }

                int? serviceWires = null;
                if (int.TryParse(circuitEntity.ServiceWire, out intValue))
                {
                    serviceWires = intValue;
                }

                var circuitMemento = new CircuitMemento(
                    id: circuitEntity.Circuit.Value,
                    description: circuitEntity.CircuitDesc,
                    meterPoint: circuitEntity.MeterPoint,
                    servicePoint: circuitEntity.ServicePoint,
                    hasBracket: circuitEntity.HasBracket == "Y" ? true : false,
                    longitude: (decimal?)circuitEntity.Longitude,
                    latitude: (decimal?)circuitEntity.Latitude,
                    serviceLocation: circuitEntity.ServiceLocation,
                    serviceVoltage: (decimal?)circuitEntity.ServiceVoltage,
                    serviceAmperage: (decimal?)circuitEntity.ServiceAmps,
                    servicePhases: servicePhases,
                    serviceWires: serviceWires,
                    wireLocation: circuitEntity.WireLocation,
                    wireSize: circuitEntity.WireSize,
                    wireType: circuitEntity.WireType,
                    numberOfConductorsPerPhase: circuitEntity.ConductorsPerPhase,
                    enclosureType: circuitEntity.EnclosureType,
                    installDate: circuitEntity.InstallDate,
                    meters: this.GetMeters(siteId, circuitEntity.Circuit.Value),
                    currentTransformers: this.GetCurrentTransformers(siteId, circuitEntity.Circuit.Value),
                    potentialTransformers: this.GetPotentialTransformers(siteId, circuitEntity.Circuit.Value));

                circuitMementos.Add(circuitMemento);
            }

            return circuitMementos;
        }

        private IEnumerable<CircuitMeterMemento> GetMeters(int siteId, int circuitId)
        {
            var meterMementos = new List<CircuitMeterMemento>();

            var meterEntities = this.dbContext.Fetch<EqpMeterEntity>(
                $@"
SELECT {DBMetadata.EqpMeter.EqpNo}, {DBMetadata.EqpMeter.Site}, {DBMetadata.EqpMeter.Circuit}, 
       {DBMetadata.EqpMeter.Scalar}, {DBMetadata.EqpMeter.EnergyMult}, {DBMetadata.EqpMeter.Kh}, 
       {DBMetadata.EqpMeter.PrimaryKh}, {DBMetadata.EqpMeter.EqpStatus}
FROM {DBMetadata.EqpMeter.FullTableName} 
WHERE {DBMetadata.EqpMeter.Owner} = @0 and {DBMetadata.EqpMeter.Site} = @1 and {DBMetadata.EqpMeter.Circuit} = @2",
                this.operatingCompany,
                siteId,
                circuitId);
            foreach (var meterEntity in meterEntities)
            {
                decimal value;
                if (!decimal.TryParse(meterEntity.Kh, out value))
                {
                    throw new InvalidOperationException(StringUtilities.Invariant($"Can not load meter {meterEntity.EqpNo} in site {siteId} circuit {circuitId}, because it doesn't have Kh value set or value is not a number."));
                }

                var meterMemento = new CircuitMeterMemento(
                    equipmentNumber: meterEntity.EqpNo,
                    siteId: meterEntity.Site,
                    circuitId: meterEntity.Circuit,
                    status: meterEntity.EqpStatus,
                    internalMultiplier: meterEntity.Scalar,
                    billingMultiplier: meterEntity.EnergyMult,
                    kh: value,
                    billingKh: meterEntity.PrimaryKh);

                meterMementos.Add(meterMemento);
            }

            return meterMementos;
        }

        private IEnumerable<CircuitCurrentTransformerMemento> GetCurrentTransformers(int siteId, int circuitId)
        {
            var currentTransformerMementos = new List<CircuitCurrentTransformerMemento>();

            var currentTransformerEntities = this.dbContext.Fetch<EqpCtEntity>(
                $@"
SELECT {DBMetadata.EqpCt.EqpNo}, {DBMetadata.EqpCt.SelectedRatio}, {DBMetadata.EqpCt.AuxMult}
FROM {DBMetadata.EqpCt.FullTableName} 
WHERE {DBMetadata.EqpCt.Owner} = @0 and {DBMetadata.EqpCt.Site} = @1 and {DBMetadata.EqpCt.Circuit} = @2",
                this.operatingCompany,
                siteId,
                circuitId);

            decimal decimalValue;
            foreach (var currentTransformerEntity in currentTransformerEntities)
            {
                decimal? selectedRatio = null;
                if (decimal.TryParse(currentTransformerEntity.SelectedRatio, out decimalValue))
                {
                    selectedRatio = decimalValue;
                }

                var currentTransformerMemento = new CircuitCurrentTransformerMemento(
                    id: currentTransformerEntity.EqpNo,
                    loopCount: currentTransformerEntity.AuxMult,
                    selectedRatio: selectedRatio);

                currentTransformerMementos.Add(currentTransformerMemento);
            }

            return currentTransformerMementos;
        }

        private IEnumerable<CircuitPotentialTransformerMemento> GetPotentialTransformers(int siteId, int circuitId)
        {
            var potentialTransformerMementos = new List<CircuitPotentialTransformerMemento>();

            var potentialTransformerEntities = this.dbContext.Fetch<EqpCtEntity>(
                $@"
SELECT {DBMetadata.EqpPt.EqpNo}, {DBMetadata.EqpPt.SelectedRatio}
FROM {DBMetadata.EqpPt.FullTableName} 
WHERE {DBMetadata.EqpPt.Owner} = @0 and {DBMetadata.EqpPt.Site} = @1 and {DBMetadata.EqpPt.Circuit} = @2",
                this.operatingCompany,
                siteId,
                circuitId);

            decimal decimalValue;
            foreach (var potentialTransformerEntity in potentialTransformerEntities)
            {
                decimal? selectedRatio = null;
                if (decimal.TryParse(potentialTransformerEntity.SelectedRatio, out decimalValue))
                {
                    selectedRatio = decimalValue;
                }

                var potentialTransformerMemento = new CircuitPotentialTransformerMemento(
                    id: potentialTransformerEntity.EqpNo,
                    selectedRatio: selectedRatio);

                potentialTransformerMementos.Add(potentialTransformerMemento);
            }

            return potentialTransformerMementos;
        }
    }
}

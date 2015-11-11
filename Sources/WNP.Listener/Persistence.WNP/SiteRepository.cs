// <copyright file="SiteRepository.cs" company="Advanced Metering Services LLC">
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
    using Metadata;
    using Repository.WNP;

    /// <summary>
    /// Repository implementation for working with <see cref="Owner"/> agregate root.
    /// </summary>
    public class SiteRepository : ISiteRepository
    {
        private WNPDBContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public SiteRepository(WNPDBContext dbContext)
        {
            this.dbContext = dbContext;
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
        public Task<IMemento> GetOwnerWithCollidingSites(int owner, string sitePremiseNumber, string siteDescription)
        {
            return this.GetOwnerWithCollidingSites(owner, -1, sitePremiseNumber, siteDescription);
        }

        /// <inheritdoc/>
        public Task<IMemento> GetSite(int owner, int siteId)
        {
            var siteEntity = this.dbContext.First<SiteEntity>($"SELECT * FROM {DBMetadata.Site.FullTableName} WHERE {DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.Site} = @1", owner, siteId);

            var circuitEntities = this.dbContext.Fetch<CircuitEntity>($"SELECT * FROM {DBMetadata.Circuit.FullTableName} WHERE {DBMetadata.Circuit.Owner} = @0 and {DBMetadata.Circuit.Site} = @1", owner, siteId);

            var circuitMementos = new List<CircuitMemento>();

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
                    installDate: circuitEntity.InstallDate);

                circuitMementos.Add(circuitMemento);
            }

            var siteMemento = new SiteMemento(
                owner: owner,
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
                circuits: circuitMementos);

            return Task.FromResult((IMemento)siteMemento);
        }

        /// <inheritdoc/>
        public Task<IMemento> GetOwnerWithCollidingSites(int owner, int siteId, string sitePremiseNumber, string siteDescription)
        {
            var siteMementos = new List<OwnerSiteMemento>();

            var select = Sql.Builder.Select("*")
                .From(DBMetadata.Site.FullTableName);

            if (!string.IsNullOrWhiteSpace(sitePremiseNumber))
            {
                var sitesWithSamePremiseNumber = this.GetSites(select.Where($"{DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.PremiseNo} = @1", owner, sitePremiseNumber));
                siteMementos.AddRange(sitesWithSamePremiseNumber);
            }

            if (!string.IsNullOrWhiteSpace(siteDescription))
            {
                var sitesWithSameDescription = this.GetSites(select.Where($"{DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.SiteDescription} = @1", owner, siteDescription));
                siteMementos.AddRange(sitesWithSameDescription);
            }

            var sitesWithSameId = this.GetSites(select.Where($"{DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.Site} = @1", owner, siteId));
            siteMementos.AddRange(sitesWithSameId);

            var ownerMemento = new OwnerMemento(owner, siteMementos);

            return Task.FromResult((IMemento)ownerMemento);
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
    }
}

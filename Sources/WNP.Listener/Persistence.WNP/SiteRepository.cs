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
            var select = Sql.Builder.Select("*")
                .From(DBMetadata.Site.FullTableName)
                .Where($"{DBMetadata.Site.Owner} = @0", owner);

            if (!string.IsNullOrWhiteSpace(sitePremiseNumber) && !string.IsNullOrWhiteSpace(siteDescription))
            {
                select.Where($"{DBMetadata.Site.Owner} = @0 and ({DBMetadata.Site.SiteDescription} = @1 or {DBMetadata.Site.PremiseNo} = @2)", owner, siteDescription, sitePremiseNumber);
            }
            else if (!string.IsNullOrWhiteSpace(sitePremiseNumber))
            {
                select.Where($"{DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.PremiseNo} = @1", owner, sitePremiseNumber);
            }
            else if (!string.IsNullOrWhiteSpace(siteDescription))
            {
                select.Where($"{DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.SiteDescription} = @1", owner, siteDescription);
            }
            else
            {
                select.Where($"{DBMetadata.Site.Owner} = @0", owner);
            }

            var siteEntities = this.dbContext.Fetch<SiteEntity>(select);

            var siteMementos = new List<OwnerSiteMemento>();
            foreach (var siteEntity in siteEntities)
            {
                var siteMemento = new OwnerSiteMemento(
                    site: siteEntity.Site.Value,
                    description: siteEntity.SiteDescription,
                    premiseNumber: siteEntity.PremiseNo);

                siteMementos.Add(siteMemento);
            }

            var ownerMemento = new OwnerMemento(owner, siteMementos);

            return Task.FromResult((IMemento)ownerMemento);
        }

        /// <inheritdoc/>
        public Task<IMemento> GetSite(int owner, int siteId)
        {
            var siteEntity = this.dbContext.First<SiteEntity>($"SELECT * FROM {DBMetadata.Site.FullTableName} WHERE {DBMetadata.Site.Owner} = @0 and {DBMetadata.Site.Site} = @1", owner, siteId);

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
                billingAccountNumber: siteEntity.AccountNo);

            return Task.FromResult((IMemento)siteMemento);
        }
    }
}

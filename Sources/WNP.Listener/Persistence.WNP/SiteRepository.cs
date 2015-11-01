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
        public Task<IEnumerable<IMemento>> GetCollidingSites(int owner, string sitePremiseNumber, string siteDescription)
        {
            var siteEntities = this.dbContext.Fetch<SiteEntity>($"SELECT * FROM {DBMetadata.Site.FullTableName} WHERE {DBMetadata.Site.Owner} = @0 and ({DBMetadata.Site.SiteDescription} = @1 or {DBMetadata.Site.PremiseNo} = @2)", owner, siteDescription, sitePremiseNumber);

            IList<SiteMemento> siteMementos = new List<SiteMemento>();
            foreach (var siteEntity in siteEntities)
            {
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
                    billingAccountNumber: siteEntity.AccountNo);

                siteMementos.Add(siteMemento);
            }

            return Task.FromResult((IEnumerable<IMemento>)siteMementos);
        }
    }
}

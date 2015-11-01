// <copyright file="OwnerRepository.cs" company="Advanced Metering Services LLC">
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
    public class OwnerRepository : IOwnerRepository
    {
        private WNPDBContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public OwnerRepository(WNPDBContext dbContext)
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
        public Task<IMemento> GetOwner(int owner)
        {
            return Task.FromResult((IMemento)this.dbContext.FirstOrDefault<OwnerMemento>("SELECT owner FROM wndba.towner WHERE owner = @0", owner));
        }

        /// <inheritdoc/>
        public async Task<IMemento> GetOwnerWithSites(ISiteRepository siteRepository, int owner, string sitePremiseNumber, string siteDescription)
        {
            var ownerEntity = this.dbContext.FirstOrDefault<OwnerEntity>($"SELECT * FROM {DBMetadata.Owner.FullTableName} WHERE {DBMetadata.Owner.Owner} = @0", owner);
            var siteMementos = await siteRepository.GetCollidingSites(owner, sitePremiseNumber, siteDescription);
            var ownerMemento = new OwnerMemento(ownerEntity.Owner.Value, siteMementos);

            return ownerMemento;
        }
    }
}

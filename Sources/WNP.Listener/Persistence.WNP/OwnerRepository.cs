// <copyright file="OwnerRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP
{
    using System;
    using System.Threading.Tasks;
    using Domain;
    using Domain.WNP.OwnerAggregate;
    using Metadata;
    using Repository.WNP;

    /// <summary>
    /// Repository implementation for working with <see cref="Owner"/> agregate root.
    /// </summary>
    public class OwnerRepository : IOwnerRepository
    {
        private WNPDBContext dbContext;
        private int operatingCompany;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerRepository" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="operatingCompany">The operating company.</param>
        public OwnerRepository(WNPDBContext dbContext, int operatingCompany)
        {
            this.dbContext = dbContext;
            this.operatingCompany = operatingCompany;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IMemento> GetOwner()
        {
            return Task.FromResult((IMemento)this.dbContext.FirstOrDefaultAsync<OwnerMemento>(
                $@"
SELECT {DBMetadata.Owner.Owner}
FROM {DBMetadata.Owner.FullTableName}
WHERE {DBMetadata.Owner.Owner} = @0",
                this.operatingCompany));
        }
    }
}

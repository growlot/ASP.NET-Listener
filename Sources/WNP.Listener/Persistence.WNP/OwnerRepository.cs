// <copyright file="OwnerRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP
{
    using System;
    using System.Threading.Tasks;
    using Domain;
    using Domain.WNP.OwnerAggregate;
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

        /// <summary>
        /// Gets the owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>The owner memento.</returns>
        public Task<IMemento> GetOwner(int owner)
        {
            return Task.FromResult((IMemento)this.dbContext.FirstOrDefault<OwnerMemento>("SELECT owner FROM wndba.towner WHERE owner = @0", owner));
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="IOwnerSitesRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Repository.WNP
{
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Repository to access Owner agregate root with specific retrieval optimized for Site management.
    /// </summary>
    public interface IOwnerSitesRepository : IOwnerRepository
    {
        /// <summary>
        /// Gets the Owner agregate root memento.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>The Owner agregate root memento.</returns>
        new Task<IMemento> GetOwner(int owner);
    }
}

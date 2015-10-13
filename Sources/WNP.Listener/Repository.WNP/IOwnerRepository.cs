//-----------------------------------------------------------------------
// <copyright file="IOwnerRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Repository.WNP
{
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Repository to access Owner agregate root
    /// </summary>
    public interface IOwnerRepository : IRepository
    {
        /// <summary>
        /// Gets the Owner agregate root memento.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>The Owner agregate root memento.</returns>
        Task<IMemento> GetOwner(int owner);
    }
}

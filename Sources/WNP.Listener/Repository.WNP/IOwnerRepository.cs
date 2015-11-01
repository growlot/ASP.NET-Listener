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

        /// <summary>
        /// Gets the owner agregate root memento with site chaild entities. Only child sites with matching premise number or site description are retrieved from db.
        /// </summary>
        /// <param name="siteRepository">The site repository.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="sitePremiseNumber">The site premise number.</param>
        /// <param name="siteDescription">The site description.</param>
        /// <returns>
        /// The Owner agregate root memento.
        /// </returns>
        Task<IMemento> GetOwnerWithSites(ISiteRepository siteRepository, int owner, string sitePremiseNumber, string siteDescription);
    }
}

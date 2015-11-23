﻿//-----------------------------------------------------------------------
// <copyright file="ISiteRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Repository.WNP
{
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Repository to access Site agregate root
    /// </summary>
    public interface ISiteRepository : IRepository
    {
        /// <summary>
        /// Gets the memento for site agregate.
        /// </summary>
        /// <param name="siteId">The site identifier.</param>
        /// <returns>The site memento</returns>
        Task<IMemento> GetSite(int siteId);

        /// <summary>
        /// Gets the owner with sites that belong to this owner and have same premise number or description.
        /// </summary>
        /// <param name="sitePremiseNumber">The site premise number.</param>
        /// <param name="siteDescription">The site description.</param>
        /// <returns>The owner mementos.</returns>
        Task<IMemento> GetOwnerWithCollidingSites(string sitePremiseNumber, string siteDescription);

        /// <summary>
        /// Gets the owner with sites that belong to this owner and have same site identity, premise number or description.
        /// </summary>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="sitePremiseNumber">The site premise number.</param>
        /// <param name="siteDescription">The site description.</param>
        /// <returns>
        /// The owner mementos.
        /// </returns>
        Task<IMemento> GetOwnerWithCollidingSites(int siteId, string sitePremiseNumber, string siteDescription);

        /// <summary>
        /// Gets the meter fields used in inventory management context.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <returns>The meter memento.</returns>
        Task<IMemento> GetMeter(string equipmentNumber);
    }
}

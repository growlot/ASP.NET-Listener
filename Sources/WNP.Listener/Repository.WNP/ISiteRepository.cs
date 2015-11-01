//-----------------------------------------------------------------------
// <copyright file="ISiteRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Repository.WNP
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Repository to access Site agregate root
    /// </summary>
    public interface ISiteRepository : IRepository
    {
        /// <summary>
        /// Gets the site by premise number.
        /// </summary>
        /// <param name="premiseNumber">The premise number.</param>
        /// <returns>The site.</returns>
        Task<IMemento> GetSiteByPremiseNumber(string premiseNumber);

        /// <summary>
        /// Gets the sites that belong to specified owner and have same premise number or description.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="sitePremiseNumber">The site premise number.</param>
        /// <param name="siteDescription">The site description.</param>
        /// <returns>The list of site mementos.</returns>
        Task<IEnumerable<IMemento>> GetCollidingSites(int owner, string sitePremiseNumber, string siteDescription);
    }
}

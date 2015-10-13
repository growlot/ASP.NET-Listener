//-----------------------------------------------------------------------
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
        /// Gets the site by premise number.
        /// </summary>
        /// <param name="premiseNumber">The premise number.</param>
        /// <returns>The site.</returns>
        Task<IMemento> GetSiteByPremiseNumber(string premiseNumber);
    }
}

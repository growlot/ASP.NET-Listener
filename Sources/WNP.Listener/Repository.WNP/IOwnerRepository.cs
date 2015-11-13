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
        /// <returns>The Owner agregate root memento.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Information is loaded from database, so property is not appropriate.")]
        Task<IMemento> GetOwner();
    }
}

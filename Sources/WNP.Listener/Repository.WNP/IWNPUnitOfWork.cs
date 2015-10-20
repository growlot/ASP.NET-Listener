//-----------------------------------------------------------------------
// <copyright file="IWNPUnitOfWork.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Repository.WNP
{
    using Domain;

    /// <summary>
    /// Unit of work interface for WNP database
    /// </summary>
    public interface IWNPUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets the owner repository.
        /// </summary>
        /// <value>
        /// The owner repository.
        /// </value>
        IOwnerRepository OwnerRepository { get; }

        /// <summary>
        /// Gets the sites respository.
        /// </summary>
        /// <value>
        /// The sites respository.
        /// </value>
        ISiteRepository SitesRepository { get; }
    }
}

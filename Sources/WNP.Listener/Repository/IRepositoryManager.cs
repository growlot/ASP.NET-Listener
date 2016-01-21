// <copyright file="IRepositoryManager.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository
{
    using System;

    /// <summary>
    /// Manages repository creation and disposal
    /// </summary>
    public interface IRepositoryManager : IDisposable
    {
        /// <summary>
        /// Creates new repository instance.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository.</typeparam>
        /// <returns>The repository.</returns>
        TRepository Create<TRepository>()
            where TRepository : IRepository;
    }
}

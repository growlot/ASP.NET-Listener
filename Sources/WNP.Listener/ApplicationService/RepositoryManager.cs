// //-----------------------------------------------------------------------
// <copyright file="RepositoryManager.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.InteropServices;
    using Core;
    using Repository;

    /// <summary>
    /// Implmenets <see cref="IRepositoryManager"/>
    /// </summary>
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ConcurrentBag<IRepository> knownRepositories = new ConcurrentBag<IRepository>();
        private readonly IDependencyInjectionAdapter diContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryManager" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public RepositoryManager(IDependencyInjectionAdapter container)
        {
            this.diContainer = container;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RepositoryManager" /> class.
        /// </summary>
        ~RepositoryManager()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public TRepository Create<TRepository>()
            where TRepository : IRepository
        {
            var repository = this.diContainer.ResolveType<TRepository>();
            this.knownRepositories.Add(repository);
            return repository;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var knownRepository in this.knownRepositories)
                {
                    knownRepository?.Dispose();
                }
            }
        }
    }
}
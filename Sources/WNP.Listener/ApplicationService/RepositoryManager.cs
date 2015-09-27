// //-----------------------------------------------------------------------
// // <copyright file="RepositoryManager.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections.Concurrent;
    using Core;
    using Repository;

    public class RepositoryManager : IRepositoryManager
    {
        private readonly ConcurrentBag<IRepository> _knownRepositories = new ConcurrentBag<IRepository>();

        public TRepository Create<TRepository>() where TRepository : IRepository
        {
            var repository = ApplicationIntegration.DependencyResolver.ResolveType<TRepository>();
            this._knownRepositories.Add(repository);
            return repository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Finalizes an instance of the <see cref="RepositoryManager" /> class.
        /// </summary>
        ~RepositoryManager()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var knownRepository in this._knownRepositories)
                {
                    knownRepository?.Dispose();
                }
            }
        }
    }
}
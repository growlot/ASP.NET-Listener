// //-----------------------------------------------------------------------
// <copyright file="RepositoryManager.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Core;
    using Repository;

    /// <summary>
    /// Implmenets <see cref="IRepositoryManager"/>
    /// </summary>
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Dictionary<Type, IRepository> knownRepositories = new Dictionary<Type, IRepository>();
        private readonly IDependencyInjectionAdapter diContainer;
        private ReaderWriterLockSlim repoLock = new ReaderWriterLockSlim();

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
            this.repoLock.EnterUpgradeableReadLock();
            try
            {
                IRepository result = null;
                if (this.knownRepositories.TryGetValue(typeof(TRepository), out result))
                {
                    return (TRepository)result;
                }
                else
                {
                    this.repoLock.EnterWriteLock();
                    try
                    {
                        result = this.diContainer.ResolveType<TRepository>();

                        this.knownRepositories.Add(typeof(TRepository), result);
                    }
                    finally
                    {
                        this.repoLock.ExitWriteLock();
                    }

                    return (TRepository)result;
                }
            }
            finally
            {
                this.repoLock.ExitUpgradeableReadLock();
            }
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
                var keys = new List<Type>(this.knownRepositories.Keys);
                foreach (Type key in keys)
                {
                    this.knownRepositories[key].Dispose();
                    this.knownRepositories.Remove(key);
                }
            }
        }
    }
}
// //-----------------------------------------------------------------------
// // <copyright file="ApplicationServiceScope.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

using System;
using AMSLLC.Listener.Domain;
using AMSLLC.Listener.Repository;

namespace AMSLLC.Listener.ApplicationService
{
    public class ApplicationServiceScope : IApplicationServiceScope
    {
        private ApplicationServiceScope(IDomainBuilder domainBuilder, IRepositoryBuilder repositoryBuilder)
        {
            DomainBuilder = domainBuilder;
            RepositoryBuilder = repositoryBuilder;
        }

        public IDomainBuilder DomainBuilder { get; } = null;
        public IRepositoryBuilder RepositoryBuilder { get; } = null;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static IApplicationServiceScope Create(IDomainBuilder domainBuilder,
            IRepositoryBuilder repositoryBuilder = null)
        {
            return new ApplicationServiceScope(domainBuilder, repositoryBuilder);
        }


        /// <summary>
        /// Finalizes an instance of the <see cref="ApplicationServiceScope" /> class.
        /// </summary>
        ~ApplicationServiceScope()
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
                RepositoryBuilder?.Dispose();
            }
        }
    }
}
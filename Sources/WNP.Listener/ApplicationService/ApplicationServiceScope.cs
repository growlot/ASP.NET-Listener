// //-----------------------------------------------------------------------
// // <copyright file="ApplicationServiceScope.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using Core;
    using Domain;
    using Repository;

    public class ApplicationServiceScope : IApplicationServiceScope
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        private ApplicationServiceScope(IDomainBuilder domainBuilder, IRepositoryManager repositoryBuilder,
            IDateTimeProvider dateTimeProvider)
        {
            this.DomainBuilder = domainBuilder;
            this.RepositoryBuilder = repositoryBuilder;
            this._dateTimeProvider = dateTimeProvider;
            this.ScopeDateTime = this._dateTimeProvider.Now();
        }

        public DateTime ScopeDateTime { get; }

        /// <summary>
        /// Nows this instance.
        /// </summary>
        /// <returns>DateTime.</returns>
        public DateTime Now => this._dateTimeProvider.Now();

        public IDomainBuilder DomainBuilder { get; } = null;
        public IRepositoryManager RepositoryBuilder { get; } = null;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static IApplicationServiceScope Create()
        {
            return new ApplicationServiceScope(ApplicationIntegration.DependencyResolver.ResolveType<IDomainBuilder>(),
                ApplicationIntegration.DependencyResolver.ResolveType<IRepositoryManager>(),
                ApplicationIntegration.DependencyResolver.ResolveType<IDateTimeProvider>());
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
                this.RepositoryBuilder?.Dispose();
            }
        }
    }
}
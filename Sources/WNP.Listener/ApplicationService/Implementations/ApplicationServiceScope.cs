// //-----------------------------------------------------------------------
// <copyright file="ApplicationServiceScope.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Implementations
{
    using System;
    using Core;
    using Domain;
    using Repository;

    /// <summary>
    /// Implements <see cref="IApplicationServiceScope"/>
    /// </summary>
    public class ApplicationServiceScope : IApplicationServiceScope
    {
        private readonly IDateTimeProvider dateTimeProvider;

        private ApplicationServiceScope(IDomainBuilder domainBuilder, IRepositoryManager repositoryBuilder, IDateTimeProvider dateTimeProvider)
        {
            this.DomainBuilder = domainBuilder;
            this.RepositoryBuilder = repositoryBuilder;
            this.dateTimeProvider = dateTimeProvider;
            this.ScopeCreated = this.dateTimeProvider.Now();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ApplicationServiceScope" /> class.
        /// </summary>
        ~ApplicationServiceScope()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public DateTime ScopeCreated { get; }

        /// <inheritdoc/>
        public DateTime Now => this.dateTimeProvider.Now();

        /// <inheritdoc/>
        public IDomainBuilder DomainBuilder { get; } = null;

        /// <inheritdoc/>
        public IRepositoryManager RepositoryBuilder { get; } = null;

        /// <summary>
        /// Creates new instance of application service scope.
        /// </summary>
        /// <returns>New instance of application service scope</returns>
        public static IApplicationServiceScope Create()
        {
            return new ApplicationServiceScope(
                ApplicationIntegration.DependencyResolver.ResolveType<IDomainBuilder>(),
                ApplicationIntegration.DependencyResolver.ResolveType<IRepositoryManager>(),
                ApplicationIntegration.DependencyResolver.ResolveType<IDateTimeProvider>());
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
                this.RepositoryBuilder?.Dispose();
            }
        }
    }
}
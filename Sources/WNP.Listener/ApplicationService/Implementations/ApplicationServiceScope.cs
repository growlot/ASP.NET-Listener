// //-----------------------------------------------------------------------
// <copyright file="ApplicationServiceScope.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Implementations
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Domain;
    using Repository;

    /// <summary>
    /// Implements <see cref="IApplicationServiceScope"/>
    /// </summary>
    public class ApplicationServiceScope : IApplicationServiceScope
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IDependencyInjectionAdapter di;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationServiceScope"/> class.
        /// </summary>
        public ApplicationServiceScope()
            : this(Guid.NewGuid().ToString("D"))
        {
        }

        private ApplicationServiceScope(string key)
            : this(ApplicationIntegration.DependencyResolver.WithScope(key, "ApplicationScopeModule"))
        {
        }

        private ApplicationServiceScope(IDomainBuilder domainBuilder, IRepositoryManager repositoryBuilder, IDateTimeProvider dateTimeProvider)
        {
            this.DomainBuilder = domainBuilder;
            this.RepositoryBuilder = repositoryBuilder;
            this.dateTimeProvider = dateTimeProvider;
            this.ScopeCreated = this.dateTimeProvider.Now();
        }

        private ApplicationServiceScope(IDependencyInjectionAdapter di)
            : this(
                  di.ResolveType<IDomainBuilder>(),
                di.ResolveType<IRepositoryManager>(new KeyValuePair<string, object>("container", di)),
                di.ResolveType<IDateTimeProvider>())
        {
            this.di = di;
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
        public IDomainBuilder DomainBuilder { get; private set; }

        /// <inheritdoc/>
        public IRepositoryManager RepositoryBuilder { get; private set; }

        /// <summary>
        /// Creates new instance of application service scope.
        /// </summary>
        /// <returns>New instance of application service scope</returns>
        public static IApplicationServiceScope Create()
        {
            var scopeKey = Guid.NewGuid().ToString("D");
            return new ApplicationServiceScope(scopeKey);
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
                this.di.Dispose();
                this.DomainBuilder = null;
                this.RepositoryBuilder?.Dispose();
                this.RepositoryBuilder = null;
            }
        }
    }
}
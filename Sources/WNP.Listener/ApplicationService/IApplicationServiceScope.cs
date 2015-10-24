// //-----------------------------------------------------------------------
// <copyright file="IApplicationServiceScope.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using Domain;
    using Repository;

    /// <summary>
    /// Defines the scope in which applicaiton service is running
    /// </summary>
    public interface IApplicationServiceScope : IDisposable
    {
        /// <summary>
        /// Gets the scope creation time.
        /// </summary>
        /// <value>
        /// The scope creation time.
        /// </value>
        DateTime ScopeCreated { get; }

        /// <summary>
        /// Gets current time of the application scope.
        /// </summary>
        /// <value>
        /// The current time of the application scope.
        /// </value>
        DateTime Now { get; }

        /// <summary>
        /// Gets the domain builder.
        /// </summary>
        /// <value>
        /// The domain builder.
        /// </value>
        IDomainBuilder DomainBuilder { get; }

        /// <summary>
        /// Gets the repository builder.
        /// </summary>
        /// <value>
        /// The repository builder.
        /// </value>
        IRepositoryManager RepositoryBuilder { get; }
    }
}
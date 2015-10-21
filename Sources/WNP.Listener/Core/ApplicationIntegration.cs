// <copyright file="ApplicationIntegration.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core
{
    /// <summary>
    /// Provides access to dependency resolution management.
    /// </summary>
    public static class ApplicationIntegration
    {
        /// <summary>
        /// Gets the dependency resolver.
        /// </summary>
        /// <value>
        /// The dependency resolver.
        /// </value>
        public static IDependencyInjectionAdapter DependencyResolver { get; private set; }

        /// <summary>
        /// Sets the dependency injection resolver.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        public static void SetDependencyInjectionResolver(IDependencyInjectionAdapter adapter)
        {
            DependencyResolver = adapter;
        }
    }
}

// <copyright file="IDependencyInjectionModule.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core
{
    /// <summary>
    /// Dependency injection module
    /// </summary>
    public interface IDependencyInjectionModule
    {
        /// <summary>
        /// Initializes the module using the provided container.
        /// </summary>
        /// <param name="container">The container.</param>
        void Initialize(
            object container);
    }
}
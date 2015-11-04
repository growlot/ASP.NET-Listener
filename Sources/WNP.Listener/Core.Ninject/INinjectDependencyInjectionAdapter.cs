// <copyright file="INinjectDependencyInjectionAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core.Ninject
{
    using System;
    using global::Ninject;

    /// <summary>
    /// Ninject dependency injection adapter
    /// </summary>
    public interface INinjectDependencyInjectionAdapter : IDependencyInjectionAdapter
    {
        /// <summary>
        /// Initializes specified action with Ninject dependency resolver.
        /// </summary>
        /// <param name="action">The action.</param>
        void Initialize(
            Action<StandardKernel> action);
    }
}
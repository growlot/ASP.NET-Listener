// <copyright file="NinjectDependencyInjectionAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core.Ninject
{
    using System;
    using global::Ninject;

    /// <summary>
    /// Implements <see cref="IDependencyInjectionAdapter"/> for Ninject
    /// </summary>
    public class NinjectDependencyInjectionAdapter : IDependencyInjectionAdapter
    {
        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>
        /// The kernel.
        /// </value>
        protected StandardKernel Kernel { get; } = new StandardKernel();

        /// <inheritdoc/>
        public TType ResolveType<TType>()
        {
            return this.Kernel.Get<TType>();
        }

        /// <inheritdoc/>
        public object ResolveType(Type type)
        {
            return this.Kernel.Get(type);
        }

        /// <inheritdoc/>
        public TType ResolveNamed<TType>(string name)
        {
            return this.Kernel.Get<TType>(name);
        }

        /// <summary>
        /// Initializes Ninject dependency resolver.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Initialize(Action<StandardKernel> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Can not initialize Ninject Kernel because action is not specified.");
            }

            action(this.Kernel);
        }
    }
}

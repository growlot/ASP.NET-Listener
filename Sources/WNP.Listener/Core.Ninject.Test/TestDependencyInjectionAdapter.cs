// <copyright file="TestDependencyInjectionAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core.Ninject.Test
{
    using System;
    using System.Collections.Generic;
    using global::Ninject.Extensions.ChildKernel;
    using global::Ninject.Syntax;

    /// <summary>
    /// Class TestDependencyInjectionAdapter.
    /// </summary>
    public class TestDependencyInjectionAdapter : NinjectDependencyInjectionAdapter
    {
        private Dictionary<Type, object> reboundInstances = new Dictionary<Type, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDependencyInjectionAdapter"/> class.
        /// </summary>
        public TestDependencyInjectionAdapter()
        {
        }

        private TestDependencyInjectionAdapter(ChildKernel kernel)
            : base(kernel)
        {
        }

        /// <summary>
        /// Rebinds the specified to.
        /// </summary>
        /// <typeparam name="TType">The type of the t type.</typeparam>
        /// <param name="to">To.</param>
        /// <returns>IBindingWhenInNamedWithOrOnSyntax&lt;TType&gt;.</returns>
        public IBindingWhenInNamedWithOrOnSyntax<TType> Rebind<TType>(
            TType to)
        {
            this.Kernel.Unbind<TType>();
            this.reboundInstances.Add(typeof(TType), to);
            return this.Kernel.Rebind<TType>().ToConstant(to);
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            foreach (var reboundInstance in this.reboundInstances)
            {
                this.Kernel.Release(reboundInstance);
            }

            this.reboundInstances.Clear();
        }

        /// <summary>
        /// Creates the adapter.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <returns>IDependencyInjectionAdapter.</returns>
        protected override IDependencyInjectionAdapter CreateAdapter(
            ChildKernel kernel)
        {
            return new TestDependencyInjectionAdapter(kernel);
        }
    }
}
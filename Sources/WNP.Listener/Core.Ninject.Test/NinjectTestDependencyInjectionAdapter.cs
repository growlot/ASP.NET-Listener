// <copyright file="NinjectTestDependencyInjectionAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core.Ninject.Test
{
    /// <summary>
    /// Implements <see cref="ITestDependencyInjectionAdapter"/> for Ninject
    /// </summary>
    public class NinjectTestDependencyInjectionAdapter : NinjectDependencyInjectionAdapter, ITestDependencyInjectionAdapter
    {
        /// <summary>
        /// Rebinds to object.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <inheritdoc />
        public void RebindToObject<TInterface, TObject>(TObject obj)
            where TObject : TInterface
        {
            this.Kernel.Rebind<TInterface>().ToConstant(obj).InSingletonScope();
        }

        /// <summary>
        /// Rebinds the named to object.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        public void RebindNamedToObject<TInterface, TObject>(TObject obj, string name)
            where TObject : TInterface
        {
            this.Kernel.Rebind<TInterface>().ToConstant(obj).InSingletonScope().Named(name);
        }
    }
}

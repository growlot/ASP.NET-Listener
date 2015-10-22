// <copyright file="ITestDependencyInjectionAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core
{
    using System;

    /// <summary>
    /// Abstracts dependency injection framework.
    /// </summary>
    public interface ITestDependencyInjectionAdapter : IDependencyInjectionAdapter
    {
        /// <summary>
        /// Rebinds specified interface to specific object.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "There is no argument for interface type, and specifying it as Type causes difficulties in interface implementation")]
        void RebindToObject<TInterface, TObject>(TObject obj)
            where TObject : TInterface;

        /// <summary>
        /// Rebinds specified interface to specific object.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="name">The name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "There is no argument for interface type, and specifying it as Type causes difficulties in interface implementation")]
        void RebindNamedToObject<TInterface, TObject>(TObject obj, string name)
            where TObject : TInterface;
    }
}

// <copyright file="IDependencyInjectionAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core
{
    using System;

    /// <summary>
    /// Abstracts dependency injection framework.
    /// </summary>
    public interface IDependencyInjectionAdapter
    {
        /// <summary>
        /// Gets the object of specified type from DI based on generic type definition.
        /// </summary>
        /// <typeparam name="TType">The type to resolve.</typeparam>
        /// <returns>The resolved object.</returns>
        TType ResolveType<TType>();

        /// <summary>
        /// Gets the object of specified type from DI.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The resolved object.</returns>
        object ResolveType(Type type);

        /// <summary>
        /// Gets the object of specified interface. Specific type is selected by name.
        /// </summary>
        /// <typeparam name="TType">The interface to resolve.</typeparam>
        /// <param name="name">The name of the specific implementation.</param>
        /// <returns>The resolved object.</returns>
        TType ResolveNamed<TType>(string name);
    }
}

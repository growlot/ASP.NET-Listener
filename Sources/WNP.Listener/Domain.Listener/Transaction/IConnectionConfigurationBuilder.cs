// //-----------------------------------------------------------------------
// <copyright file="IConnectionConfigurationBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Connection configuration builder
    /// </summary>
    public interface IConnectionConfigurationBuilder
    {
        /// <summary>
        /// Create the connection configuration using specified memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        /// <returns>IConnectionConfiguration.</returns>
        IConnectionConfiguration Create(IMemento memento);
    }
}
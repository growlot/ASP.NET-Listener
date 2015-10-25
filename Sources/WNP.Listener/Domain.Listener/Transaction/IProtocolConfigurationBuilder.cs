// <copyright file="IProtocolConfigurationBuilder.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using Communication;

    /// <summary>
    /// Protocol configuration builder interface
    /// </summary>
    public interface IProtocolConfigurationBuilder
    {
        /// <summary>
        /// Creates the specified my memento.
        /// </summary>
        /// <param name="myMemento">My memento.</param>
        /// <returns>IProtocolConfiguration.</returns>
        IProtocolConfiguration Create(IMemento myMemento);
    }
}
// <copyright file="ProtocolConfigurationFactory.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using Communication;
    using Core;

    /// <summary>
    /// Protocol configuration factory
    /// </summary>
    public static class ProtocolConfigurationFactory
    {
        /// <summary>
        /// Creating the protocol configuration for the specified protocol
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="myMemento">Memento.</param>
        /// <returns>Connection configuration</returns>
        public static IProtocolConfiguration Create(string protocol, IMemento myMemento)
        {
            var builder = ApplicationIntegration.DependencyResolver.ResolveNamed<IProtocolConfigurationBuilder>("protocol-builder-{0}".FormatWith(protocol));

            return builder.Create(myMemento);
        }
    }
}
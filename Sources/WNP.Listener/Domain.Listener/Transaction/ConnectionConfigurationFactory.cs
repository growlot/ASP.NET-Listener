// //-----------------------------------------------------------------------
// <copyright file="ConnectionConfigurationFactory.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using Communication;
    using Core;

    /// <summary>
    /// Class ConnectionConfigurationFactory.
    /// </summary>
    public static class ConnectionConfigurationFactory
    {
        /// <summary>
        /// Creating the connection configuration for the specified protocol
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="myMemento">Memento.</param>
        /// <returns>Connection configuration</returns>
        public static IConnectionConfiguration Create(string protocol, IMemento myMemento)
        {
            var builder =
                ApplicationIntegration.DependencyResolver.ResolveNamed<IConnectionConfigurationBuilder>(
                    "connection-builder-{0}".FormatWith(protocol));

            return builder.Create(myMemento);
        }
    }
}
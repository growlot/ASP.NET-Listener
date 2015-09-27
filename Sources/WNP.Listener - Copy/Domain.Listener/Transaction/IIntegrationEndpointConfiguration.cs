// //-----------------------------------------------------------------------
// // <copyright file="IIntegrationEndpointConfiguration.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Integration endpoint configuration marker
    /// </summary>
    public interface IIntegrationEndpointConfiguration
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }
    }
}
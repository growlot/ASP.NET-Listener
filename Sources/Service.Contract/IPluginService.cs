//-----------------------------------------------------------------------
// <copyright file="IPluginService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Contract
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interface that needs to be implemented by each plugin that will be hosting web services.
    /// </summary>
    public interface IPluginService
    {
        /// <summary>
        /// Gets all the service types hosted by this addin.
        /// These types are used for web service registration in service host.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        IEnumerable<Type> ServiceTypes { get; }
    }
}

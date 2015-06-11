//-----------------------------------------------------------------------
// <copyright file="INamedPluginMetadata.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Contract
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    /// <summary>
    /// Interface that defines name metadata for the plugin.
    /// </summary>
    public interface INamedPluginMetadata
    {
        /// <summary>
        /// Gets the name of plugin.
        /// </summary>
        /// <value>
        /// The plugin name.
        /// </value>
        string Name { get; }
    }
}

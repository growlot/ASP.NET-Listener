//-----------------------------------------------------------------------
// <copyright file="PluginService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.KCPL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;
    using AMSLLC.Listener.Service.Contract;

    /// <summary>
    /// Implements <see cref="IPluginService"/> interface for KCP&amp;L.
    /// </summary>
    [ExportNamedPlugin(typeof(IPluginService), "KCP&L")]
    public class PluginService : IPluginService
    {
        /// <summary>
        /// The service types hosted by Alliant plugin.
        /// </summary>
        private IEnumerable<Type> serviceTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginService"/> class.
        /// </summary>
        public PluginService()
        {
            this.serviceTypes = new Type[2]
            {
                typeof(CustomService),
                typeof(TransactionResponseService)
            };
        }

        /// <summary>
        /// Gets all the service types hosted by this addin.
        /// These types are used for web service registration in service host.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        public IEnumerable<Type> ServiceTypes
        {
            get
            {
                return this.serviceTypes;
            }
        }
    }
}

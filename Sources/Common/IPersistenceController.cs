//-----------------------------------------------------------------------
// <copyright file="IPersistenceController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Interface to access different business systems implementing persistence.
    /// </summary>
    public interface IPersistenceController
    {
        /// <summary>
        /// Gets the listener system.
        /// </summary>
        /// <value>
        /// The listener system.
        /// </value>
        ListenerSystem ListenerSystem { get; }

        /// <summary>
        /// Gets the listener client system.
        /// </summary>
        /// <value>
        /// The listener client system.
        /// </value>
        ListenerClientSystem ListenerClientSystem { get; }

        /// <summary>
        /// Initializes systems working with listener persistence providers.
        /// </summary>
        /// <param name="listenerPersistenceManager">The listener persistence manager.</param>
        void InitializeListenerSystems(IPersistenceManager listenerPersistenceManager);

        /// <summary>
        /// Initializes systems working with listener client persistence providers.
        /// </summary>
        /// <param name="clientPersistenceManager">The client persistence manager.</param>
        void InitializeListenerClientSystems(IPersistenceManager clientPersistenceManager);

        /// <summary>
        /// Check if listener systems initialized.
        /// </summary>
        /// <returns>True if listener systems are initialized. False if not.</returns>
        bool ListenerSystemsInitialized();

        /// <summary>
        /// Check if listener client systems initialized.
        /// </summary>
        /// <returns>True if listener client systems are initialized. False if not.</returns>
        bool ListenerClientSystemsInitialized();
    }
}

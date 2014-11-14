//-----------------------------------------------------------------------
// <copyright file="PersistenceController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;

    /// <summary>
    /// Provides access to various data model systems depending on provided PersistenceManager
    /// </summary>
    public class PersistenceController : IPersistenceController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceController"/> class.
        /// </summary>
        public PersistenceController()
        {
            this.ListenerSystem = null;
            this.ListenerClientSystem = null;
        }

        /// <summary>
        /// Gets the listener system.
        /// </summary>
        /// <value>
        /// The listener system.
        /// </value>
        public ListenerSystem ListenerSystem { get; private set; }

        /// <summary>
        /// Gets the listener client system.
        /// </summary>
        /// <value>
        /// The listener client system.
        /// </value>
        public ListenerClientSystem ListenerClientSystem { get; private set; }

        /// <summary>
        /// Initializes systems working with listener persistence providers.
        /// </summary>
        /// <param name="listenerPersistenceManager">The listener persistence manager.</param>
        public void InitializeListenerSystems(IPersistenceManager listenerPersistenceManager)
        {
            if (this.ListenerSystem == null)
            {
                this.ListenerSystem = new ListenerSystem(listenerPersistenceManager);
            }
        }

        /// <summary>
        /// Initializes systems working with listener client persistence providers.
        /// </summary>
        /// <param name="clientPersistenceManager">The client persistence manager.</param>
        public virtual void InitializeListenerClientSystems(IPersistenceManager clientPersistenceManager)
        {
            if (this.ListenerClientSystem == null)
            {
                this.ListenerClientSystem = new ListenerClientSystem(clientPersistenceManager);
            }
        }

        /// <summary>
        /// Check if listener systems initialized.
        /// </summary>
        /// <returns>True if listener systems are initialized. False if not.</returns>
        public bool ListenerSystemsInitialized()
        {
            if (this.ListenerSystem != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if listener client systems initialized.
        /// </summary>
        /// <returns>True if listener client systems are initialized. False if not.</returns>
        public virtual bool ListenerClientSystemsInitialized()
        {
            if (this.ListenerClientSystem != null)
            {
                return true;
            }

            return false;
        }
    }
}

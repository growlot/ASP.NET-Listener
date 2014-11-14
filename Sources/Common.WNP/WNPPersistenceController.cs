//-----------------------------------------------------------------------
// <copyright file="WNPPersistenceController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AMSLLC.Listener.Common;

    /// <summary>
    /// Provides access to various data model systems depending on provided PersistenceManager.
    /// Additionally provides access to client specific systems
    /// </summary>
    public class WNPPersistenceController : PersistenceController, IWNPPersistenceController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WNPPersistenceController"/> class.
        /// </summary>
        public WNPPersistenceController()
        {
            this.WNPSystem = null;
        }

        /// <summary>
        /// Gets the WNP system.
        /// </summary>
        /// <value>
        /// The WNP system.
        /// </value>
        public WNPSystem WNPSystem { get; private set; }

        /// <summary>
        /// Initializes systems working with listener client persistence providers.
        /// </summary>
        /// <param name="clientPersistenceManager">The client persistence manager.</param>
        public override void InitializeListenerClientSystems(IPersistenceManager clientPersistenceManager)
        {
            base.InitializeListenerClientSystems(clientPersistenceManager);

            if (this.WNPSystem == null)
            {
                this.WNPSystem = new WNPSystem(clientPersistenceManager);
            }
        }

        /// <summary>
        /// Check if listener client systems initialized.
        /// </summary>
        /// <returns>True if listener client systems are initialized. False if not.</returns>
        public override bool ListenerClientSystemsInitialized()
        {
            if (base.ListenerClientSystemsInitialized() && this.WNPSystem != null)
            {
                return true;
            }

            return false;
        }
    }
}

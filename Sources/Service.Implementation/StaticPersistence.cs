//-----------------------------------------------------------------------
// <copyright file="StaticPersistence.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation
{
    using System.Configuration;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.WNP;

    /// <summary>
    /// Class that keeps persistence objects available while service is not used.
    /// </summary>
    public static class StaticPersistence
    {
        /// <summary>
        /// Initializes static members of the <see cref="StaticPersistence"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "The field initialization process is quite complex and has to be extracted as a constructor")]
        static StaticPersistence()
        {
            using (IPersistenceManager persistenceManager = new WNPPersistenceManager(ConfigurationManager.ConnectionStrings["ListenerDb"].ConnectionString))
            {
                using (IPersistenceManager clientPersistenceManager = new WNPPersistenceManager(ConfigurationManager.ConnectionStrings["WnpDb"].ConnectionString))
                {
                    IWNPPersistenceController persistenceController = new WNPPersistenceController();
                    persistenceController.InitializeListenerSystems(persistenceManager);
                    persistenceController.InitializeListenerClientSystems(clientPersistenceManager);
                    TransactionLogManager = new TransactionManager(persistenceController);
                    DeviceManager = new DeviceManager(persistenceController);
                    WnpSystem = persistenceController.WNPSystem;
                }
            }
        }

        /// <summary>
        /// Gets the transaction log manager.
        /// </summary>
        /// <value>
        /// The transaction log manager.
        /// </value>
        public static ITransactionManager TransactionLogManager { get; private set; }

        /// <summary>
        /// Gets the device manager.
        /// </summary>
        /// <value>
        /// The device manager.
        /// </value>
        public static IDeviceManager DeviceManager { get; private set; }

        /// <summary>
        /// Gets the WNP system.
        /// </summary>
        /// <value>
        /// The WNP system.
        /// </value>
        public static WNPSystem WnpSystem { get; private set; }
    }
}

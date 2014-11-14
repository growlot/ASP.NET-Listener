//-----------------------------------------------------------------------
// <copyright file="ListenerDataProvider.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AMSLLC.Listener.Client.Implementation.Messages;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Model;

    /// <summary>
    /// Class providing access to data stored in Listener system.
    /// </summary>
    public class ListenerDataProvider : IListenerDataProvider
    {
        /// <summary>
        /// The transaction manager
        /// </summary>
        private ITransactionManager transactionLogManager;

        /// <summary>
        /// The device manager
        /// </summary>
        private IDeviceManager deviceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerDataProvider"/> class.
        /// </summary>
        public ListenerDataProvider()
        {
            using (IPersistenceManager persistenceManager = new PersistenceManager())
            {
                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerSystems(persistenceManager);
                this.transactionLogManager = new TransactionManager(persistenceController);
                this.deviceManager = new DeviceManager(persistenceController);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerDataProvider"/> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        public ListenerDataProvider(IPersistenceManager persistenceManager)
        {
            IPersistenceController persistenceController = new PersistenceController();
            persistenceController.InitializeListenerSystems(persistenceManager);
            this.transactionLogManager = new TransactionManager(persistenceController);
            this.deviceManager = new DeviceManager(persistenceController);
        }

        /// <summary>
        /// Gets the transaction log.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// List of transaction log entries for specified device
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request;Can not retrieve transaction log when request is not specified</exception>
        public IList<TransactionLogResponse> GetTransactionLog(TransactionLogRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not retrieve transaction log when request is not specified.");
            }

            EquipmentType equipmentType = this.deviceManager.GetEquipmentTypeByInternalCode(request.ServiceType, request.EquipmentType);

            Device device = this.deviceManager.GetDevice(request.CompanyId, request.EquipmentNumber, equipmentType.Id);
            IList<TransactionLogResponse> logResponse = new List<TransactionLogResponse>();

            if (device != null)
            {
                IList<TransactionLog> log = this.transactionLogManager.GetDeviceTransactions(device.Id);

                logResponse = ((List<TransactionLog>)log).ConvertAll<TransactionLogResponse>(x => new TransactionLogResponse
                {
                    TransactionSource = x.TransactionSource.Description,
                    TransactionStatus = x.TransactionStatus.Description,
                    TransactionType = x.TransactionType.Description,
                    TransactionStart = x.TransactionLogState.OrderByDescending(y => y.Id).Last().ExecutionTime,
                    TestDate = (x.DeviceTest != null) ? (DateTime?)x.DeviceTest.TestDate : null
                });
            }

            return logResponse;
        }
    }
}

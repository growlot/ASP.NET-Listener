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
    using AMSLLC.Listener.Common.Lookup;
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

            IList<TransactionLogResponse> logResponse = new List<TransactionLogResponse>();
            TransactionLog searchCriteria = new TransactionLog();
            if (!string.IsNullOrWhiteSpace(request.EquipmentNumber) && !string.IsNullOrWhiteSpace(request.EquipmentType))
            {
                EquipmentType equipmentType = this.deviceManager.GetEquipmentTypeByInternalCode(request.ServiceType, request.EquipmentType);
                if (equipmentType == null)
                {
                    return logResponse;
                }

                searchCriteria.Device = this.deviceManager.GetDevice(request.CompanyId, request.EquipmentNumber, equipmentType.Id);
                if (searchCriteria.Device == null)
                {
                    return logResponse;
                }
            }

            if (request.FailedOnly)
            {
                searchCriteria.TransactionStatus = new TransactionStatus((int)TransactionStatusLookup.Failed);
            }

            searchCriteria.TransactionStart = request.LogDate;

            IList<TransactionLog> log = this.transactionLogManager.GetTransactions(searchCriteria, request.IncludeSkipped);

            logResponse = ((List<TransactionLog>)log).ConvertAll<TransactionLogResponse>(x => new TransactionLogResponse
            {
                TransactionSource = x.TransactionType.TransactionSource.Description,
                TransactionStatus = x.TransactionStatus.Description,
                TransactionType = x.TransactionType.Name,
                TransactionStart = (DateTime)x.TransactionStart,
                TestDate = x.DeviceTest != null ? (DateTime?)x.DeviceTest.TestDate : null,
                ErrorMessage = request.IncludeDetails ? x.Message : null,
                DebugInfo = request.IncludeDetails ? x.DebugInfo : null,
                EquipmentNumber = x.Device != null ? x.Device.EquipmentNumber : null,
                EquipmentType = x.Device != null ? x.Device.EquipmentType.Description : null
            });

            return logResponse;
        }
    }
}

﻿//-----------------------------------------------------------------------
// <copyright file="ListenerDataProvider.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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

            bool searchSpecified = false;

            IList<TransactionLogResponse> logResponse = new List<TransactionLogResponse>();
            TransactionLog searchCriteria = new TransactionLog();
            if (!string.IsNullOrWhiteSpace(request.EquipmentNumber) && !string.IsNullOrWhiteSpace(request.EquipmentType))
            {
                EquipmentType equipmentType = this.deviceManager.GetEquipmentTypeByInternalCode(request.ServiceType, request.EquipmentType);
                if (equipmentType == null)
                {
                    return logResponse;
                }

                Company company = this.deviceManager.GetCompanyByInternalCode(request.CompanyId.ToString(CultureInfo.InvariantCulture));
                if (company == null)
                {
                    return logResponse;
                }

                searchCriteria.Device = this.deviceManager.GetDevice(company.Id, request.EquipmentNumber, equipmentType.Id);
                if (searchCriteria.Device == null)
                {
                    return logResponse;
                }

                searchSpecified = true;
            }

            if (!string.IsNullOrWhiteSpace(request.BatchNumber))
            {
                searchCriteria.DeviceBatch = this.deviceManager.GetDeviceBatchByBatchNumber(request.BatchNumber);
                searchSpecified = true;
            }

            if (request.FailedOnly)
            {
                searchCriteria.TransactionStatus = new TransactionStatus((int)TransactionStatusLookup.Failed);
                searchSpecified = true;
            }

            if (request.LogDate.HasValue)
            {
                searchCriteria.TransactionStart = request.LogDate;
                searchSpecified = true;
            }

            // if none search criteria options are specified, then return empty transaction list.
            if (!searchSpecified)
            {
                return logResponse;
            }

            IList<TransactionLog> log = this.transactionLogManager.GetTransactions(searchCriteria, request.IncludeSkipped);

            logResponse = ((List<TransactionLog>)log).ConvertAll<TransactionLogResponse>(x => new TransactionLogResponse
            {
                TransactionSource = x.TransactionType.TransactionSource.Description,
                TransactionStatus = x.TransactionStatus.Description,
                TransactionType = x.TransactionType.Name,
                TransactionStart = (DateTime)x.TransactionStart,
                TestDate = x.DeviceTest != null ? (DateTime?)x.DeviceTest.TestDate : null,
                BatchNumber = x.DeviceBatch != null ? x.DeviceBatch.BatchNumber : string.Empty,
                ErrorMessage = request.IncludeDetails && x.Message != null ? x.Message : string.Empty,
                DebugInfo = request.IncludeDetails && x.DebugInfo != null ? x.DebugInfo : string.Empty,
                EquipmentNumber = x.Device != null ? x.Device.EquipmentNumber : string.Empty,
                EquipmentType = x.Device != null ? x.Device.EquipmentType.Description : string.Empty
            });

            return logResponse;
        }
    }
}

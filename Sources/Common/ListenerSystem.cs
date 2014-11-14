﻿//-----------------------------------------------------------------------
// <copyright file="ListenerSystem.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AMSLLC.Listener.Common.Model;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    /// <summary>
    /// Manages all business objects related to Listener system.
    /// </summary>
    public class ListenerSystem
    {
        /// <summary>
        /// The persistence manager
        /// </summary>
        private IPersistenceManager persistenceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerSystem" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        public ListenerSystem(IPersistenceManager persistenceManager)
        {
            this.persistenceManager = persistenceManager;
        }        

        /* TransactionStatus retrievedTransactionStatus = persistenceManager.RetrieveFirstEqual<TransactionStatus>("Id", (int)Lookups.TransactionStatus.InProgress);
         * IList<TransactionType> allTransactionTypes = persistenceManager.RetrieveAll<TransactionType>(SessionAction.BeginAndEnd);
         * allTransactionTypes.FirstOrDefault(c => c.Id == (int)transactionType); */

        /// <summary>
        /// Updates the state of the transaction.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="transactionStateId">The transaction state identifier.</param>
        public void UpdateTransactionLogState(int transactionId, int transactionStateId)
        {
            TransactionLogState transactionStatistics = new TransactionLogState();
            transactionStatistics.ExecutionTime = DateTime.Now;
            transactionStatistics.TransactionLog = new TransactionLog(transactionId);
            transactionStatistics.TransactionState = new TransactionState(transactionStateId);

            this.persistenceManager.Save<TransactionLogState>(transactionStatistics);
        }

        /// <summary>
        /// Updates the transaction status.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="transactionStatusId">The transaction status identifier.</param>
        /// <param name="message">The message.</param>
        /// <param name="debugInfo">The debug information.</param>
        public void UpdateTransactionLogStatus(int transactionId, int transactionStatusId, string message, string debugInfo)
        {
            TransactionLog transaction = this.persistenceManager.RetrieveFirstEqual<TransactionLog>("Id", transactionId);

            transaction.TransactionStatus = new TransactionStatus(transactionStatusId);
            transaction.Message = message;
            transaction.DebugInfo = debugInfo;

            this.persistenceManager.Save<TransactionLog>(transaction);
        }

        /// <summary>
        /// Logs the new transaction.
        /// </summary>
        /// <param name="transactionTypeId">The transaction type identifier.</param>
        /// <param name="transactionStatusId">The transaction status identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceTestId">The device test identifier.</param>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="transactionSourceId">The transaction source identifier.</param>
        /// <returns>
        /// Id of new transaction.
        /// </returns>
        public int AddTransactionLog(int transactionTypeId, int transactionStatusId, int? deviceId, int? deviceTestId, int? batchId, int transactionSourceId)
        {
            TransactionLog transaction = new TransactionLog();
            transaction.TransactionType = new TransactionType(transactionTypeId);
            transaction.TransactionStatus = new TransactionStatus(transactionStatusId);
            if (deviceId != null)
            {
                transaction.Device = new Device((int)deviceId);
            }

            if (deviceTestId != null)
            {
                transaction.DeviceTest = new DeviceTest((int)deviceTestId);
            }

            if (batchId != null)
            {
                transaction.Batch = new Batch((int)batchId);
            }

            transaction.TransactionSource = new TransactionSource(transactionSourceId);

            this.persistenceManager.Save<TransactionLog>(transaction);

            return transaction.Id;
        }

        /// <summary>
        /// Gets the transaction list for specified device
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// Returns list of device transactions.
        /// </returns>
        public IList<TransactionLog> GetDeviceTransactions(int deviceId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TransactionLog>();
            criteria.Add(Restrictions.Eq("Device", new Device(deviceId)));
            criteria.SetResultTransformer(Transformers.DistinctRootEntity);

            return this.persistenceManager.RetrieveAllEqual<TransactionLog>(criteria);
        }

        /// <summary>
        /// Adds the device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns>
        /// New device
        /// </returns>
        public Device AddDevice(Device device)
        {
            this.persistenceManager.Save<Device>(device);

            return device;
        }

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="equipmentTypeId">The equipment type identifier.</param>
        /// <returns>
        /// The device loaded from persistent storage
        /// </returns>
        public Device GetDevice(int companyId, string equipmentNumber, int equipmentTypeId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Device>();
            criteria.Add(Restrictions.Eq("Company", new Company(companyId)));
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("EquipmentType", new EquipmentType(equipmentTypeId)));

            return this.persistenceManager.RetrieveFirstEqual<Device>(criteria);
        }

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// The device loaded from persistent storage
        /// </returns>
        public Device GetDevice(int deviceId)
        {
            return this.persistenceManager.GetByKey<Device>(deviceId);
        }
        
        /// <summary>
        /// Saves the device test.
        /// </summary>
        /// <param name="deviceTest">The device test object.</param>
        /// <returns>
        /// The device test entity.
        /// </returns>
        public DeviceTest SaveDeviceTest(DeviceTest deviceTest)
        {
            this.persistenceManager.Save<DeviceTest>(deviceTest);

            return deviceTest;
        }

        /// <summary>
        /// Gets the device test.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="testDate">The test date.</param>
        /// <returns>Device test entry.</returns>
        public DeviceTest GetDeviceTest(int deviceId, DateTime testDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DeviceTest>();
            criteria.Add(Restrictions.Eq("Device", new Device(deviceId)));
            criteria.Add(Restrictions.Eq("TestDate", testDate));

            return this.persistenceManager.RetrieveFirstEqual<DeviceTest>(criteria);
        }

        /// <summary>
        /// Gets the device test.
        /// </summary>
        /// <param name="deviceTestId">The device test identifier.</param>
        /// <returns>
        /// Device test entry.
        /// </returns>
        public DeviceTest GetDeviceTest(int deviceTestId)
        {
            return this.persistenceManager.GetByKey<DeviceTest>(deviceTestId);
        }

        /// <summary>
        /// Gets the equipment type by internal service and equipment type codes.
        /// </summary>
        /// <param name="serviceTypeInternalCode">The service type internal code.</param>
        /// <param name="equipmentTypeInternalCode">The equipment type internal code.</param>
        /// <returns>The equipment type</returns>
        public EquipmentType GetEquipmentTypeByInternalCode(string serviceTypeInternalCode, string equipmentTypeInternalCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ServiceType>();
            criteria.Add(Restrictions.Eq("InternalCode", serviceTypeInternalCode));

            ServiceType serviceType = this.persistenceManager.RetrieveFirstEqual<ServiceType>(criteria);

            criteria = DetachedCriteria.For<EquipmentType>();
            criteria.Add(Restrictions.Eq("ServiceType", serviceType));
            criteria.Add(Restrictions.Eq("InternalCode", equipmentTypeInternalCode));

            return this.persistenceManager.RetrieveFirstEqual<EquipmentType>(criteria);
        }

        /// <summary>
        /// Gets the company by internal code.
        /// </summary>
        /// <param name="internalCode">The company internal code.</param>
        /// <returns>The company</returns>
        public Company GetCompanyByInternalCode(string internalCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Company>();
            criteria.Add(Restrictions.Eq("InternalCode", internalCode));

            return this.persistenceManager.RetrieveFirstEqual<Company>(criteria);
        }

        /// <summary>
        /// Gets all companies.
        /// </summary>
        /// <returns>
        /// Company list.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Method is more appropriate in this place, because data is queried from database")]
        public IList<Company> GetCompanies()
        {
            return this.persistenceManager.RetrieveAll<Company>(SessionAction.BeginAndEnd);
        }
    }
}

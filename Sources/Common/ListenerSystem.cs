//-----------------------------------------------------------------------
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
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using NHibernate.Criterion;
    using NHibernate.SqlCommand;
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
            if (transactionStatusId == (int)TransactionStatusLookup.Succeeded || transactionStatusId == (int)TransactionStatusLookup.Failed)
            {
                transaction.TransactionEnd = DateTime.Now;
            }

            this.persistenceManager.Save<TransactionLog>(transaction);
        }

        /// <summary>
        /// Updates the transaction data hash.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="dataHash">The data hash.</param>
        public void UpdateTransactionDataHash(int transactionId, string dataHash)
        {
            TransactionLog transaction = this.persistenceManager.RetrieveFirstEqual<TransactionLog>("Id", transactionId);

            transaction.DataHash = dataHash;

            this.persistenceManager.Save<TransactionLog>(transaction);
        }

        /// <summary>
        /// Starts new transaction.
        /// </summary>
        /// <param name="transactionLog">The transaction log.</param>
        /// <returns>
        /// The transaction identifier for this new transaction.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">transactionLog;Can not start transaction log if no transaction is specified</exception>
        public int NewTransaction(TransactionLog transactionLog)
        {
            if (transactionLog == null)
            {
                throw new ArgumentNullException("transactionLog", "Can not start transaction log if no transaction is specified");
            }

            transactionLog.TransactionStatus = new TransactionStatus((int)TransactionStatusLookup.InProgress);
            transactionLog.TransactionStart = DateTime.Now;

            this.persistenceManager.Save<TransactionLog>(transactionLog);

            return transactionLog.Id;
        }

        /// <summary>
        /// Gets the transaction list for specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="includeSkipped">If set to <c>true</c> includes skipped transactions in the search results.</param>
        /// <returns>
        /// Returns list of transactions.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">searchCriteria;Can not get transaction log if no search criteria is specified</exception>
        public IList<TransactionLog> GetTransactions(TransactionLog searchCriteria, bool includeSkipped)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria", "Can not get transaction log if no search criteria is specified");
            }

            DetachedCriteria criteria = DetachedCriteria.For<TransactionLog>();
            if (searchCriteria.Device != null)
            {
                criteria.Add(Restrictions.Eq("Device", searchCriteria.Device));
            }

            if (searchCriteria.DeviceBatch != null)
            {
                criteria.Add(Restrictions.Eq("DeviceBatch", searchCriteria.DeviceBatch));
            }

            if (searchCriteria.TransactionStatus != null)
            {
                criteria.Add(Restrictions.Eq("TransactionStatus", searchCriteria.TransactionStatus));
            }

            if (!includeSkipped)
            {
                TransactionStatus skippedStatus = new TransactionStatus((int)TransactionStatusLookup.Skipped);
                criteria.Add(Restrictions.Not(Restrictions.Eq("TransactionStatus", skippedStatus)));
            }

            if (searchCriteria.TransactionType != null)
            {
                criteria.Add(Restrictions.Eq("TransactionType", searchCriteria.TransactionType));
            }

            if (searchCriteria.TransactionStart.HasValue)
            {
                criteria.Add(Restrictions.Ge("TransactionStart", searchCriteria.TransactionStart));
                criteria.Add(Restrictions.Lt("TransactionStart", ((DateTime)searchCriteria.TransactionStart).AddDays(1)));
            }

            criteria.SetResultTransformer(Transformers.DistinctRootEntity);

            return this.persistenceManager.RetrieveAllEqual<TransactionLog>(criteria);
        }

        /// <summary>
        /// Gets the latest successful transaction hash for specified device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <returns>
        /// Returns the data hash.
        /// </returns>
        public string GetLastSuccessfulDeviceTransactionDataHash(Device device, TransactionType transactionType)
        {
            string result = GlobalConstants.PreviousSuccessfulTransactionNotFound;

            DetachedCriteria criteria = DetachedCriteria.For<TransactionLog>();
            criteria.Add(Restrictions.Eq("Device", device));
            criteria.Add(Restrictions.Eq("TransactionStatus", new TransactionStatus((int)TransactionStatusLookup.Succeeded)));
            criteria.Add(Restrictions.Eq("TransactionType", transactionType));

            ProjectionList projectionList = Projections.ProjectionList();
            projectionList.Add(Projections.Property("TransactionStart"), "TransactionStart");
            projectionList.Add(Projections.Property("DataHash"), "DataHash");

            criteria.SetResultTransformer(Transformers.DistinctRootEntity);
            criteria.SetProjection(projectionList).SetResultTransformer(Transformers.AliasToBean<TransactionLog>());

            IList<TransactionLog> transactions = this.persistenceManager.RetrieveAllEqual<TransactionLog>(criteria);

            if (transactions.Count > 0)
            {
                result = transactions.OrderByDescending<TransactionLog, DateTime>(x => (DateTime)x.TransactionStart).First().DataHash;
            }

            return result;
        }

        /// <summary>
        /// Gets the latest successful transaction hash for specified device test.
        /// </summary>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        /// <returns>
        /// Returns the data hash.
        /// </returns>
        public string GetLastSuccessfulDeviceTestTransactionDataHash(DeviceTest deviceTest, TransactionType transactionType)
        {
            string result = GlobalConstants.PreviousSuccessfulTransactionNotFound;

            DetachedCriteria criteria = DetachedCriteria.For<TransactionLog>();
            criteria.Add(Restrictions.Eq("DeviceTest", deviceTest));
            criteria.Add(Restrictions.Eq("TransactionStatus", new TransactionStatus((int)TransactionStatusLookup.Succeeded)));
            criteria.Add(Restrictions.Eq("TransactionType", transactionType));

            ProjectionList projectionList = Projections.ProjectionList();
            projectionList.Add(Projections.Property("TransactionStart"), "TransactionStart");
            projectionList.Add(Projections.Property("DataHash"), "DataHash");

            criteria.SetResultTransformer(Transformers.DistinctRootEntity);
            criteria.SetProjection(projectionList).SetResultTransformer(Transformers.AliasToBean<TransactionLog>());

            IList<TransactionLog> transactions = this.persistenceManager.RetrieveAllEqual<TransactionLog>(criteria);

            if (transactions.Count > 0)
            {
                result = transactions.OrderByDescending<TransactionLog, DateTime>(x => (DateTime)x.TransactionStart).First().DataHash;
            }

            return result;
        }

        /// <summary>
        /// Gets the transaction information.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>
        /// The transaction information.
        /// </returns>
        public TransactionLog GetTransaction(int transactionId)
        {
            return this.persistenceManager.GetByKey<TransactionLog>(transactionId);
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
        /// Gets all the equipment types.
        /// </summary>
        /// <returns>The list of equipment types</returns>
        public IList<EquipmentType> GetEquipmentTypes()
        {
            return this.persistenceManager.RetrieveAll<EquipmentType>(SessionAction.BeginAndEnd);
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

        /// <summary>
        /// Gets the transaction type list.
        /// </summary>
        /// <param name="transactionDataId">The transaction data identifier.</param>
        /// <param name="transactionDirectionId">The transaction direction identifier.</param>
        /// <param name="transactionSourceId">The transaction source identifier.</param>
        /// <returns>
        /// The list of transactions that need to be run.
        /// </returns>
        public IList<TransactionType> GetTransactionTypes(int transactionDataId, int transactionDirectionId, int transactionSourceId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TransactionType>();
            criteria.Add(Restrictions.Eq("TransactionData", new TransactionData(transactionDataId)));
            criteria.Add(Restrictions.Eq("TransactionDirection", new TransactionDirection(transactionDirectionId)));
            criteria.Add(Restrictions.Eq("TransactionSource", new TransactionSource(transactionSourceId)));

            return this.persistenceManager.RetrieveAllEqual<TransactionType>(criteria);
        }

        /// <summary>
        /// Gets the transaction type list.
        /// </summary>
        /// <param name="transactionDataId">The transaction data identifier.</param>
        /// <param name="transactionDirectionId">The transaction direction identifier.</param>
        /// <param name="transactionSourceId">The transaction source identifier.</param>
        /// <param name="externalSystemName">Name of the external system.</param>
        /// <returns>
        /// The list of transactions that need to be run.
        /// </returns>
        public IList<TransactionType> GetTransactionTypes(int transactionDataId, int transactionDirectionId, int transactionSourceId, string externalSystemName)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TransactionType>();
            criteria.Add(Restrictions.Eq("TransactionData", new TransactionData(transactionDataId)));
            criteria.Add(Restrictions.Eq("TransactionDirection", new TransactionDirection(transactionDirectionId)));
            criteria.Add(Restrictions.Eq("TransactionSource", new TransactionSource(transactionSourceId)));
            criteria.CreateCriteria("ExternalSystem", JoinType.FullJoin)
                .Add(Restrictions.Eq("Name", externalSystemName));

            return this.persistenceManager.RetrieveAllEqual<TransactionType>(criteria);
        }

        /// <summary>
        /// Gets the device batch.
        /// </summary>
        /// <param name="batchNumber">The batch number.</param>
        /// <returns>
        /// The device batch
        /// </returns>
        public DeviceBatch GetDeviceBatchByBatchNumber(string batchNumber)
        {
            DetachedCriteria criteria = DetachedCriteria.For<DeviceBatch>();
            criteria.Add(Restrictions.Eq("BatchNumber", batchNumber));

            return this.persistenceManager.RetrieveFirstEqual<DeviceBatch>(criteria);
        }

        /// <summary>
        /// Gets the device batch.
        /// </summary>
        /// <param name="deviceBatchId">The device batch identifier.</param>
        /// <returns>
        /// The device batch
        /// </returns>
        public DeviceBatch GetDeviceBatch(int deviceBatchId)
        {
            return this.persistenceManager.GetByKey<DeviceBatch>(deviceBatchId);
        }

        /// <summary>
        /// Creates or updates the device batch.
        /// </summary>
        /// <param name="deviceBatch">The device batch object.</param>
        /// <returns>
        /// The device batch entity.
        /// </returns>
        public DeviceBatch SaveDeviceBatch(DeviceBatch deviceBatch)
        {
            this.persistenceManager.Save<DeviceBatch>(deviceBatch);

            return deviceBatch;
        }
    }
}

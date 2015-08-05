//-----------------------------------------------------------------------
// <copyright file="ServiceCore.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.EntityClient;
    using System.Data.Objects;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;
    using System.Threading;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Service.Contract;
    using log4net;

    /// <summary>
    /// Implements Listener web service
    /// </summary>
    public class ServiceCore : IService1
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCore"/> class.
        /// </summary>
        public ServiceCore()
        {
            this.TransactionLogManager = StaticPersistence.TransactionLogManager;
            this.DeviceManager = StaticPersistence.DeviceManager;
            this.WnpSystem = StaticPersistence.WnpSystem;
        }

        /// <summary>
        /// Gets or sets the transaction log manager.
        /// </summary>
        /// <value>
        /// The transaction log manager.
        /// </value>
        protected ITransactionManager TransactionLogManager { get; set; }

        /// <summary>
        /// Gets or sets the device manager.
        /// </summary>
        /// <value>
        /// The device manager.
        /// </value>
        protected IDeviceManager DeviceManager { get; set; }

        /// <summary>
        /// Gets or sets the WNP system.
        /// </summary>
        /// <value>
        /// The WNP system.
        /// </value>
        protected WNPSystem WnpSystem { get; set; }

        /// <summary>
        /// Receive device data.
        /// </summary>
        /// <param name="request">The device receive request.</param>
        /// <exception cref="System.ArgumentNullException">Equipment number must be specified.</exception>
        public void GetDevice(GetDeviceServiceRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("request", "Can not receive device if request is not specified");
                }

                this.TransactionLogManager.UpdateTransactionState(request.TransactionId, TransactionStateLookup.ServiceStart);

                Device device = this.DeviceManager.GetDevice(request.DeviceId);

                this.OnGetDevice(request, device);

                this.TransactionLogManager.UpdateTransactionState(request.TransactionId, TransactionStateLookup.ServiceEnd);
            }
            catch (Exception exception)
            {
                Log.Error("Get device operation  failed.", exception);
                throw;
            }
        }

        /// <summary>
        /// Sends the device information.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.ArgumentNullException">request;Can not send device data if request is not specified</exception>
        public void SendDevice(SendDataServiceRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("request", "Can not send device data if request is not specified");
                }

                this.TransactionLogManager.UpdateTransactionState(request.TransactionId, TransactionStateLookup.ServiceStart);

                Device device = this.DeviceManager.GetDevice(request.ObjectId);

                this.OnSendDeviceData(request.TransactionId, device);

                this.TransactionLogManager.UpdateTransactionState(request.TransactionId, TransactionStateLookup.ServiceEnd);
            }
            catch (Exception exception)
            {
                Log.Error("Send test data operation failed.", exception);
                throw;
            }
        }

        /// <summary>
        /// Send device test results.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.InvalidOperationException">Meter can not be found</exception>
        public void SendTestData(SendDataServiceRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("request", "Can not send device test data if request is not specified");
                }

                this.TransactionLogManager.UpdateTransactionState(request.TransactionId, TransactionStateLookup.ServiceStart);

                DeviceTest deviceTest = this.DeviceManager.GetDeviceTest(request.ObjectId);

                this.OnSendTestData(request.TransactionId, deviceTest);

                this.TransactionLogManager.UpdateTransactionState(request.TransactionId, TransactionStateLookup.ServiceEnd);
            }
            catch (Exception exception)
            {
                Log.Error("Send test data operation failed.", exception);
                throw;
            }
        }

        /// <summary>
        /// Sends the batch information.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.ArgumentNullException">request;Can not send batch data if request is not specified</exception>
        public void SendBatch(SendDataServiceRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("request", "Can not send batch data if request is not specified");
                }

                this.TransactionLogManager.UpdateTransactionState(request.TransactionId, TransactionStateLookup.ServiceStart);

                DeviceBatch deviceBatch = this.DeviceManager.GetDeviceBatch(request.ObjectId);

                this.OnSendBatchData(request.TransactionId, deviceBatch.BatchNumber);

                this.TransactionLogManager.UpdateTransactionState(request.TransactionId, TransactionStateLookup.ServiceEnd);
            }
            catch (Exception exception)
            {
                Log.Error("Send test data operation failed.", exception);
                throw;
            }
        }

        /// <summary>
        /// Called when [get device]. Must override with client specific implementation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="device">The device.</param>
        /// <exception cref="System.NotImplementedException">This transaction type is not available for your company.</exception>
        protected virtual void OnGetDevice(GetDeviceServiceRequest request, Device device)
        {
            throw new NotImplementedException("This transaction type is not available for your company.");
        }

        /// <summary>
        /// Called when [send test data]. Must override with client specific implementation.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <exception cref="System.NotImplementedException">This transaction type is not available for your company.</exception>
        protected virtual void OnSendTestData(int transactionId, DeviceTest deviceTest)
        {
            throw new NotImplementedException("This transaction type is not available for your company.");
        }

        /// <summary>
        /// Called when [send device data]. Must override with client specific implementation.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="device">The device.</param>
        /// <exception cref="System.NotImplementedException">This transaction type is not available for your company.</exception>
        protected virtual void OnSendDeviceData(int transactionId, Device device)
        {
            throw new NotImplementedException("This transaction type is not available for your company.");
        }

        /// <summary>
        /// Called when [send batch data]. Must override with client specific implementation.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="batchNumber">The batch number.</param>
        /// <exception cref="System.NotImplementedException">This transaction type is not available for your company.</exception>
        protected virtual void OnSendBatchData(int transactionId, string batchNumber)
        {
            throw new NotImplementedException("This transaction type is not available for your company.");
        }
    }
}

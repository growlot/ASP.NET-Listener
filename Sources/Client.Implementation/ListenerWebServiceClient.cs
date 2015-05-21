//-----------------------------------------------------------------------
// <copyright file="ListenerWebServiceClient.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using AMSLLC.Listener.Client.Implementation.ListenerService;
    using AMSLLC.Listener.Client.Implementation.Messages;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Globalization;
    using log4net;

    /// <summary>
    /// Implements listener web service calls
    /// </summary>
    public class ListenerWebServiceClient : IListenerWebServiceClient
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerWebServiceClient"/> class.
        /// </summary>
        public ListenerWebServiceClient()
        {
            using (IPersistenceManager persistenceManager = new PersistenceManager(ConfigurationManager.ConnectionStrings["ListenerDb"].ConnectionString))
            {
                using (IPersistenceManager clientPersistenceManager = new PersistenceManager(ConfigurationManager.ConnectionStrings["WnpDb"].ConnectionString))
                {
                    this.Initialize(persistenceManager, clientPersistenceManager);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerWebServiceClient" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        /// <param name="clientPersistenceManager">The client persistence manager.</param>
        public ListenerWebServiceClient(IPersistenceManager persistenceManager, IPersistenceManager clientPersistenceManager)
        {
            this.Initialize(persistenceManager, clientPersistenceManager);
        }

        /// <summary>
        /// Gets or sets the transaction manager
        /// </summary>
        protected ITransactionManager TransactionLogManager { get; set; }

        /// <summary>
        /// Gets or sets the device manager
        /// </summary>
        protected IDeviceManager DeviceManager { get; set; }

        /// <summary>
        /// Gets or sets the WNP system.
        /// </summary>
        /// <value>
        /// The WNP system.
        /// </value>
        protected WNPSystem WnpSystem { get; set; }

        /// <summary>
        /// Gets or sets the string manager.
        /// </summary>
        /// <value>
        /// The string manager.
        /// </value>
        protected ResourceManager StringManager { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        protected TransactionDataLookup DataType { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        protected Device Device { get; set; }

        /// <summary>
        /// Gets or sets the device test.
        /// </summary>
        /// <value>
        /// The device test.
        /// </value>
        protected DeviceTest DeviceTest { get; set; }

        /// <summary>
        /// Gets or sets the batch number.
        /// </summary>
        /// <value>
        /// The batch number.
        /// </value>
        protected DeviceBatch DeviceBatch { get; set; }

        /// <summary>
        /// Calls web service to retrieve device information.
        /// </summary>
        /// <param name="request">The device retrieve request message.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request;Can not retrieve device information when request is not specified.</exception>
        public virtual ClientResponse GetDeviceData(GetDeviceRequest request) // string equipmentType, string equipmentNumber, int owner, Uri listenerUrl)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not retrieve device information when request is not specified.");
            }

            ClientResponse response = new ClientResponse()
            {
                ReturnCode = 0,
            };

            IList<TransactionType> transactionTypes = this.TransactionLogManager.GetTransactionTypes(TransactionDataLookup.Device, TransactionDirectionLookup.Incoming, TransactionSourceLookup.WNP);

            // do nothing if no transactions are configured for this action.
            if (transactionTypes.Count == 0)
            {
                return response;
            }

            this.CreateDevice(request);
            
            foreach (TransactionType transactionType in transactionTypes)
            {
                TransactionLog transaction = new TransactionLog()
                {
                    TransactionType = transactionType,
                    Device = this.Device
                };

                int transactionId = this.TransactionLogManager.NewTransaction(transaction);
                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientStart);

                int returnCode = 0;
                string message = null;
                string debugInfo = null;
                
                GetDeviceServiceRequest serviceRequest = null;

                using (Service1Client client = new Service1Client("BasicHttpBinding_IService1", request.ListenerUrl.ToString()))
                {
                    try
                    {
                        this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientSendMessage);
                        serviceRequest = new GetDeviceServiceRequest()
                        {
                            TransactionId = transactionId,
                            DeviceId = this.Device.Id,
                            Location = request.Location,
                            TesterId = request.TesterId,
                            TestStandard = request.TestStandard
                        };
                        client.GetDevice(serviceRequest);
                    }
                    catch (FaultException<ServiceFaultDetails> ex)
                    {
                        Log.Error("Customer service call returned error.", ex);
                        Log.Error(ex.Detail.DebugInfo);
                        returnCode = -1;
                        message = this.StringManager.GetString("ServiceSOAPFault", CultureInfo.CurrentCulture) + ex.Detail.Message;
                        debugInfo = ex.Detail.DebugInfo;
                    }
                    catch (FaultException ex)
                    {
                        Log.Error("Service call returned error.", ex);
                        returnCode = -1;
                        message = this.StringManager.GetString("ServiceSOAPFault", CultureInfo.CurrentCulture) + ex.Message;
                        debugInfo = ex.ToString();
                    }
                    catch (CommunicationException ex)
                    {
                        Log.Error("Service call failed.", ex);
                        returnCode = -1;
                        message = this.StringManager.GetString("ServiceCallFailed", CultureInfo.CurrentCulture);
                        debugInfo = ex.ToString();
                    }
                    catch (TimeoutException ex)
                    {
                        Log.Error("Service call timed out.", ex);
                        returnCode = -1;
                        message = this.StringManager.GetString("ServiceCallTimeout", CultureInfo.CurrentCulture);
                        debugInfo = ex.ToString();
                    }
                }

                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientEnd);
                
                // reload transaction info from db
                transaction = this.TransactionLogManager.GetTransaction(transactionId);

                if (transaction.TransactionStatus.Id == (int)TransactionStatusLookup.InProgress && transaction.TransactionType.TransactionCompletion.Id == (int)TransactionCompletionLookup.Default)
                {
                    this.TransactionLogManager.UpdateTransactionStatus(transactionId, returnCode, message, debugInfo);
                }

                if (returnCode != 0)
                {
                    response.ReturnCode = returnCode;
                    response.Message += message;
                    response.DebugInfo += debugInfo;

                    this.TransactionLogManager.UpdateTransactionStatus(transactionId, returnCode, message, debugInfo);
                }
            }

            return response;
        }

        /// <summary>
        /// Call web service to publish device test results
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        /// <exception cref="ArgumentNullException">request;Can not send device test information when request is not specified.</exception>
        public virtual ClientResponse SendDeviceTestData(DeviceTestRequest request)
        {
            this.DataType = TransactionDataLookup.DeviceTest;
            this.CreateDeviceTest(request);
            return this.ProcessRequest(request);
        }

        /// <summary>
        /// Call web service to publish device information
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        /// <exception cref="ArgumentNullException">request;Can not send device test information when request is not specified.</exception>
        public virtual ClientResponse SendDeviceData(DeviceRequest request)
        {
            this.DataType = TransactionDataLookup.Device;
            this.CreateDevice(request);
            return this.ProcessRequest(request);
        }

        /// <summary>
        /// Call web service to publish batch information
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        /// <exception cref="ArgumentNullException">request;Can not send device test information when request is not specified.</exception>
        public virtual ClientResponse SendBatchData(BatchRequest request)
        {
            this.DataType = TransactionDataLookup.NewBatch;
            this.CreateDeviceBatch(request);

            return this.ProcessRequest(request);
        }

        /// <summary>
        /// Creates the device object.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.ArgumentNullException">request;Can not create device record if request is not specified.</exception>
        protected void CreateDevice(DeviceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not create device record if request is not specified.");
            } 

            EquipmentType equipmentType = this.DeviceManager.GetEquipmentTypeByInternalCode(request.ServiceType, request.EquipmentType);
            Company company = this.DeviceManager.GetCompanyByInternalCode(request.CompanyId.ToString(CultureInfo.InvariantCulture));
            this.Device = new Device
            {
                Company = company,
                EquipmentNumber = request.EquipmentNumber,
                EquipmentType = equipmentType
            };

            this.Device = this.DeviceManager.GetOrCreateDevice(this.Device);
        }

        /// <summary>
        /// Creates the device test.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.ArgumentNullException">request;Can not create device test record if request is not specified.</exception>
        protected void CreateDeviceTest(DeviceTestRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not create device test record if request is not specified.");
            }

            this.CreateDevice(request);

            DeviceTest deviceTest = new DeviceTest
            {
                Device = this.Device,
                TestDate = request.TestDate
            };
            this.DeviceTest = this.DeviceManager.GetOrCreateDeviceTest(deviceTest);
        }

        /// <summary>
        /// Creates the device batch.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.ArgumentNullException">request;Can not create device batch record if request is not specified.</exception>
        private void CreateDeviceBatch(BatchRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not create device batch record if request is not specified.");
            }

            this.DeviceBatch = this.DeviceManager.GetDeviceBatchByBatchNumber(request.BatchNumber);

            if (this.DeviceBatch == null)
            {
                DeviceBatch deviceBatch = new DeviceBatch
                {
                    BatchNumber = request.BatchNumber
                };

                this.DeviceBatch = this.DeviceManager.SaveDeviceBatch(deviceBatch);
            }
        }        

        /// <summary>
        /// Initializes the <see cref="ListenerWebServiceClient" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        /// <param name="clientPersistenceManager">The client persistence manager.</param>
        private void Initialize(IPersistenceManager persistenceManager, IPersistenceManager clientPersistenceManager)
        {
            IPersistenceController persistenceController = new PersistenceController();
            IWNPPersistenceController wnpPersistenceController = new WNPPersistenceController();
            persistenceController.InitializeListenerSystems(persistenceManager);
            wnpPersistenceController.InitializeListenerClientSystems(clientPersistenceManager);
            this.TransactionLogManager = new TransactionManager(persistenceController);
            this.DeviceManager = new DeviceManager(persistenceController);
            this.StringManager = Init.StringManager;
            this.WnpSystem = wnpPersistenceController.WNPSystem;
        }
        
        /// <summary>
        /// Call web service to publish device test results
        /// </summary>
        /// <typeparam name="T">The type of request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request;Can not process request if it is not specified.</exception>
        /// <exception cref="System.InvalidOperationException">Transaction data type is unclear</exception>
        private ClientResponse ProcessRequest<T>(T request) where T : IListenerFields
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not process request if it is not specified.");
            }

            ClientResponse response = new ClientResponse()
            {
                ReturnCode = 0,
            };

            IList<TransactionType> transactionTypes = this.TransactionLogManager.GetTransactionTypes(this.DataType, TransactionDirectionLookup.Outgoing, TransactionSourceLookup.WNP);

            // do nothing if no transactions are configured for this action.
            if (transactionTypes.Count == 0)
            {
                return response;
            }

            foreach (TransactionType transactionType in transactionTypes)
            {
                int transactionId;

                TransactionLog transaction = new TransactionLog();
                transaction.TransactionType = transactionType;

                switch (this.DataType)
                {
                    case TransactionDataLookup.Device:
                        transaction.Device = this.Device;
                        transactionId = this.TransactionLogManager.NewTransaction(transaction);
                        break;
                    case TransactionDataLookup.DeviceTest:
                        transaction.Device = this.Device;
                        transaction.DeviceTest = this.DeviceTest;
                        transactionId = this.TransactionLogManager.NewTransaction(transaction);
                        break;
                    case TransactionDataLookup.NewBatch:
                        transaction.DeviceBatch = this.DeviceBatch;
                        transactionId = this.TransactionLogManager.NewTransaction(transaction);
                        break;
                    default:
                        throw new InvalidOperationException("Transaction data type is unclear");
                }

                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientStart);

                int returnCode = 0;
                string message = null;
                string debugInfo = null;

                using (Service1Client client = new Service1Client("BasicHttpBinding_IService1", request.ListenerUrl.ToString()))
                {
                    try
                    {
                        this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientSendMessage);

                        switch (this.DataType)
                        {
                            case TransactionDataLookup.Device:
                                SendDataServiceRequest deviceRequest = new SendDataServiceRequest()
                                {
                                    TransactionId = transactionId,
                                    ObjectId = this.Device.Id,
                                };
                                client.SendDevice(deviceRequest);
                                break;
                            case TransactionDataLookup.DeviceTest:
                                SendDataServiceRequest deviceTestRequest = new SendDataServiceRequest()
                                {
                                    TransactionId = transactionId,
                                    ObjectId = this.DeviceTest.Id
                                };
                                client.SendTestData(deviceTestRequest);
                                break;
                            case TransactionDataLookup.NewBatch:
                                SendDataServiceRequest newBatchRequest = new SendDataServiceRequest()
                                {
                                    TransactionId = transactionId,
                                    ObjectId = this.DeviceBatch.Id
                                };
                                client.SendBatch(newBatchRequest);
                                break;
                            default:
                                throw new InvalidOperationException("Transaction data type is unclear");
                        }
                    }
                    catch (FaultException<ServiceFaultDetails> ex)
                    {
                        Log.Error("Customer service call returned error.", ex);
                        Log.Error(ex.Detail.DebugInfo);
                        returnCode = -1;
                        message = this.StringManager.GetString("ServiceSOAPFault", CultureInfo.CurrentCulture) + ex.Detail.Message;
                        debugInfo = ex.Detail.DebugInfo;
                    }
                    catch (FaultException<ExceptionDetail> ex)
                    {
                        Log.Error("Service call returned error.", ex);
                        Log.Error(ex.Detail);
                        returnCode = -1;
                        message = this.StringManager.GetString("ServiceSOAPFault", CultureInfo.CurrentCulture) + ex.Detail.Message;
                        debugInfo = ex.Detail.ToString();
                    }
                    catch (FaultException ex)
                    {
                        Log.Error("Service call returned error.", ex);
                        returnCode = -1;
                        message = this.StringManager.GetString("ServiceSOAPFault", CultureInfo.CurrentCulture) + ex.Message;
                        debugInfo = ex.ToString();
                    }
                    catch (CommunicationException ex)
                    {
                        Log.Error("Service call failed.", ex);
                        returnCode = -1;
                        message = this.StringManager.GetString("ServiceCallFailed", CultureInfo.CurrentCulture);
                        debugInfo = ex.ToString();
                    }
                    catch (TimeoutException ex)
                    {
                        Log.Error("Service call timed out.", ex);
                        returnCode = -1;
                        message = this.StringManager.GetString("ServiceCallTimeout", CultureInfo.CurrentCulture);
                        debugInfo = ex.ToString();
                    }
                }

                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientEnd);

                // reload transaction info from db
                transaction = this.TransactionLogManager.GetTransaction(transactionId);

                if (transaction.TransactionStatus.Id == (int)TransactionStatusLookup.InProgress && transaction.TransactionType.TransactionCompletion.Id == (int)TransactionCompletionLookup.Default)
                {
                    this.TransactionLogManager.UpdateTransactionStatus(transactionId, returnCode, message, debugInfo);
                }

                if (returnCode != 0)
                {
                    response.ReturnCode = returnCode;
                    response.Message += message;
                    response.DebugInfo += debugInfo;

                    this.TransactionLogManager.UpdateTransactionStatus(transactionId, returnCode, message, debugInfo);
                }
            }

            return response;
        }
    }
}

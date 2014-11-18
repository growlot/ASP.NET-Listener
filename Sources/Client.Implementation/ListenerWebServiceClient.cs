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
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    using AMSLLC.Listener.Client.Implementation.ListenerService;
    using AMSLLC.Listener.Client.Implementation.Messages;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Globalization;
    using log4net;
    using System.IO;
    using System.Xml.Serialization;

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
                this.Initialize(persistenceManager);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerWebServiceClient" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        public ListenerWebServiceClient(IPersistenceManager persistenceManager)
        {
            this.Initialize(persistenceManager);
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
        /// Gets or sets the string manager.
        /// </summary>
        /// <value>
        /// The string manager.
        /// </value>
        protected ResourceManager StringManager { get; set; }

        /// <summary>
        /// Calls web service to retrieve device information.
        /// </summary>
        /// <param name="request">The device retrieve request message.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request;Can not retrieve device information when request is not specified.</exception>
        public virtual ClientResponse GetDevice(GetDeviceRequest request) // string equipmentType, string equipmentNumber, int owner, Uri listenerUrl)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not retrieve device information when request is not specified.");
            }

            ClientResponse response = new ClientResponse()
            {
                ReturnCode = 0,
                Message = this.StringManager.GetString("DeviceReceivedSuccess", CultureInfo.CurrentCulture)
            };

            Device device = this.CreateDevice(request);
            int deviceId = this.DeviceManager.GetOrCreateDevice(device).Id;

            int transactionId = this.TransactionLogManager.NewTransaction(TransactionTypeLookup.GetDevice, deviceId, null, null, TransactionSourceLookup.WNP);
            this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientStart);

            GetDeviceServiceRequest serviceRequest = null;

            using (Service1Client client = new Service1Client("BasicHttpBinding_IService1", request.ListenerUrl.ToString()))
            {
                try
                {
                    this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientSendMessage);
                    serviceRequest = new GetDeviceServiceRequest()
                    {
                        TransactionId = transactionId,
                        DeviceId = deviceId,
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
                    response.ReturnCode = -1;
                    response.Message = this.StringManager.GetString("ServiceSOAPFault", CultureInfo.CurrentCulture) + ex.Detail.Message;
                    response.DebugInfo = ex.Detail.DebugInfo;
                }
                catch (FaultException ex)
                {
                    Log.Error("Service call returned error.", ex);
                    response.ReturnCode = -1;
                    response.Message = this.StringManager.GetString("ServiceSOAPFault", CultureInfo.CurrentCulture) + ex.Message;
                    response.DebugInfo = ex.ToString();
                }
                catch (CommunicationException ex)
                {
                    Log.Error("Service call failed.", ex);
                    response.ReturnCode = -1;
                    response.Message = this.StringManager.GetString("ServiceCallFailed", CultureInfo.CurrentCulture);
                    response.DebugInfo = ex.ToString();
                }
                catch (TimeoutException ex)
                {
                    Log.Error("Service call timed out.", ex);
                    response.ReturnCode = -1;
                    response.Message = this.StringManager.GetString("ServiceCallTimeout", CultureInfo.CurrentCulture);
                    response.DebugInfo = ex.ToString();
                }
            }

            this.FinishTransaction(response, transactionId);

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
        public virtual ClientResponse SendDeviceTest(SendDeviceTestRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not send device test information when request is not specified.");
            }

            ClientResponse response = new ClientResponse()
            {
                ReturnCode = 0,
                Message = this.StringManager.GetString("DeviceShopTestSuccess", CultureInfo.CurrentCulture)
            };

            Device device = this.CreateDevice(request);
            device = this.DeviceManager.GetOrCreateDevice(device);

            DeviceTest deviceTest = new DeviceTest
            {
                Device = device,
                TestDate = request.TestDate
            };
            int deviceTestId = this.DeviceManager.GetOrCreateDeviceTest(deviceTest).Id;

            int transactionId = this.TransactionLogManager.NewTransaction(TransactionTypeLookup.SendTestData, device.Id, deviceTestId, null, TransactionSourceLookup.WNP);
            this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientStart);

            using (Service1Client client = new Service1Client("BasicHttpBinding_IService1", request.ListenerUrl.ToString()))
            {
                try
                {
                    this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientSendMessage);

                    SendTestDataServiceRequest serviceRequest = new SendTestDataServiceRequest()
                    {
                        TransactionId = transactionId,
                        DeviceId = device.Id,
                        DeviceTestId = deviceTestId
                    };
                    client.SendTestData(serviceRequest);
                }
                catch (FaultException<ServiceFaultDetails> ex)
                {
                    Log.Error("Customer service call returned error.", ex);
                    Log.Error(ex.Detail.DebugInfo);
                    response.ReturnCode = -1;
                    response.Message = this.StringManager.GetString("ServiceSOAPFault", CultureInfo.CurrentCulture) + ex.Detail.Message;
                    response.DebugInfo = ex.Detail.DebugInfo;
                }
                catch (FaultException ex)
                {
                    Log.Error("Service call returned error.", ex);
                    response.ReturnCode = -1;
                    response.Message = this.StringManager.GetString("ServiceSOAPFault", CultureInfo.CurrentCulture) + ex.Message;
                    response.DebugInfo = ex.ToString();
                }
                catch (CommunicationException ex)
                {
                    Log.Error("Service call failed.", ex);
                    response.ReturnCode = -1;
                    response.Message = this.StringManager.GetString("ServiceCallFailed", CultureInfo.CurrentCulture);
                    response.DebugInfo = ex.ToString();
                }
                catch (TimeoutException ex)
                {
                    Log.Error("Service call timed out.", ex);
                    response.ReturnCode = -1;
                    response.Message = this.StringManager.GetString("ServiceCallTimeout", CultureInfo.CurrentCulture);
                    response.DebugInfo = ex.ToString();
                }
            }

            this.FinishTransaction(response, transactionId);

            return response;
        }

        /// <summary>
        /// Finishes the transaction by setting transaction status and last transaction state.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        protected void FinishTransaction(ClientResponse response, int transactionId)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response", "Can not finish transaction if response is not provided.");
            }

            if (response.ReturnCode != 0)
            {
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Failed, response.Message.Substring(0, 1000), response.DebugInfo);
            }
            else
            {
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Succeeded);
            }

            this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientEnd);
        }

        /// <summary>
        /// Creates the device object from device retrieve request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The device object
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request;Can not create device record if request is not specified.</exception>
        protected Device CreateDevice(GetDeviceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not create device record if request is not specified.");
            } 

            EquipmentType equipmentType = this.DeviceManager.GetEquipmentTypeByInternalCode(request.ServiceType, request.EquipmentType);
            Company company = this.DeviceManager.GetCompanyByInternalCode(request.CompanyId.ToString(CultureInfo.InvariantCulture));
            Device device = new Device
            {
                Company = company,
                EquipmentNumber = request.EquipmentNumber,
                EquipmentType = equipmentType
            };
            return device;
        }

        /// <summary>
        /// Creates the device object from device shop test request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The device object
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request;Can not create device record if request is not specified.</exception>
        protected Device CreateDevice(SendDeviceTestRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not create device record if request is not specified.");
            } 

            EquipmentType equipmentType = this.DeviceManager.GetEquipmentTypeByInternalCode(request.ServiceType, request.EquipmentType);
            Company company = this.DeviceManager.GetCompanyByInternalCode(request.CompanyId.ToString(CultureInfo.InvariantCulture));
            Device device = new Device
            {
                Company = company,
                EquipmentNumber = request.EquipmentNumber,
                EquipmentType = equipmentType
            };
            return device;
        }

        /// <summary>
        /// Initializes the <see cref="ListenerWebServiceClient" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        private void Initialize(IPersistenceManager persistenceManager)
        {
            IPersistenceController persistenceController = new PersistenceController();
            persistenceController.InitializeListenerSystems(persistenceManager);
            this.TransactionLogManager = new TransactionManager(persistenceController);
            this.DeviceManager = new DeviceManager(persistenceController);
            this.StringManager = Init.StringManager;
        }
    }
}

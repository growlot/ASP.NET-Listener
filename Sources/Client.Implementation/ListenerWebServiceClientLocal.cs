//-----------------------------------------------------------------------
// <copyright file="ListenerWebServiceClientLocal.cs" company="Advanced Metering Services LLC">
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
    using AMSLLC.Listener.Client.Implementation.Messages;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP;
    using AMSLLC.Listener.Common.WNP.Model;
    using log4net;

    /// <summary>
    /// Implements listener web service calls
    /// </summary>
    public class ListenerWebServiceClientLocal : ListenerWebServiceClient, IListenerWebServiceClient
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerWebServiceClientLocal"/> class.
        /// </summary>
        public ListenerWebServiceClientLocal()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerWebServiceClientLocal" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        /// <param name="clientPersistenceManager">The client persistence manager.</param>
        public ListenerWebServiceClientLocal(IPersistenceManager persistenceManager, IPersistenceManager clientPersistenceManager)
            : base(persistenceManager, clientPersistenceManager)
        {
        }

        /// <summary>
        /// Calls web service to retrieve device information.
        /// </summary>
        /// <param name="request">The device retrieve request message.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request;Can not retrieve device information when request is not specified.</exception>
        public override ClientResponse GetDeviceData(GetDeviceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not retrieve device information when request is not specified.");
            }

            Log.Info("Creating dummy DeviceReceive transaction.");
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
                
                switch (request.EquipmentType)
                {
                    case "EM":
                        Meter meter = CreateDummyEquipmentMeter(request);
                        this.WnpSystem.AddOrReplaceEquipment(meter);
                        Log.Info("Adding dummy electric meter device.");
                        break;
                    case "CT":
                        CurrentTransformer ct = CreateDummyEquipmentCurrentTransformer(request);
                        this.WnpSystem.AddOrReplaceEquipment(ct);
                        Log.Info("Adding dummy current transformer device.");
                        break;
                    case "PT":
                        PotentialTransformer pt = CreateDummyEquipmentPotentialTransformer(request);
                        this.WnpSystem.AddOrReplaceEquipment(pt);
                        Log.Info("Adding dummy potential transformer device.");
                        break;
                }

                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientEnd);
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, response.ReturnCode, response.Message, response.DebugInfo);
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
        public override ClientResponse SendDeviceTestData(DeviceTestRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not send device test information when request is not specified.");
            }

            Log.Info("Creating dummy DeviceShopTest transaction.");
            ClientResponse response = new ClientResponse()
            {
                ReturnCode = 0,
            };

            IList<TransactionType> transactionTypes = this.TransactionLogManager.GetTransactionTypes(TransactionDataLookup.DeviceTest, TransactionDirectionLookup.Outgoing, TransactionSourceLookup.WNP);

            // do nothing if no transactions are configured for this action.
            if (transactionTypes.Count == 0)
            {
                return response;
            }

            this.CreateDeviceTest(request);

            foreach (TransactionType transactionType in transactionTypes)
            {
                TransactionLog transaction = new TransactionLog()
                {
                    TransactionType = transactionType,
                    Device = this.Device,
                    DeviceTest = this.DeviceTest
                };
                int transactionId = this.TransactionLogManager.NewTransaction(transaction);
                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientStart);
                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientEnd);
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, response.ReturnCode, response.Message, response.DebugInfo);
            }

            return response;
        }

        /// <summary>
        /// Call web service to publish device results
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        /// <exception cref="ArgumentNullException">request;Can not send device information when request is not specified.</exception>
        public override ClientResponse SendDeviceData(DeviceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not send device test information when request is not specified.");
            }

            Log.Info("Creating dummy DevicePush transaction.");
            ClientResponse response = new ClientResponse()
            {
                ReturnCode = 0,
            };

            IList<TransactionType> transactionTypes = this.TransactionLogManager.GetTransactionTypes(TransactionDataLookup.Device, TransactionDirectionLookup.Outgoing, TransactionSourceLookup.WNP);

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
                    Device = this.Device,
                    DeviceTest = this.DeviceTest
                };
                int transactionId = this.TransactionLogManager.NewTransaction(transaction);
                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientStart);
                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientEnd);
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, response.ReturnCode, response.Message, response.DebugInfo);
            }

            return response;
        }

        /// <summary>
        /// Creates the dummy equipment meter.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The equipment meter
        /// </returns>
        /// <exception cref="ArgumentNullException">request;Can not transform data when initial data is not specified</exception>
        private static Meter CreateDummyEquipmentMeter(GetDeviceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not transform data when initial data is not specified");
            }

            Meter result = new Meter
            {
                EquipmentNumber = request.EquipmentNumber,
                Owner = new Owner(request.CompanyId),
                FirmwareRevision1 = "1.1.1.1",
                FirmwareRevision2 = "2",
                MeterCode = "C02",
                CustomField1 = "E", // service type
                CustomField2 = "MR", // equipment type
                CustomField3 = "N", // new device
                CustomField4 = DateTime.Now.AddDays(-1).ToString(CultureInfo.InvariantCulture), // last test date
                CustomField5 = "N", // loss compensation
                CustomField6 = "00" // reason for test
            };
            return result;
        }

        /// <summary>
        /// Creates the dummy current transformer.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The current transformer
        /// </returns>
        /// <exception cref="ArgumentNullException">request;Can not transform data when initial data is not specified</exception>
        private static CurrentTransformer CreateDummyEquipmentCurrentTransformer(GetDeviceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not transform data when initial data is not specified");
            }

            CurrentTransformer result = new CurrentTransformer
            {
                EquipmentNumber = request.EquipmentNumber,
                Owner = new Owner(request.CompanyId),
                TransformerCode = "C01A",
                CustomField1 = "E", // service type
                CustomField2 = "CT", // equipment type
                CustomField3 = "N", // new device
                CustomField4 = DateTime.Now.AddDays(-1).ToString(CultureInfo.InvariantCulture), // last test date
                CustomField5 = "00" // reason for test
            };
            return result;
        }

        /// <summary>
        /// Creates the dummy potential transformer.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The potential transformer
        /// </returns>
        /// <exception cref="ArgumentNullException">request;Can not transform data when initial data is not specified</exception>
        private static PotentialTransformer CreateDummyEquipmentPotentialTransformer(GetDeviceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not transform data when initial data is not specified");
            }

            PotentialTransformer result = new PotentialTransformer
            {
                EquipmentNumber = request.EquipmentNumber,
                Owner = new Owner(request.CompanyId),
                TransformerCode = "V01A",
                CustomField1 = "E", // service type
                CustomField2 = "PT", // equipment type
                CustomField3 = "N", // new device
                CustomField4 = DateTime.Now.AddDays(-1).ToString(CultureInfo.InvariantCulture), // last test date
                CustomField5 = "00" // reason for test
            };
            return result;
        }
    }
}

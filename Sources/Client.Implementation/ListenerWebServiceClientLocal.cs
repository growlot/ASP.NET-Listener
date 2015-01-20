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
    using AMSLLC.Listener.Client.Implementation.AlliantExternalService;
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
        /// The WNP system
        /// </summary>
        private WNPSystem wnpSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerWebServiceClientLocal"/> class.
        /// </summary>
        public ListenerWebServiceClientLocal()
            : base()
        {
            using (IPersistenceManager clientPersistenceManager = new PersistenceManager(ConfigurationManager.ConnectionStrings["WnpDb"].ConnectionString))
            {
                this.Initialize(clientPersistenceManager);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerWebServiceClientLocal" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        public ListenerWebServiceClientLocal(IPersistenceManager persistenceManager)
            : base(persistenceManager)
        {
            using (IPersistenceManager clientPersistenceManager = new PersistenceManager(ConfigurationManager.ConnectionStrings["WnpDb"].ConnectionString))
            {
                this.Initialize(clientPersistenceManager);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerWebServiceClientLocal" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        /// <param name="clientPersistenceManager">The client persistence manager.</param>
        public ListenerWebServiceClientLocal(IPersistenceManager persistenceManager, IPersistenceManager clientPersistenceManager)
            : base(persistenceManager)
        {
            this.Initialize(clientPersistenceManager);
        }

        /// <summary>
        /// Calls web service to retrieve device information.
        /// </summary>
        /// <param name="request">The device retrieve request message.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">request;Can not retrieve device information when request is not specified.</exception>
        public override ClientResponse GetDevice(GetDeviceRequest request)
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

            Device device = this.CreateDevice(request);
            int deviceId = this.DeviceManager.GetOrCreateDevice(device).Id;

            foreach (TransactionType transactionType in transactionTypes)
            {
                int transactionId = this.TransactionLogManager.NewTransaction(transactionType.Id, deviceId, null, null);
                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientStart);
                
                switch (request.EquipmentType)
                {
                    case "EM":
                        Meter meter = CreateDummyEquipmentMeter(request);
                        this.wnpSystem.AddOrReplaceEquipment(meter);
                        Log.Info("Adding dummy electric meter device.");
                        break;
                    case "CT":
                        CurrentTransformer ct = CreateDummyEquipmentCurrentTransformer(request);
                        this.wnpSystem.AddOrReplaceEquipment(ct);
                        Log.Info("Adding dummy current transformer device.");
                        break;
                    case "PT":
                        PotentialTransformer pt = CreateDummyEquipmentPotentialTransformer(request);
                        this.wnpSystem.AddOrReplaceEquipment(pt);
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
        public override ClientResponse SendDeviceTest(SendDeviceTestRequest request)
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

            Device device = this.CreateDevice(request);
            device = this.DeviceManager.GetOrCreateDevice(device);

            DeviceTest deviceTest = new DeviceTest
            {
                Device = device,
                TestDate = request.TestDate
            };
            int deviceTestId = this.DeviceManager.GetOrCreateDeviceTest(deviceTest).Id;

            foreach (TransactionType transactionType in transactionTypes)
            {
                int transactionId = this.TransactionLogManager.NewTransaction(transactionType.Id, device.Id, deviceTestId, null);
                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientStart);
                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ClientEnd);
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, response.ReturnCode, response.Message, response.DebugInfo);
            }

            return response;
        }

        /// <summary>
        /// Sends the barcodes to web service.
        /// </summary>
        /// <returns>Response detailing if call succeeded.</returns>
        public ClientResponse SendBarcodes()
        {
            ClientResponse response = new ClientResponse()
            {
                ReturnCode = 0,
                Message = this.StringManager.GetString("DeviceShopTestSuccess", CultureInfo.CurrentCulture)
            };

            UpdateDeviceClassificationCodeABMType updateDeviceCode = new UpdateDeviceClassificationCodeABMType()
            {
                BatchesTotal = 1,
                BatchNumber = 1,
                BatchesTotalSpecified = true,
                BatchNumberSpecified = true,
                UpdateDeviceClassificationCode = new DeviceClassificationCodeType[] { }
            };

            List<DeviceClassificationCodeType> classificationCodes = new List<DeviceClassificationCodeType>();

            List<MeterBarcode> meterBarcodes = Utilities.ReadFromXmlFile<List<MeterBarcode>>(@".\meterBarcodes.xml");
            foreach (MeterBarcode meterBarcode in meterBarcodes)
            {
                DeviceClassificationCodeType classificationCode = CreateClassificationCode(meterBarcode);
                classificationCodes.Add(classificationCode);
            }

            updateDeviceCode.UpdateDeviceClassificationCode = classificationCodes.ToArray<DeviceClassificationCodeType>();

            using (AlliantExternalService.DeviceClassificationCodeClient client = new AlliantExternalService.DeviceClassificationCodeClient("BasicHttpBinding_DeviceClassificationCode"))
            {
                try
                {
                    client.Update(updateDeviceCode);
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

            return response;
        }

        /// <summary>
        /// Exports the barcodes from database to file.
        /// </summary>
        public void ExportBarcodes()
        {
            List<MeterBarcode> meterBarcodes = this.wnpSystem.GetBarcodes<MeterBarcode>() as List<MeterBarcode>;
            Utilities.WriteToXmlFile<List<MeterBarcode>>(@".\meterBarcodes.xml", meterBarcodes);
        }

        /// <summary>
        /// Creates the classification code.
        /// </summary>
        /// <param name="meterBarcode">The meter barcode.</param>
        /// <returns>The classification code.</returns>
        private static DeviceClassificationCodeType CreateClassificationCode(MeterBarcode meterBarcode)
        {
            DeviceClassificationCodeType response = new DeviceClassificationCodeType()
            {
                AssetProfileID = meterBarcode.CustomField6,
                ClassificationCode = meterBarcode.LookupCode,
                DeviceDescription = meterBarcode.Description,
                DeviceTestType = meterBarcode.CustomField7,
                DeviceType = "MR",
                ElectricDevice = new DeviceClassificationCodeTypeElectricDevice()
                {
                    AMIIndicator = meterBarcode.CustomField13,
                    Base = meterBarcode.Base.ToString(),
                    ERTIndicator = meterBarcode.CustomField14,
                    Form = meterBarcode.Form,
                    LossCompensationCapableIndicator = meterBarcode.CustomField22,
                    NetworkIndicator = meterBarcode.CustomField17,
                    Phase = meterBarcode.Phase.ToString(CultureInfo.InvariantCulture),
                    RecorderExists = meterBarcode.CustomField21,
                    RegisterRatio = meterBarcode.RegisterRatio,
                    RemoteConnectDisconnectIndicator = meterBarcode.CustomField20,
                    TestAmps = (decimal)meterBarcode.Amp,
                    TestAmpsSpecified = true,
                    TestSequence = meterBarcode.CustomField15,
                    TestVoltage = (int)meterBarcode.Volt,
                    TestVoltageSpecified = true,
                    TransformerRatedIndicator = meterBarcode.CustomField16,
                    VoltageClass = meterBarcode.CustomField12,
                    Wire = meterBarcode.Wire,
                    WireSpecified = true
                },
                ForceRetirementSwitch = meterBarcode.CustomField4,
                Manufacturer = meterBarcode.CustomField2,
                MaterialID = meterBarcode.CustomField5,
                Model = meterBarcode.CustomField3,
                Status = meterBarcode.CustomField1,
                TemplateDevice = meterBarcode.CustomField8,
            };

            int temp;
            decimal tempDecimal;
            if (int.TryParse(meterBarcode.CustomField11, out temp))
            {
                response.ElectricDevice.Ampacity = temp;
                response.ElectricDevice.AmpacitySpecified = true;
            }

            if (int.TryParse(meterBarcode.CustomField9, out temp))
            {
                response.ElectricDevice.BatteryLife = temp;
                response.ElectricDevice.BatteryLifeSpecified = true;
            }

            if (decimal.TryParse(meterBarcode.KH, NumberStyles.Float, CultureInfo.InvariantCulture, out tempDecimal))
            {
                response.ElectricDevice.Constant = tempDecimal;
                response.ElectricDevice.ConstantSpecified = true;
            }

            if (meterBarcode.Owner.Id == 0)
            {
                response.ElectricDevice.IPLSelectionType = meterBarcode.CustomField18;
                if (int.TryParse(meterBarcode.CustomField19, out temp))
                {
                    response.ElectricDevice.IPLTestInterval = temp;
                    response.ElectricDevice.IPLTestIntervalSpecified = true;
                }
            }

            if (meterBarcode.Owner.Id == 1)
            {
                response.ElectricDevice.WPLSelectionType = meterBarcode.CustomField18;
                if (int.TryParse(meterBarcode.CustomField19, out temp))
                {
                    response.ElectricDevice.WPLTestInterval = temp;
                    response.ElectricDevice.WPLTestIntervalSpecified = true;
                }
            }

            if (int.TryParse(meterBarcode.CustomField10, out temp))
            {
                response.ElectricDevice.Stator = temp;
                response.ElectricDevice.StatorSpecified = true;
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

        /// <summary>
        /// Initializes the <see cref="ListenerWebServiceClientLocal" /> class.
        /// </summary>
        /// <param name="clientPersistenceManager">The client persistence manager.</param>
        private void Initialize(IPersistenceManager clientPersistenceManager)
        {
            IWNPPersistenceController persistenceController = new WNPPersistenceController();
            persistenceController.InitializeListenerClientSystems(clientPersistenceManager);
            this.wnpSystem = persistenceController.WNPSystem;
        }
    }
}

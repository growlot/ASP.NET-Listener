//-----------------------------------------------------------------------
// <copyright file="CustomService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.Alliant
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.ServiceModel;
    using System.Xml.Serialization;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Globalization;
    using AMSLLC.Listener.Service.Contract;
    using AMSLLC.Listener.Service.Implementation;
    using AMSLLC.Listener.Service.Implementation.Alliant.GetDevice;
    using AMSLLC.Listener.Service.Implementation.Alliant.SendTestResult;
    using log4net;

    /// <summary>
    /// Alliant customer specific service implementation.
    /// </summary>
    public class CustomService : ServiceCore
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The string manager
        /// </summary>
        private ResourceManager stringManager = Init.StringManager;

        /// <summary>
        /// Called when [get device].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="device">The device.</param>
        /// <exception cref="System.ArgumentNullException">
        /// request;Can not get device information if request is not specified.
        /// or
        /// request;Can not get device information if device is not specified.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        protected override void OnGetDevice(GetDeviceServiceRequest request, Device device)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not get device information if request is not specified.");
            }

            if (device == null)
            {
                throw new ArgumentNullException("device", "Can not get device information if device is not specified.");
            }

            QueryDeviceTestInfoABMType alliantRequest = new QueryDeviceTestInfoABMType();
            alliantRequest.Company = device.Company.ExternalCode; // company
            alliantRequest.DeviceNumber = device.EquipmentNumber; // meterNumber
            alliantRequest.DeviceType = device.EquipmentType.ExternalCode; // deviceType
            alliantRequest.ServiceType = device.EquipmentType.ServiceType.ExternalCode; // serviceType
            alliantRequest.Tester = request.TesterId; // testerID
            alliantRequest.TestLocation = request.Location; // location
            alliantRequest.TestStandard = request.TestStandard; // testStandard

            QueryDeviceTestInfoResponseABMType alliantResponse;

            bool useMockup;
            bool.TryParse(ConfigurationManager.AppSettings["Test.UseMockup"], out useMockup);

            try
            {
                // get response from mockup file or web service
                if (useMockup)
                {
                    alliantResponse = this.GetDeviceResponseFromFile(device, alliantRequest);
                }
                else
                {
                    alliantResponse = this.GetDeviceResponseFromWebService(request.TransactionId, alliantRequest);
                }
            }
            catch (FaultException<Alliant.GetDevice.FaultNotificationType> ex)
            {
                Log.Error("Customer service call returned error.", ex);
                using (TextWriter writer = new StringWriter(CultureInfo.InvariantCulture))
                {
                    var serializer = new XmlSerializer(typeof(Alliant.GetDevice.FaultNotificationType));
                    serializer.Serialize(writer, ex.Detail);
                    Log.Error(writer.ToString());
                    ServiceFaultDetails transformedException = new ServiceFaultDetails()
                    {
                        Message = this.stringManager.GetString("CheckListenerLogReport", CultureInfo.CurrentCulture),
                        DebugInfo = writer.ToString()
                    };

                    throw new FaultException<ServiceFaultDetails>(transformedException, transformedException.Message);
                }
            }
            catch (FaultException<Alliant.GetDevice.FaultNotificationType[]> ex)
            {
                Log.Error("Customer service call returned error.", ex);
                using (TextWriter writer = new StringWriter(CultureInfo.InvariantCulture))
                {
                    var serializer = new XmlSerializer(typeof(Alliant.GetDevice.FaultNotificationType[]));
                    serializer.Serialize(writer, ex.Detail);
                    Log.Error(writer.ToString());
                    ServiceFaultDetails transformedException = new ServiceFaultDetails()
                    {
                        Message = this.stringManager.GetString("CheckListenerLogReport", CultureInfo.CurrentCulture),
                        DebugInfo = writer.ToString()
                    };

                    throw new FaultException<ServiceFaultDetails>(transformedException, transformedException.Message);
                }
            }
            catch (FaultException ex)
            {
                Log.Error("Service call returned error.", ex);
                throw;
            }
            catch (CommunicationException ex)
            {
                Log.Error("Service call failed.", ex);
                throw;
            }
            catch (TimeoutException ex)
            {
                Log.Error("Service call timed out.", ex);
                throw;
            }
            
            if (alliantResponse != null)
            {
                switch (device.EquipmentType.ServiceType.ExternalCode)
                {
                    case "E":
                        switch (device.EquipmentType.ExternalCode)
                        {
                            case "MR":
                                this.AddMeter(device, alliantResponse);
                                break;
                            case "CT":
                                this.AddCurrentTransformer(device, alliantResponse);
                                break;
                            case "PT":
                                this.AddPotentialTransformer(device, alliantResponse);
                                break;
                            default:
                                string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("DeviceTypeNotSupported", CultureInfo.CurrentCulture), device.EquipmentType.Description, device.EquipmentType.ServiceType.Description);
                                Log.Error(message);
                                throw new ArgumentException(message);
                        }

                        break;
                    default:
                        string message1 = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("ServiceTypeNotSupported", CultureInfo.CurrentCulture), device.EquipmentType.ServiceType.Description);
                        Log.Error(message1);
                        throw new ArgumentException(message1);
                }
            }
        }

        /// <summary>
        /// Called when [send test data].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <exception cref="System.ArgumentNullException">
        /// request;Can not send device test data if request is not specified.
        /// or
        /// device;Can not send device test data if device is not specified.
        /// or
        /// deviceTest;Can not send device test data if device test is not specified.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">Meter can not be found in WNP.</exception>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        protected override void OnSendTestData(SendTestDataServiceRequest request, DeviceTest deviceTest)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not send device test data if request is not specified.");
            }

            if (deviceTest == null)
            {
                throw new ArgumentNullException("deviceTest", "Can not send device test data if device test is not specified.");
            }

            Device device = deviceTest.Device;
            CreateDeviceTestResultABMType alliantRequest;

            RequestConstructor requestConstructor = new RequestConstructor(this.WnpSystem);

            switch (device.EquipmentType.ServiceType.ExternalCode)
            {
                case "E":
                    switch (device.EquipmentType.ExternalCode)
                    {
                        case "MR":
                            alliantRequest = requestConstructor.PrepareElectricMeterTestResultsRequest(deviceTest);
                            break;
                        case "CT":
                            alliantRequest = requestConstructor.PrepareCurrentTransformerTestResultsRequest(deviceTest);
                            break;
                        case "PT":
                            alliantRequest = requestConstructor.PreparePotentialTransformerTestResultsRequest(deviceTest);
                            break;
                        default:
                            string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("DeviceTypeNotSupported", CultureInfo.CurrentCulture), device.EquipmentType.Description, device.EquipmentType.ServiceType.Description);
                            Log.Error(message);
                            throw new ArgumentException(message);
                    }

                    break;
                default:
                    string message1 = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("ServiceTypeNotSupported", CultureInfo.CurrentCulture), device.EquipmentType.ServiceType.Description);
                    Log.Error(message1);
                    throw new ArgumentException(message1);
            }

            CreateDeviceTestResultResponseABMType alliantResponse;

            bool useMockup;
            if (!bool.TryParse(ConfigurationManager.AppSettings["Test.UseMockup"], out useMockup))
            {
                useMockup = false;
            }

            try
            {
                if (useMockup)
                {
                    alliantResponse = this.SendDeviceTestResponseFromFile(device, alliantRequest);
                }
                else
                {
                    alliantResponse = this.SendDeviceTestResponseFromWebService(request.TransactionId, alliantRequest);
                }
            }
            catch (FaultException<Alliant.SendTestResult.FaultNotificationType> ex)
            {
                Log.Error("Customer service call returned error.", ex);
                using (TextWriter writer = new StringWriter(CultureInfo.InvariantCulture))
                {
                    var serializer = new XmlSerializer(typeof(Alliant.SendTestResult.FaultNotificationType));
                    serializer.Serialize(writer, ex.Detail);
                    Log.Error(writer.ToString());
                    ServiceFaultDetails transformedException = new ServiceFaultDetails()
                    {
                        Message = this.stringManager.GetString("CheckListenerLogReport", CultureInfo.CurrentCulture),
                        DebugInfo = writer.ToString()
                    };

                    throw new FaultException<ServiceFaultDetails>(transformedException, transformedException.Message);
                }
            }
            catch (FaultException<Alliant.SendTestResult.FaultNotificationType[]> ex)
            {
                Log.Error("Customer service call returned error.", ex);
                using (TextWriter writer = new StringWriter(CultureInfo.InvariantCulture))
                {
                    var serializer = new XmlSerializer(typeof(Alliant.SendTestResult.FaultNotificationType[]));
                    serializer.Serialize(writer, ex.Detail);
                    Log.Error(writer.ToString());
                    ServiceFaultDetails transformedException = new ServiceFaultDetails()
                    {
                        Message = this.stringManager.GetString("CheckListenerLogReport", CultureInfo.CurrentCulture),
                        DebugInfo = writer.ToString()
                    };

                    throw new FaultException<ServiceFaultDetails>(transformedException, transformedException.Message);
                }
            }
            catch (FaultException ex)
            {
                Log.Error("Service call returned error.", ex);
                throw;
            }
            catch (CommunicationException ex)
            {
                Log.Error("Service call failed.", ex);
                throw;
            }
            catch (TimeoutException ex)
            {
                Log.Error("Service call timed out.", ex);
                throw;
            }
            
            if (alliantResponse != null)
            {
                // save test results id received from CC&B
                deviceTest.ExternalId = alliantResponse.DeviceTestID;
                this.DeviceManager.SaveDeviceTest(deviceTest);

                string message;
                switch (alliantResponse.DeviceTestStatus)
                {
                    case "E":
                        message = "CC&B received test results, but returned Error status with message: " + alliantResponse.ResultMessage;
                        throw new FaultException(message);
                    case "F":
                        message = "CC&B received test results, but returned Failure status with message: " + alliantResponse.ResultMessage;
                        throw new FaultException(message);
                    case "P":
                        break;
                    default:
                        message = "CC&B received test results, but returned unknown status " + alliantResponse.DeviceTestStatus + " with message: " + alliantResponse.ResultMessage;
                        throw new FaultException(message);
                }
            }
        }

        /// <summary>
        /// Adds the meter to WNP.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="alliantResponse">The alliant response.</param>
        private void AddMeter(Device device, QueryDeviceTestInfoResponseABMType alliantResponse)
        {
            Meter meter = new Meter()
            {
                EquipmentNumber = device.EquipmentNumber,
                Owner = new Owner(int.Parse(device.Company.InternalCode, CultureInfo.InvariantCulture)),
                FirmwareRevision1 = alliantResponse.CommunicationBoardVersion,
                FirmwareRevision2 = alliantResponse.CommunicationModuleFirmwareVersion,
                MeterCode = alliantResponse.ClassificationCode,
                CustomField1 = device.EquipmentType.ServiceType.ExternalCode,
                CustomField2 = device.EquipmentType.ExternalCode,
                CustomField3 = alliantResponse.NewDeviceIndicator,
                CustomField5 = alliantResponse.LossCompensationCodeFlag,
                CustomField20 = alliantResponse.TestReason
            };
            if (alliantResponse.LastTestDate.HasValue)
            {
                meter.CustomField4 = ((DateTime)alliantResponse.LastTestDate).ToLongDateString();
            }

            this.WnpSystem.AddOrReplaceEquipment(meter);
        }

        /// <summary>
        /// Adds the current transformer to WNP.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="alliantResponse">The alliant response.</param>
        private void AddCurrentTransformer(Device device, QueryDeviceTestInfoResponseABMType alliantResponse)
        {
            CurrentTransformer ct = new CurrentTransformer()
            {
                EquipmentNumber = device.EquipmentNumber,
                Owner = new Owner(int.Parse(device.Company.InternalCode, CultureInfo.InvariantCulture)),
                TransformerCode = alliantResponse.ClassificationCode,
                CustomField1 = device.EquipmentType.ServiceType.ExternalCode,
                CustomField2 = device.EquipmentType.ExternalCode,
                CustomField3 = alliantResponse.NewDeviceIndicator,
                CustomField5 = alliantResponse.TestReason
            };
            if (alliantResponse.LastTestDate.HasValue)
            {
                ct.CustomField4 = ((DateTime)alliantResponse.LastTestDate).ToLongDateString();
            }

            this.WnpSystem.AddOrReplaceEquipment(ct);
        }

        /// <summary>
        /// Adds the potential transformer to WNP.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="alliantResponse">The alliant response.</param>
        private void AddPotentialTransformer(Device device, QueryDeviceTestInfoResponseABMType alliantResponse)
        {
            PotentialTransformer pt = new PotentialTransformer()
            {
                EquipmentNumber = device.EquipmentNumber,
                Owner = new Owner(int.Parse(device.Company.InternalCode, CultureInfo.InvariantCulture)),
                TransformerCode = alliantResponse.ClassificationCode,
                CustomField1 = device.EquipmentType.ServiceType.ExternalCode,
                CustomField2 = device.EquipmentType.ExternalCode,
                CustomField3 = alliantResponse.NewDeviceIndicator,
                CustomField5 = alliantResponse.TestReason
            };
            if (alliantResponse.LastTestDate.HasValue)
            {
                pt.CustomField4 = ((DateTime)alliantResponse.LastTestDate).ToLongDateString();
            }

            this.WnpSystem.AddOrReplaceEquipment(pt);
        }

        /// <summary>
        /// Reads the get device response from file.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="alliantRequest">The alliant request.</param>
        /// <returns>
        /// The alliant response.
        /// </returns>
        /// <exception cref="System.ServiceModel.FaultException">Throws exception with specified message if fault file exists.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Throws exception if response file was not found.</exception>
        /// <exception cref="FileNotFoundException"></exception>
        private QueryDeviceTestInfoResponseABMType GetDeviceResponseFromFile(Device device, QueryDeviceTestInfoABMType alliantRequest)
        {
            QueryDeviceTestInfoResponseABMType alliantResponse = null;
            string mockupFilesLocation = ConfigurationManager.AppSettings["Test.MockupFilesLocation"];
            string mockupFile = Path.Combine(mockupFilesLocation, "GetDevice_" + device.EquipmentType.InternalCode + "_" + device.EquipmentNumber + ".xml");
            string mockupFileFault = Path.Combine(mockupFilesLocation, "GetDevice_Fault_" + device.EquipmentType.InternalCode + "_" + device.EquipmentNumber + ".txt");
            string mockupFileDefault = Path.Combine(mockupFilesLocation, "GetDevice_" + device.EquipmentType.InternalCode + "_Default.xml");
            string requestFilesLocation = Path.Combine(mockupFilesLocation, "Requests");
            string requestFile = Path.Combine(requestFilesLocation, "GetDevice_" + device.EquipmentType.InternalCode + "_" + device.EquipmentNumber + "_" + DateTime.Now.ToString("HH.mm.ss", CultureInfo.InvariantCulture) + ".xml");
            Directory.CreateDirectory(mockupFilesLocation);
            Directory.CreateDirectory(requestFilesLocation);
            Utilities.WriteToXmlFile<QueryDeviceTestInfoABMType>(requestFile, alliantRequest);

            if (File.Exists(mockupFileFault))
            {
                Alliant.GetDevice.FaultNotificationType[] detail = Utilities.ReadFromXmlFile<List<Alliant.GetDevice.FaultNotificationType>>(mockupFileFault).ToArray<Alliant.GetDevice.FaultNotificationType>();
                FaultException<Alliant.GetDevice.FaultNotificationType[]> ex = new FaultException<Alliant.GetDevice.FaultNotificationType[]>(detail);

                throw ex;
            }

            if (File.Exists(mockupFile))
            {
                alliantResponse = Utilities.ReadFromXmlFile<QueryDeviceTestInfoResponseABMType>(mockupFile);
            }
            else
            {
                if (File.Exists(mockupFileDefault))
                {
                    alliantResponse = Utilities.ReadFromXmlFile<QueryDeviceTestInfoResponseABMType>(mockupFileDefault);
                }
                else
                {
                    ////alliantResponse = new QueryDeviceTestInfoResponseABMType()
                    ////{
                    ////    ClassificationCode = "AA",
                    ////    CommunicationBoardVersion = "1.1.1.1",
                    ////    CommunicationModuleFirmwareVersion = "2",
                    ////    LastTestDateSpecified = true,
                    ////    LastTestDate = DateTime.Now.AddDays(-1),
                    ////    LossCompensationCodeFlag = "N",
                    ////    NewDeviceIndicator = "Y",
                    ////    TestReason = "AA"
                    ////};
                    ////Utilities.WriteToXmlFile<QueryDeviceTestInfoResponseABMType>(mockupFileDefault, alliantResponse);

                    ////List<Alliant.GetDevice.FaultNotificationType> faults = new List<GetDevice.FaultNotificationType>();
                    ////Alliant.GetDevice.FaultNotificationType fault = new Alliant.GetDevice.FaultNotificationType()
                    ////{
                    ////    BusinessComponentID = "123",
                    ////    CorrectiveAction = new string[] { "Fix value", "Fix other value" },
                    ////    FaultingService = new Alliant.GetDevice.FaultingServiceType()
                    ////    {
                    ////        ID = "456",
                    ////        ExecutionContextID = "789",
                    ////        ImplementationCode = "111",
                    ////        InstanceID = "222"
                    ////    },
                    ////    ReportingDateTime = DateTime.Now,
                    ////    ReportingDateTimeSpecified = true,
                    ////    FaultMessage = new GetDevice.FaultMessageType()
                    ////    {
                    ////        Code = "ns0:assertFailure",
                    ////        Text = new string[] { "See error Stack." },
                    ////        Stack = new string[] { "ns0:assertFailurefaultName" },
                    ////        Severity = "Critical"
                    ////    }
                    ////};
                    ////faults.Add(fault);
                    ////fault = new Alliant.GetDevice.FaultNotificationType()
                    ////{
                    ////    BusinessComponentID = "2123",
                    ////    CorrectiveAction = new string[] { "2Fix value", "Fix other value" },
                    ////    FaultingService = new Alliant.GetDevice.FaultingServiceType()
                    ////    {
                    ////        ID = "2456",
                    ////        ExecutionContextID = "2789",
                    ////        ImplementationCode = "2111",
                    ////        InstanceID = "2222"
                    ////    },
                    ////    ReportingDateTime = DateTime.Now,
                    ////    ReportingDateTimeSpecified = true,
                    ////    FaultMessage = new GetDevice.FaultMessageType()
                    ////    {
                    ////        Code = "2ns0:assertFailure",
                    ////        Text = new string[] { "2See error Stack." },
                    ////        Stack = new string[] { "2ns0:assertFailurefaultName" },
                    ////        Severity = "2Critical"
                    ////    }
                    ////};
                    ////faults.Add(fault);
                    ////Utilities.WriteToXmlFile<List<Alliant.GetDevice.FaultNotificationType>>(mockupFileFault, faults);
                     
                    string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("GetDeviceMockupFileNotFound", CultureInfo.CurrentCulture), mockupFile, mockupFileDefault);
                    Log.Error(message);
                    throw new FileNotFoundException(message);
                }
            }

            if (alliantResponse.LastTestDate > DateTime.Now)
            {
                alliantResponse.LastTestDate = DateTime.Now;
            }

            return alliantResponse;
        }

        /// <summary>
        /// Reads the send device test response from file.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="alliantRequest">The alliant request.</param>
        /// <returns>The alliant response.</returns>
        /// <exception cref="System.IO.FileNotFoundException">Throws exception if response file was not found.</exception>
        private CreateDeviceTestResultResponseABMType SendDeviceTestResponseFromFile(Device device, CreateDeviceTestResultABMType alliantRequest)
        {
            CreateDeviceTestResultResponseABMType alliantResponse;
            string mockupFilesLocation = ConfigurationManager.AppSettings["Test.MockupFilesLocation"];
            string mockupFile = Path.Combine(mockupFilesLocation, "SendDeviceTest_" + device.EquipmentType.InternalCode + "_" + device.EquipmentNumber + ".xml");
            string mockupFileFault = Path.Combine(mockupFilesLocation, "SendDeviceTest_Fault_" + device.EquipmentType.InternalCode + "_" + device.EquipmentNumber + ".txt");
            string mockupFileDefault = Path.Combine(mockupFilesLocation, "SendDeviceTest_" + device.EquipmentType.InternalCode + "_Default.xml");
            string requestFilesLocation = Path.Combine(mockupFilesLocation, "Requests");
            string requestFile = Path.Combine(requestFilesLocation, "SendDeviceTest_" + device.EquipmentType.InternalCode + "_" + device.EquipmentNumber + "_" + DateTime.Now.ToString("HH.mm.ss", CultureInfo.InvariantCulture) + ".xml");
            Directory.CreateDirectory(mockupFilesLocation);
            Directory.CreateDirectory(requestFilesLocation);
            Utilities.WriteToXmlFile<CreateDeviceTestResultABMType>(requestFile, alliantRequest);

            if (File.Exists(mockupFileFault))
            {
                Alliant.SendTestResult.FaultNotificationType[] detail = Utilities.ReadFromXmlFile<List<Alliant.SendTestResult.FaultNotificationType>>(mockupFileFault).ToArray<Alliant.SendTestResult.FaultNotificationType>();
                FaultException<Alliant.SendTestResult.FaultNotificationType[]> ex = new FaultException<Alliant.SendTestResult.FaultNotificationType[]>(detail);

                throw ex;
            }

            if (File.Exists(mockupFile))
            {
                alliantResponse = Utilities.ReadFromXmlFile<CreateDeviceTestResultResponseABMType>(mockupFile);
            }
            else
            {
                if (File.Exists(mockupFileDefault))
                {
                    alliantResponse = Utilities.ReadFromXmlFile<CreateDeviceTestResultResponseABMType>(mockupFileDefault);
                }
                else
                {
                    ////alliantResponse = new CreateDeviceTestResultResponseABMType()
                    ////{
                    ////    DeviceTestID = "1234",
                    ////    DeviceTestStatus = "P",
                    ////    ResultMessage = "OK"
                    ////};
                    ////Utilities.WriteToXmlFile<CreateDeviceTestResultResponseABMType>(mockupFileDefault, alliantResponse);

                    ////List<Alliant.SendTestResult.FaultNotificationType> faults = new List<SendTestResult.FaultNotificationType>();
                    ////Alliant.SendTestResult.FaultNotificationType fault = new Alliant.SendTestResult.FaultNotificationType()
                    ////{
                    ////    BusinessComponentID = "123",
                    ////    CorrectiveAction = new string[] { "Fix value", "Fix other value" },
                    ////    FaultingService = new Alliant.SendTestResult.FaultingServiceType()
                    ////    {
                    ////        ID = "456",
                    ////        ExecutionContextID = "789",
                    ////        ImplementationCode = "111",
                    ////        InstanceID = "222"
                    ////    },
                    ////    ReportingDateTime = DateTime.Now,
                    ////    ReportingDateTimeSpecified = true,
                    ////    FaultMessage = new SendTestResult.FaultMessageType()
                    ////    {
                    ////        Code = "ns0:assertFailure",
                    ////        Text = new string[] { "See error Stack." },
                    ////        Stack = new string[] { "ns0:assertFailurefaultName" },
                    ////        Severity = "Critical"
                    ////    }
                    ////};
                    ////faults.Add(fault);
                    ////fault = new Alliant.SendTestResult.FaultNotificationType()
                    ////{
                    ////    BusinessComponentID = "2123",
                    ////    CorrectiveAction = new string[] { "2Fix value", "Fix other value" },
                    ////    FaultingService = new Alliant.SendTestResult.FaultingServiceType()
                    ////    {
                    ////        ID = "2456",
                    ////        ExecutionContextID = "2789",
                    ////        ImplementationCode = "2111",
                    ////        InstanceID = "2222"
                    ////    },
                    ////    ReportingDateTime = DateTime.Now,
                    ////    ReportingDateTimeSpecified = true,
                    ////    FaultMessage = new SendTestResult.FaultMessageType()
                    ////    {
                    ////        Code = "2ns0:assertFailure",
                    ////        Text = new string[] { "2See error Stack." },
                    ////        Stack = new string[] { "2ns0:assertFailurefaultName" },
                    ////        Severity = "2Critical"
                    ////    }
                    ////};
                    ////faults.Add(fault);
                    ////Utilities.WriteToXmlFile<List<Alliant.SendTestResult.FaultNotificationType>>(mockupFileFault, faults);

                    string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("SendDeviceTestMockupFileNotFound", CultureInfo.CurrentCulture), mockupFile, mockupFileDefault);
                    Log.Error(message);
                    throw new FileNotFoundException(message);
                }
            }

            return alliantResponse;
        }

        /// <summary>
        /// Gets the get device response from web service.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="alliantRequest">The alliant request.</param>
        /// <returns>The alliant response.</returns>
        private QueryDeviceTestInfoResponseABMType GetDeviceResponseFromWebService(int transactionId, QueryDeviceTestInfoABMType alliantRequest)
        {
            QueryDeviceTestInfoResponseABMType alliantResponse = null;
            using (DeviceTestInfoABCSClient client = new DeviceTestInfoABCSClient())
            {
                client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WebService.Client.UserName"];
                client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WebService.Client.Password"];
                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ServiceSendMessage);
                alliantResponse = client.Query(alliantRequest);
            }

            return alliantResponse;
        }
        
        /// <summary>
        /// Gets the send device test response from web service.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="alliantRequest">The alliant request.</param>
        /// <returns>The alliant response.</returns>
        private CreateDeviceTestResultResponseABMType SendDeviceTestResponseFromWebService(int transactionId, CreateDeviceTestResultABMType alliantRequest)
        {
            CreateDeviceTestResultResponseABMType alliantResponse = null;
            using (DeviceTestResultABCSClient client = new DeviceTestResultABCSClient())
            {
                client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["WebService.Client.UserName"];
                client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["WebService.Client.Password"];
                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ServiceSendMessage);
                alliantResponse = client.Create(alliantRequest);
            }

            return alliantResponse;
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="CustomService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.KCPL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Threading;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP.Lookup;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Globalization;
    using AMSLLC.Listener.Service.Contract;
    using AMSLLC.Listener.Service.Implementation;
    using AMSLLC.Listener.Service.Implementation.KCPL.Messages;
    using AMSLLC.Listener.Service.Implementation.MessageBasedSoap;
    using FileHelpers;
    using log4net;

    /// <summary>
    /// Alliant customer specific service implementation.
    /// </summary>
    public class CustomService : ServiceCore
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The string manager
        /// </summary>
        private static readonly ResourceManager StringManager = Init.StringManager;

        /// <summary>
        /// The string manager for customer specific notifications.
        /// </summary>
        private static readonly ResourceManager CustomStringManager = new ResourceManager("AMSLLC.Listener.Service.Implementation.KCPL.Properties.Resources", Assembly.GetExecutingAssembly());

        /// <summary>
        /// The meter equipment type
        /// </summary>
        private static EquipmentType meterEquipmentType;

        /// <summary>
        /// The company
        /// </summary>
        private static Company company;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomService"/> class.
        /// </summary>
        public CustomService()
            : base()
        {
            meterEquipmentType = this.DeviceManager.GetEquipmentTypeByInternalCode("E", "EM");
            company = this.DeviceManager.GetCompanyByInternalCode("0".ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Processes initial load to ODM.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is expected behaviour. Exception will be logged and processing must continue")]
        public void ProcessInitialLoad()
        {
            string errorMessage = string.Empty;
            IList<Meter> meters = this.WnpSystem.GetEquipmentIdOnly<Meter>();

            IList<TransactionType> deviceTransactionTypes = this.TransactionLogManager.GetTransactionTypes(TransactionDataLookup.Device, TransactionDirectionLookup.Outgoing, TransactionSourceLookup.WNP);
            TransactionType deviceOdmTransaction = null;
            foreach (TransactionType transactionType in deviceTransactionTypes)
            {
                if (transactionType.ExternalSystem.Name == "ODM")
                {
                    deviceOdmTransaction = transactionType;
                }
            }

            IList<TransactionType> deviceTestTransactionTypes = this.TransactionLogManager.GetTransactionTypes(TransactionDataLookup.DeviceTest, TransactionDirectionLookup.Outgoing, TransactionSourceLookup.WNP);
            TransactionType deviceTestOdmTransaction = null;
            foreach (TransactionType transactionType in deviceTestTransactionTypes)
            {
                if (transactionType.ExternalSystem.Name == "ODM")
                {
                    deviceTestOdmTransaction = transactionType;
                }
            }

            int totalMeters = meters.Count;
            int currentMeter = 0;

            foreach (Meter meter in meters)
            {
                currentMeter++;
                Log.Fatal(string.Format(CultureInfo.InvariantCulture, "Processing {0} of {1}", currentMeter, totalMeters));

                Meter fullMeter = this.WnpSystem.GetEquipment<Meter>(meter.EquipmentNumber, meter.Owner.Id);

                if (string.IsNullOrWhiteSpace(fullMeter.CustomField13))
                {
                    continue;
                }

                Device device = this.CreateDevice(fullMeter);

                string currentHash = this.TransactionLogManager.GetLastSuccessfulDeviceTransactionDataHash(device, deviceOdmTransaction);
                string newHash = GetMeterHash(fullMeter);
                
                if (currentHash != newHash)
                {
                    int deviceTransactionId = this.TransactionLogManager.NewTransaction(deviceOdmTransaction.Id, device.Id, null, null);
                    this.TransactionLogManager.UpdateTransactionDataHash(deviceTransactionId, newHash);

                    this.TransactionLogManager.UpdateTransactionStatus(deviceTransactionId, TransactionStatusLookup.Succeeded, CustomStringManager.GetString("InitialLoadTransactionMessage", CultureInfo.CurrentCulture), null);
                }

                if (fullMeter.CreateDate < new DateTime(2008, 1, 1) || fullMeter.CustomField13 != "A")
                {
                    continue;
                }

                IList<MeterTestResult> meterTests = this.WnpSystem.GetEquipmentAllTestResult<MeterTestResult>(meter.EquipmentNumber, meter.Owner.Id);
                IList<DateTime> uniqueTestStarts = meterTests.Select(x => x.TestDate).Distinct().ToList();

                foreach (DateTime testDate in uniqueTestStarts)
                {
                    DeviceTest deviceTest = this.CreateDeviceTest(device, testDate);

                    currentHash = this.TransactionLogManager.GetLastSuccessfulDeviceTestTransactionDataHash(deviceTest, deviceTestOdmTransaction);

                    if (currentHash == GlobalConstants.PreviousSuccessfulTransactionNotFound)
                    {
                        int testTransactionId = this.TransactionLogManager.NewTransaction(deviceTestOdmTransaction.Id, device.Id, deviceTest.Id, null);

                        try
                        {
                            this.TransactionLogManager.UpdateTransactionState(testTransactionId, TransactionStateLookup.ServiceStart);
                            this.ProcessMeterTestResults(device, deviceTest, fullMeter, testTransactionId);
                            this.TransactionLogManager.UpdateTransactionState(testTransactionId, TransactionStateLookup.ServiceEnd);
                        }
                        catch (Exception ex)
                        {
                            string message = string.Format(CultureInfo.InvariantCulture, CustomStringManager.GetString("MeterTestProcessingFailed", CultureInfo.CurrentCulture), device.EquipmentNumber, deviceTest.TestDate);
                            Log.Error(message, ex);
                            errorMessage += message;
                            this.TransactionLogManager.UpdateTransactionStatus(testTransactionId, (int)TransactionStatusLookup.Failed, message, ex.ToString());
                        }
                    }
                }
            }

            Log.Error(errorMessage);
        }

        /// <summary>
        /// Called when [send device data]. Must override with client specific implementation.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="device">The device.</param>
        /// <exception cref="System.ArgumentNullException">device;Can not send device data if device is not specified.</exception>
        /// <exception cref="System.InvalidOperationException">Meter can not be found in WNP.</exception>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        protected override void OnSendDeviceData(int transactionId, Device device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device", "Can not send device data if device is not specified.");
            }

            Log.Info("KCPL Send Device Data");

            Meter meter = this.WnpSystem.GetEquipment<Meter>(device.EquipmentNumber, device.Company.Id);
            if (meter == null)
            {
                throw new InvalidOperationException("Meter can not be found in WNP.");
            }

            string message;
            switch (device.EquipmentType.ServiceType.ExternalCode)
            {
                case "E":
                    switch (device.EquipmentType.ExternalCode)
                    {
                        case "EM":
                            this.ProcessMeter(device, meter, transactionId);
                            break;
                        default:
                            message = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("DeviceTypeNotSupported", CultureInfo.CurrentCulture), device.EquipmentType.Description, device.EquipmentType.ServiceType.Description);
                            Log.Error(message);
                            throw new ArgumentException(message);
                    }

                    break;
                default:
                    string message1 = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("ServiceTypeNotSupported", CultureInfo.CurrentCulture), device.EquipmentType.ServiceType.Description);
                    Log.Error(message1);
                    throw new ArgumentException(message1);
            }
        }
        
        /// <summary>
        /// Called when [send test data].
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <exception cref="System.ArgumentNullException">
        /// deviceTest;Can not send device test data if device test is not specified.
        /// </exception>
        protected override void OnSendTestData(int transactionId, DeviceTest deviceTest)
        {
            if (deviceTest == null)
            {
                throw new ArgumentNullException("deviceTest", "Can not send device test data if device test is not specified.");
            }

            Log.Info("KCPL Send Test Data");

            Device device = deviceTest.Device;
            Meter meter = this.WnpSystem.GetEquipment<Meter>(device.EquipmentNumber, device.Company.Id);
            if (meter == null)
            {
                throw new InvalidOperationException("Meter can not be found in WNP.");
            }

            string message;
            switch (device.EquipmentType.ServiceType.ExternalCode)
            {
                case "E":
                    switch (device.EquipmentType.ExternalCode)
                    {
                        case "EM":
                            this.ProcessMeterTestResults(device, deviceTest, meter, transactionId);
                            break;
                        default:
                            message = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("DeviceTypeNotSupported", CultureInfo.CurrentCulture), device.EquipmentType.Description, device.EquipmentType.ServiceType.Description);
                            Log.Error(message);
                            throw new ArgumentException(message);
                    }

                    break;
                default:
                    string message1 = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("ServiceTypeNotSupported", CultureInfo.CurrentCulture), device.EquipmentType.ServiceType.Description);
                    Log.Error(message1);
                    throw new ArgumentException(message1);
            } 
        }

        /// <summary>
        /// Called when [send batch data]. Must override with client specific implementation.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="batchNumber">The batch number.</param>
        /// <exception cref="System.NotImplementedException">This transaction type is not available for your company.</exception>
        protected override void OnSendBatchData(int transactionId, string batchNumber)
        {
            NewBatch batch = this.WnpSystem.GetNewBatch(batchNumber);
            if (batch == null)
            {
                throw new InvalidOperationException("Batch can not be found in WNP.");
            }

            string message;
            switch (batch.EquipmentType)
            {
                case "EM":
                    this.ProcessMetersBatch(batch);
                    break;
                default:
                    message = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("BatchDeviceTypeNotSupported", CultureInfo.CurrentCulture), batch.EquipmentType);
                    Log.Error(message);
                    throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Saves the results to file.
        /// </summary>
        /// <param name="results">The test results string.</param>
        /// <param name="exportFile">The export file.</param>
        private static void SaveResultsToFile(string results, string exportFile)
        {
            string fileLocation = Path.GetDirectoryName(exportFile);
            Directory.CreateDirectory(fileLocation);

            using (StreamWriter writer = File.AppendText(exportFile))
            {
                writer.Write(results);
            }
        }

        /// <summary>
        /// Prepares the electric meter asset load request for ODM.
        /// </summary>
        /// <param name="meter">The meter.</param>
        /// <returns>The asset load request for ODM</returns>
        private static AssetLoadServiceRequest PrepareElectricMeterAssetLoadForODM(Meter meter)
        {
            AssetLoadServiceRequest serviceRequest = new AssetLoadServiceRequest()
            {
                assetDetails = new AssetLoadServiceRequestAssetDetails()
                {
                    badgeNo = GetMeterBadgeNumber(meter),
                    commModuleFirmware = meter.FirmwareRevision1,
                    configurationGroup = null,
                    dcw = meter.CustomField4,
                    endPointId = meter.CustomField2,
                    externalId = null,
                    hhfId = meter.SerialNumber,
                    itronId = GetMeterItronId(meter),
                    kH = meter.KH,
                    manufacturer = meter.Manufacturer,
                    meterBase = meter.Base.ToString(CultureInfo.InvariantCulture),
                    meterClass = meter.CustomField3,
                    meterCode = meter.MeterCode,
                    meterForm = meter.Form,
                    metrologyFirmware = meter.FirmwareRevision2,
                    model = meter.ModelNumber,
                    ownershipTerritory = meter.CustomField1,
                    phase = meter.Phase.ToString(CultureInfo.InvariantCulture),
                    programId = meter.ProgramId,
                    purchaseOrderNo = meter.PurchaseOrderReference,
                    serialNo = GetMeterSerialNumber(meter),
                    vendor = meter.Manufacturer,
                    vendorPartNo = meter.CustomField6,
                    warrantyDetail = null,
                    wires = meter.Wire.ToString(CultureInfo.InvariantCulture),
                    zigbeeFirmware = meter.FirmwareRevision3
                }
            };

            DateTime tempDate;
            if (DateTime.TryParseExact(meter.CustomField15, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
            {
                serviceRequest.assetDetails.warrantyExpirationDate = tempDate;
            }
            else
            {
                string message = string.Format(CultureInfo.InvariantCulture, CustomService.CustomStringManager.GetString("WarrantyExpirationDateNotFilled", CultureInfo.CurrentCulture), meter.CustomField15);
                Log.Error(message);
                throw new ArgumentException(message);
            }

            if (meter.PurchaseDate.HasValue)
            {
                serviceRequest.assetDetails.meterReceiptDate = meter.PurchaseDate;
            }
            else
            {
                string message = CustomStringManager.GetString("PurchaseDateNotFilled", CultureInfo.CurrentCulture);
                Log.Error(message);
                throw new ArgumentException(message);
            }
  
            if (meter.KwhDials.HasValue)
            {
                serviceRequest.assetDetails.numberOfDials = meter.KwhDials.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (meter.TestAmps.HasValue)
            {
                serviceRequest.assetDetails.testAmps = meter.TestAmps.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (meter.TestVolts.HasValue)
            {
                serviceRequest.assetDetails.voltage = meter.TestVolts.Value.ToString(CultureInfo.InvariantCulture);
            }

            return serviceRequest;
        }

        /// <summary>
        /// Prepares the electric meter asset update request for ODM.
        /// </summary>
        /// <param name="meter">The meter.</param>
        /// <returns>
        /// The asset update request for ODM
        /// </returns>
        /// <exception cref="System.ArgumentException">Throws exception if device status is not supported.</exception>
        private static AssetUpdateServiceRequest PrepareElectricMeterAssetUpdateForODM(Meter meter)
        {
            AssetUpdateServiceRequest request = new AssetUpdateServiceRequest()
            {
                badgeNo = GetMeterBadgeNumber(meter),
            };

            if (meter.ModifiedDate.HasValue)
            {
                request.statusDateTime = meter.ModifiedDate.Value;
            }

            switch (meter.CustomField13)
            {
                case "A":
                    request.status = AssetUpdateServiceRequestStatus.ACTIVE;
                    break;
                case "R":
                    request.status = AssetUpdateServiceRequestStatus.RETIRED;
                    request.retirementReasonCode = meter.CustomField9;
                    break;
                case "P":
                    request.status = AssetUpdateServiceRequestStatus.REPAIR;
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, CustomStringManager.GetString("MeterStatusNotSupported", CultureInfo.CurrentCulture), meter.CustomField13);
                    Log.Error(message);
                    throw new ArgumentException(message);                    
            }

            return request;
        }

        /// <summary>
        /// Gets the hash for the meter.
        /// </summary>
        /// <param name="meter">The meter.</param>
        /// <returns>The hash calculated from meter record.</returns>
        private static string GetMeterHash(Meter meter)
        {
            return Utilities.GetHash(meter.CustomField13);
        }

        /// <summary>
        /// Gets the meter badge number.
        /// </summary>
        /// <param name="meter">The meter.</param>
        /// <returns>The badge number.</returns>
        private static string GetMeterBadgeNumber(Meter meter)
        {
            return meter.Manufacturer + meter.MeterCode + meter.SerialNumber;
        }

        /// <summary>
        /// Gets the meter serial number as it is defined in ODM.
        /// </summary>
        /// <param name="meter">The meter.</param>
        /// <returns>The meter serial number as it is defined in ODM.</returns>
        /// <exception cref="System.ArgumentException">Throws exception if device company is not supported.</exception>
        private static string GetMeterSerialNumber(Meter meter)
        {
            string result;

            switch (meter.CustomField1)
            {
                case "KCPL":
                    result = meter.SerialNumber;
                    break;
                case "MPS":
                case "SJLP":
                    result = meter.CustomField16 + meter.SerialNumber;
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("CompanyNotSupported", CultureInfo.CurrentCulture), meter.CustomField1);
                    Log.Error(message);
                    throw new ArgumentException(message);
            }

            return result;
        }

        /// <summary>
        /// Gets the meter itron identifier.
        /// </summary>
        /// <param name="meter">The meter.</param>
        /// <returns>The itron identifier.</returns>
        /// <exception cref="System.ArgumentException">Throws exception if device company is not supported.</exception>
        private static string GetMeterItronId(Meter meter)
        {
            string result;

            switch (meter.CustomField1)
            {
                case "KCPL":
                    result = GetMeterBadgeNumber(meter);
                    break;
                case "MPS":
                case "SJLP":
                    result = GetMeterSerialNumber(meter);
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("CompanyNotSupported", CultureInfo.CurrentCulture), meter.CustomField1);
                    Log.Error(message);
                    throw new ArgumentException(message);
            }

            return result;
        }
        
        /// <summary>
        /// Processes the meters batch.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <exception cref="System.AggregateException">All errors from sub transactions.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "All exceptions are logged and processing is continued.")]
        private void ProcessMetersBatch(NewBatch batch)
        {
            string errorMessage = string.Empty;
            IList<Meter> batchMeters = this.WnpSystem.GetEquipmentByBatch<Meter>(batch);

            foreach (Meter meter in batchMeters)
            {
                Device device = this.CreateDevice(meter);
                IList<TransactionType> deviceTransactionTypes = this.TransactionLogManager.GetTransactionTypes(TransactionDataLookup.Device, TransactionDirectionLookup.Outgoing, TransactionSourceLookup.WNP);

                if (deviceTransactionTypes.Count > 0)
                {
                    foreach (TransactionType transactionType in deviceTransactionTypes)
                    {
                        int deviceTransactionId = this.TransactionLogManager.NewTransaction(transactionType.Id, device.Id, null, null);
                        try
                        {
                            this.TransactionLogManager.UpdateTransactionState(deviceTransactionId, TransactionStateLookup.ServiceStart);

                            this.ProcessMeter(device, meter, deviceTransactionId, true);

                            this.TransactionLogManager.UpdateTransactionState(deviceTransactionId, TransactionStateLookup.ServiceEnd);
                        }
                        catch (Exception ex)
                        {
                            string message = string.Format(CultureInfo.InvariantCulture, CustomStringManager.GetString("MeterProcessingFailed", CultureInfo.CurrentCulture), device.EquipmentNumber);
                            Log.Error(message, ex);
                            errorMessage += message;
                            this.TransactionLogManager.UpdateTransactionStatus(deviceTransactionId, (int)TransactionStatusLookup.Failed, ex.Message, ex.ToString());
                        }
                    }
                }

                IList<MeterTestResult> meterTests = this.WnpSystem.GetEquipmentAllTestResult<MeterTestResult>(meter.EquipmentNumber, meter.Owner.Id);
                IList<DateTime> uniqueTestStarts = meterTests.Select(x => x.TestDate).Distinct().ToList();

                foreach (DateTime testDate in uniqueTestStarts)
                {
                    DeviceTest deviceTest = this.CreateDeviceTest(device, testDate);
                    IList<TransactionType> deviceTestTransactionTypes = this.TransactionLogManager.GetTransactionTypes(TransactionDataLookup.DeviceTest, TransactionDirectionLookup.Outgoing, TransactionSourceLookup.WNP);

                    if (deviceTestTransactionTypes.Count > 0)
                    {
                        foreach (TransactionType transactionType in deviceTestTransactionTypes)
                        {
                            int testTransactionId = this.TransactionLogManager.NewTransaction(transactionType.Id, device.Id, deviceTest.Id, null);
                            try
                            {
                                this.TransactionLogManager.UpdateTransactionState(testTransactionId, TransactionStateLookup.ServiceStart);

                                this.ProcessMeterTestResults(device, deviceTest, meter, testTransactionId);

                                this.TransactionLogManager.UpdateTransactionState(testTransactionId, TransactionStateLookup.ServiceEnd);

                                TransactionLog transaction = this.TransactionLogManager.GetTransaction(testTransactionId);
                                if (transactionType.ExternalSystem.Name == "CIS" && transaction.TransactionStatus.Id == (int)TransactionStatusLookup.InProgress)
                                {
                                    this.TransactionLogManager.UpdateTransactionStatus(testTransactionId, (int)TransactionStatusLookup.Succeeded);
                                }
                            }
                            catch (Exception ex)
                            {
                                string message = string.Format(CultureInfo.InvariantCulture, CustomStringManager.GetString("MeterTestProcessingFailed", CultureInfo.CurrentCulture), device.EquipmentNumber, deviceTest.TestDate);
                                Log.Error(message, ex);
                                errorMessage += message;
                                this.TransactionLogManager.UpdateTransactionStatus(testTransactionId, (int)TransactionStatusLookup.Failed, ex.Message, ex.ToString());
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new AggregateException(errorMessage);
            }
        }

        /// <summary>
        /// Processes the electric meter.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="meter">The meter.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        private void ProcessMeter(Device device, Meter meter, int transactionId)
        {
            this.ProcessMeter(device, meter, transactionId, false);
        }

        /// <summary>
        /// Processes the electric meter.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="meter">The meter.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="batchAcceptance">If set to <c>true</c> then it is triggered by batch acceptance process.</param>
        private void ProcessMeter(Device device, Meter meter, int transactionId, bool batchAcceptance)
        {
            if (meter.NewBatch != null &&
                (meter.NewBatch.Status == char.Parse(Utilities.GetEnumDescription(NewBatchLookup.New)) ||
                meter.NewBatch.Status == char.Parse(Utilities.GetEnumDescription(NewBatchLookup.Pending))))
            {
                string message = string.Format(CultureInfo.InvariantCulture, CustomStringManager.GetString("SkipMeterBelongsToNewBatch", CultureInfo.CurrentCulture), meter.NewBatch.Description);
                Log.Info(message);
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Skipped, message, null);
                return;
            }

            TransactionLog currentTransaction = this.TransactionLogManager.GetTransaction(transactionId);

            string previousHash = this.TransactionLogManager.GetLastSuccessfulDeviceTransactionDataHash(device, currentTransaction.TransactionType);
            string currentHash = GetMeterHash(meter);

            if (previousHash != currentHash)
            {
                if (batchAcceptance)
                {
                    // set meter to active status, because ODM by default sets all new meters as active
                    meter.CustomField13 = "A";
                    this.TransactionLogManager.UpdateTransactionDataHash(transactionId, GetMeterHash(meter));

                    AssetLoadServiceRequest kcplServiceRequest = PrepareElectricMeterAssetLoadForODM(meter);
                    this.CallOdm(kcplServiceRequest, ConfigurationManager.AppSettings["Kcpl.AssetLoad.Url"], transactionId);
                }
                else
                {
                    this.TransactionLogManager.UpdateTransactionDataHash(transactionId, currentHash);

                    AssetUpdateServiceRequest kcplServiceRequest = PrepareElectricMeterAssetUpdateForODM(meter);
                    this.CallOdm(kcplServiceRequest, ConfigurationManager.AppSettings["Kcpl.AssetUpdate.Url"], transactionId);
                } 
            }
            else
            {
                string message = CustomStringManager.GetString("SkipMeterStatusNotChanged", CultureInfo.CurrentCulture);
                Log.Info(message);
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Skipped, message, null);
            }
        }

        /// <summary>
        /// Processes the test results of electric meter.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="meter">The meter.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <exception cref="System.ArgumentException">Throws exception if external system is not supported.</exception>
        private void ProcessMeterTestResults(Device device, DeviceTest deviceTest, Meter meter, int transactionId)
        {
            if (meter.NewBatch != null &&
                (meter.NewBatch.Status == char.Parse(Utilities.GetEnumDescription(NewBatchLookup.New)) ||
                meter.NewBatch.Status == char.Parse(Utilities.GetEnumDescription(NewBatchLookup.Pending))))
            {
                string message = string.Format(CultureInfo.InvariantCulture, CustomStringManager.GetString("SkipMeterBelongsToNewBatch", CultureInfo.CurrentCulture), meter.NewBatch.Description);
                Log.Info(message);
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Skipped, message, null);
                return;
            }

            TransactionLog currentTransaction = this.TransactionLogManager.GetTransaction(transactionId);
            string previousHash = this.TransactionLogManager.GetLastSuccessfulDeviceTestTransactionDataHash(deviceTest, currentTransaction.TransactionType);

            if (previousHash == GlobalConstants.PreviousSuccessfulTransactionNotFound)
            {
                switch (currentTransaction.TransactionType.ExternalSystem.Name)
                {
                    case "CIS":
                        this.ProcessCisTestResults(device, deviceTest, meter);
                        break;
                    case "ODM":
                        TestResultServiceRequest serviceRequest = this.PrepareElectricMeterTestResultsForODM(device, deviceTest, meter);
                        this.CallOdm(serviceRequest, ConfigurationManager.AppSettings["Kcpl.AssetTestResult.Url"], transactionId);
                        break;
                    default:
                        string message = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("ExternalSystemNotSupported", CultureInfo.CurrentCulture), currentTransaction.TransactionType.ExternalSystem.Name);
                        Log.Error(message);
                        throw new ArgumentException(message);
                }
            }
            else
            {
                string message = StringManager.GetString("SkipDuplicate", CultureInfo.CurrentCulture);
                Log.Info(message);
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Skipped, message, null);
            }
        }
        
        /// <summary>
        /// Processes meter test results export to CIS flat files.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="meter">The meter.</param>
        /// <exception cref="System.ArgumentException">Throws exception if device company is not supported.</exception>
        private void ProcessCisTestResults(Device device, DeviceTest deviceTest, Meter meter)
        {
            switch (meter.CustomField1)
            {
                case "KCPL":
                    string kcplCisEntry = this.PrepareElectricMeterTestResultsForKcplCisFile(device, deviceTest, meter);
                    SaveResultsToFile(kcplCisEntry, ConfigurationManager.AppSettings["ExportFileLocation.KcplCis"]);
                    break;
                case "MPS":
                case "SJLP":
                    string gmoCisEntry = this.PrepareElectricMeterTestResultsForGmoCisFile(device, deviceTest, meter);
                    SaveResultsToFile(gmoCisEntry, ConfigurationManager.AppSettings["ExportFileLocation.GmoCis"]);
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("CompanyNotSupported", CultureInfo.CurrentCulture), meter.CustomField1);
                    Log.Error(message);
                    throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Sends specified request to ODM.
        /// </summary>
        /// <typeparam name="T">The request type.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="serviceAddress">The service address.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        private void CallOdm<T>(T request, string serviceAddress, int transactionId) where T : IXmlNamespaceExtension, IOdmRequest
        {
            request.listenerTransactionId = transactionId;
            Uri address = new Uri(serviceAddress);

            try
            {
////                this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ServiceSendMessage);
                MessageBasedSoapWebService.CallWebService<T>(address, request);
            }
            catch (Exception ex)
            {
                string message = StringManager.GetString("ServiceCallFailed", CultureInfo.CurrentCulture);
                Log.Error(message, ex);
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Failed, message, ex.ToString());
                throw;
            }
        }
         
        /// <summary>
        /// Prepares the electric meter test results for GMO cis file.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="meter">The meter.</param>
        /// <returns>
        /// The electric meter test results as a string.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Meter can not be found in WNP.
        /// or
        /// Can not prepare GMO CIS file export entry, because there is more than one comment related to this test.
        /// or
        /// Can not prepare GMO CIS file export entry, because there is more than one reading related to this test.</exception>
        private string PrepareElectricMeterTestResultsForGmoCisFile(Device device, DeviceTest deviceTest, Meter meter)
        {
            int owner = int.Parse(device.Company.InternalCode, CultureInfo.InvariantCulture);

            IList<MeterTestResult> meterTestResults = this.WnpSystem.GetEquipmentTestResult<MeterTestResult>(device.EquipmentNumber, owner, deviceTest.TestDate);
            if (meterTestResults.Count == 0)
            {
                throw new InvalidOperationException("Meter test results can not be found in WNP.");
            }

            MeterTestResult meterTest = meterTestResults.First<MeterTestResult>();
            GmoCisFile gmoCisFile = new GmoCisFile()
            {
                Manufacturer = meter.CustomField16,
                MeterSerialNumber = meter.SerialNumber,
                NWH = "NWH",
                TestStartTime = meterTest.TestDate,
                TestEndTime = meterTest.TestDateStop,
                TesterId = meterTest.TesterId,
                IndependentBusinessUnit = meter.CustomField1 == "MPS" ? "MOWAC" : "MOWAF",
                MeterType = meter.CustomField10,
                Wires2 = "W",
                Class = meter.CustomField3,
                RegisterType = meter.CustomField7,
                DemandInterval = "  M",
                MeterConstant = 1,
                BoardId = meterTest.WecoSerialNumber,
                CommentsPrefix = meter.CustomField1 == "MPS" ? "MPS" : "SJ ",
                Comments = this.WnpSystem.GetTestCommentsConcatenated(device.EquipmentNumber, owner, device.EquipmentType.InternalCode, deviceTest.TestDate),
                CommentDate = meterTest.TestDate,
                TestCodePrefix = "RSN TEST ",
                MeterStatusPrefix = " REPR CODE ",
                AepTestSetup = meter.AepCode,
                Form = meter.Form,
                Base = meter.Base.ToString(),
                Volts = (decimal)meter.TestVolts,
                Amps = (decimal)meter.TestAmps,
                NotUsed13 = " 99.70100.30 99.00101.00 99.70100.30 99.70100.30 99.00101.00 99.70100.30  2.00  2.00  2.00  1  1  1  1  1  1 0 299999999999999999999999999",
                AsFoundFullLoad = Transformations.GetAsFound(meterTestResults, 'S', "FL", -999.99M),
                AsFoundPowerFactor = Transformations.GetAsFound(meterTestResults, 'S', "PF", -999.99M),
                AsFoundLightLoad = Transformations.GetAsFound(meterTestResults, 'S', "LL", -999.99M),
                AsFoundAFullLoad = Transformations.GetAsFound(meterTestResults, 'A', "FL", -999.99M),
                NotUsed14 = "-999.99-999.99",
                AsFoundBFullLoad = Transformations.GetAsFound(meterTestResults, 'B', "FL", -999.99M),
                NotUsed15 = "-999.99-999.99",
                AsFoundCFullLoad = Transformations.GetAsFound(meterTestResults, 'C', "FL", -999.99M),
                NotUsed16 = "-999.99-999.99",
                AsLeftFullLoad = Transformations.GetAsLeft(meterTestResults, 'S', "FL", -999.99M),
                AsLeftPowerFactor = Transformations.GetAsLeft(meterTestResults, 'S', "PF", -999.99M),
                AsLeftLightLoad = Transformations.GetAsLeft(meterTestResults, 'S', "LL", -999.99M),
                AsLeftAFullLoad = Transformations.GetAsLeft(meterTestResults, 'A', "FL", -999.99M),
                NotUsed17 = "-999.99-999.99",
                AsLeftBFullLoad = Transformations.GetAsLeft(meterTestResults, 'B', "FL", -999.99M),
                NotUsed18 = "-999.99-999.99",
                AsLeftCFullLoad = Transformations.GetAsLeft(meterTestResults, 'C', "FL", -999.99M),
                NotUsed19 = "-999.99-999.99",
                AsFoundWeightedAverage = Transformations.GetAsFound(meterTestResults, 'S', "WA", -999.99M),
                AsLeftWeightedAverage = Transformations.GetAsLeft(meterTestResults, 'S', "WA", -999.99M),
                LineEnd = "    0    0                    900  900  1-999.99000   0.00000NO      600"
            };

            if (!string.IsNullOrWhiteSpace(meter.CustomField9))
            {
                switch (meter.CustomField9)
                {
                    case "OB":
                        gmoCisFile.RetireReason = "01";
                        break;
                    case "MD":
                        gmoCisFile.RetireReason = "02";
                        break;
                    case "TM":
                        gmoCisFile.RetireReason = "03";
                        break;
                    default:
                        gmoCisFile.RetireReason = "04";
                        break;
                }
            }

            switch (meterTest.TestReason)
            {
                case "MT":
                    gmoCisFile.TestCode = "02";
                    break;
                case "NS":
                case "NT":
                    gmoCisFile.TestCode = "03";
                    break;
                case "SS":
                    gmoCisFile.TestCode = "04";
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, "Test Code {0} is not supported by CIS.", meterTest.TestReason);
                    Log.Error(message);
                    throw new InvalidOperationException(message);
            }

            switch (meter.CustomField13)
            {
                case "A":
                    gmoCisFile.MeterStatus = "01";
                    break;
                case "R":
                    gmoCisFile.MeterStatus = "08";
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, "Meter status {0} is not supported by CIS.", meter.CustomField13);
                    Log.Error(message);
                    throw new InvalidOperationException(message);
            }

            int tempInt;
            if (int.TryParse(meter.Phase.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out tempInt))
            {
                gmoCisFile.Phase = tempInt;
            }

            if (int.TryParse(meter.Wire.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out tempInt))
            {
                gmoCisFile.Wires = tempInt;
            }

            decimal tempDecimal;
            if (decimal.TryParse(meter.KH, NumberStyles.Float, CultureInfo.InvariantCulture, out tempDecimal))
            {
                gmoCisFile.KH1 = tempDecimal;
                gmoCisFile.KH2 = tempDecimal;
            }

            IList<Reading> testReadings = this.WnpSystem.GetTestReading(device.EquipmentNumber, owner, deviceTest.TestDate, "KWH READING");
            if (testReadings.Count > 0)
            {
                if (testReadings.Count > 1)
                {
                    throw new InvalidOperationException("Can not prepare GMO CIS file export entry, because there is more than one KWH reading related to this test.");
                }

                Reading testReading = testReadings.First<Reading>();
                gmoCisFile.KWHReading = testReading.ReadingValue;
            }

            testReadings = this.WnpSystem.GetTestReading(device.EquipmentNumber, owner, deviceTest.TestDate, "AF DMD");
            if (testReadings.Count > 0)
            {
                if (testReadings.Count > 1)
                {
                    throw new InvalidOperationException("Can not prepare GMO CIS file export entry, because there is more than one AL DMD reading related to this test.");
                }

                Reading testReading = testReadings.First<Reading>();
                gmoCisFile.DemandReading = testReading.ReadingValue;
            }

            FileHelperEngine engine = new FileHelperEngine(typeof(GmoCisFile));
            return engine.WriteString(new GmoCisFile[] { gmoCisFile });
        }

        /// <summary>
        /// Prepares the electric meter test results for KCPL cis file.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="meter">The meter.</param>
        /// <returns>
        /// The electric meter test results as a string.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Meter can not be found in WNP.
        /// or
        /// Can not prepare GMO CIS file export entry, because there is more than one comment related to this test.
        /// or
        /// Can not prepare GMO CIS file export entry, because there is more than one reading related to this test.</exception>
        private string PrepareElectricMeterTestResultsForKcplCisFile(Device device, DeviceTest deviceTest, Meter meter)
        {
            int owner = int.Parse(device.Company.InternalCode, CultureInfo.InvariantCulture);

            IList<MeterTestResult> meterTestResults = this.WnpSystem.GetEquipmentTestResult<MeterTestResult>(device.EquipmentNumber, owner, deviceTest.TestDate);
            if (meterTestResults.Count == 0)
            {
                throw new InvalidOperationException("Meter test results can not be found in WNP.");
            }

            MeterTestResult meterTest = meterTestResults.First<MeterTestResult>();
            KcplCisFile kcplCisFile = new KcplCisFile()
            {
                TesterId = meterTest.TesterId.PadLeft(7, '0'),
                TestStartDate = meterTest.TestDate,
                TestEndTime = meterTest.TestDateStop,
                StationNumber = meterTest.StationId,
                TestStandard = meterTest.TestStandard,
                Location = meterTest.Location,
                TestCode = meterTest.TestReason,
                Manufacturer = meter.Manufacturer,
                CompanyCode = meter.MeterCode,
                MeterNumber = meter.SerialNumber,
                Form = meter.Form.PadLeft(2, '0'),
                Base = meter.Base.ToString(),
                AsFoundFullLoad = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "FL"),
                AsFoundLightLoad = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "LL"),
                AsFoundPowerFactor = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "PF"),
                AsLeftFullLoad = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "FL"),
                AsLeftLightLoad = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "LL"),
                AsLeftPowerFactor = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "PF"),
                Balance = meterTest.CustomField4,
                AsFoundDemand = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "DMD"),
                AsLeftDemand = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "DMD"),
                AsFoundWeightedAverage = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "WA"),
                AsLeftWeightedAverage = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "WA"),
                MeterStatus = meter.CustomField13,
                Comments = this.WnpSystem.GetTestCommentsConcatenated(device.EquipmentNumber, owner, device.EquipmentType.InternalCode, deviceTest.TestDate),
                AmrModuleNumber = meter.CustomField2 != null ? meter.CustomField2.PadLeft(10, '0') : string.Empty,
                RetireReason = meter.CustomField9,
                FirmwareRevision = meter.FirmwareRevision1,
                KYZPresent = meter.CustomField8,
                Company = "KCP&L",
            };

            if (meter.TestVolts.HasValue)
            {
                kcplCisFile.Volts = (int)Math.Ceiling(meter.TestVolts.Value);
            }

            decimal tempDecimal;
            if (decimal.TryParse(meter.KH, NumberStyles.Float, CultureInfo.InvariantCulture, out tempDecimal))
            {
                kcplCisFile.KH = tempDecimal;
            }

            IList<Reading> testReadings = this.WnpSystem.GetTestReading(device.EquipmentNumber, owner, deviceTest.TestDate, "KWH READING");
            if (testReadings.Count > 0)
            {
                if (testReadings.Count > 1)
                {
                    throw new InvalidOperationException("Can not prepare GMO CIS file export entry, because there is more than one AL DMD reading related to this test.");
                }

                Reading testReading = testReadings.First<Reading>();
                kcplCisFile.KWReading = testReading.ReadingValue.PadLeft(8, '0');
            }

            FileHelperEngine engine = new FileHelperEngine(typeof(KcplCisFile));
            return engine.WriteString(new KcplCisFile[] { kcplCisFile });
        }

        /// <summary>
        /// Prepares the electric meter test results for ODM.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="meter">The meter.</param>
        /// <returns>
        /// The electric meter test results as a string.
        /// </returns>
        private TestResultServiceRequest PrepareElectricMeterTestResultsForODM(Device device, DeviceTest deviceTest, Meter meter)
        {
            int owner = int.Parse(device.Company.InternalCode, CultureInfo.InvariantCulture);

            IList<MeterTestResult> meterTestResults = this.WnpSystem.GetEquipmentTestResult<MeterTestResult>(device.EquipmentNumber, owner, deviceTest.TestDate);
            if (meterTestResults.Count == 0)
            {
                throw new InvalidOperationException("Meter test results can not be found in WNP.");
            }

            MeterTestResult meterTest = meterTestResults.First<MeterTestResult>();
            TestResultServiceRequest serviceRequest = new TestResultServiceRequest()
            {
                badgeNo = GetMeterBadgeNumber(meter),
                testDateTime = meterTest.TestDate,
                testerId = meterTest.TesterId,
                testResults = new TestResultServiceRequestTestResults()
                {
                    asFound = new TestResultServiceRequestTestResultsAsFound()
                    {
                        fullLoad = Transformations.GetAsFound(meterTestResults, 'S', "FL").ToString(CultureInfo.InvariantCulture),
                        lightLoad = Transformations.GetAsFound(meterTestResults, 'S', "LL").ToString(CultureInfo.InvariantCulture),
                        weightedAverage = Transformations.GetAsFound(meterTestResults, 'S', "WA").ToString(CultureInfo.InvariantCulture)
                    },
                    asLeft = new TestResultServiceRequestTestResultsAsLeft()
                    {
                        fullLoad = Transformations.GetAsLeft(meterTestResults, 'S', "FL").ToString(CultureInfo.InvariantCulture),
                        lightLoad = Transformations.GetAsLeft(meterTestResults, 'S', "LL").ToString(CultureInfo.InvariantCulture),
                        weightedAverage = Transformations.GetAsLeft(meterTestResults, 'S', "WA").ToString(CultureInfo.InvariantCulture)
                    },
                    seriesPowerFactor = Transformations.GetAsLeft(meterTestResults, 'S', "PF").ToString(CultureInfo.InvariantCulture)
                }
            };
            
            switch (meterTest.Location)
            {
                case "FL":
                    serviceRequest.testLocation = TestResultServiceRequestTestLocation.FL;
                    break;
                case "MN":
                    serviceRequest.testLocation = TestResultServiceRequestTestLocation.MN;
                    break;
                case "SH":
                    serviceRequest.testLocation = TestResultServiceRequestTestLocation.SH;
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, "Test Location {0} is not supported by ODM.", meterTest.Location);
                    Log.Error(message);
                    throw new InvalidOperationException(message);
            }

            switch (meterTest.TestReason)
            {
                case "MT":
                    serviceRequest.testType = TestResultServiceRequestTestType.MT;
                    break;
                case "NS":
                    serviceRequest.testType = TestResultServiceRequestTestType.NS;
                    break;
                case "NT":
                    serviceRequest.testType = TestResultServiceRequestTestType.NT;
                    break;
                case "SS":
                    serviceRequest.testType = TestResultServiceRequestTestType.SS;
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, "Test Type {0} is not supported by ODM.", meterTest.TestReason);
                    Log.Error(message);
                    throw new InvalidOperationException(message);
            }

            IList<Reading> testReadings = this.WnpSystem.GetTestReading(device.EquipmentNumber, owner, deviceTest.TestDate);
            if (testReadings.Count > 0)
            {
                serviceRequest.testResults.meterReadsList = new TestResultServiceRequestTestResultsMeterReads[testReadings.Count];
                int i = 0;
                foreach (Reading testReading in testReadings)
                {
                    serviceRequest.testResults.meterReadsList[i] = new TestResultServiceRequestTestResultsMeterReads()
                    {
                        channel = testReading.ReadLabel,
                        reading = testReading.ReadingValue
                    };
                    i++;
                }
            }

            return serviceRequest;
        }

        /// <summary>
        /// Creates the device test.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="testDate">The test date.</param>
        /// <returns>The device test.</returns>
        private DeviceTest CreateDeviceTest(Device device, DateTime testDate)
        {
            DeviceTest test = new DeviceTest()
            {
                Device = device,
                TestDate = testDate
            };

            return this.DeviceManager.GetOrCreateDeviceTest(test);
        }

        /// <summary>
        /// Creates the device object.
        /// </summary>
        /// <param name="meter">The meter.</param>
        /// <returns>The device</returns>
        private Device CreateDevice(Meter meter)
        {
            Device device = new Device
            {
                Company = company,
                EquipmentNumber = meter.EquipmentNumber,
                EquipmentType = meterEquipmentType
            };

            return this.DeviceManager.GetOrCreateDevice(device);
        }
    }
}

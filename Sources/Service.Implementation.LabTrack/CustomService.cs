//-----------------------------------------------------------------------
// <copyright file="CustomService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.LabTrack
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP.Lookup;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Globalization;
    using FileHelpers;
    using log4net;

    /// <summary>
    /// Specific service implementation for LabTrack system.
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
        private static readonly ResourceManager CustomStringManager = new ResourceManager("AMSLLC.Listener.Service.Implementation.LabTrack.Properties.Resources", Assembly.GetExecutingAssembly());

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

            Log.Info("LabTrack Send Test Data");

            Device device = deviceTest.Device;
            Meter meter = this.WnpSystem.GetEquipment<Meter>(device.EquipmentNumber, device.Company.Id);
            if (meter == null)
            {
                throw new InvalidOperationException("Meter can not be found in WNP.");
            }

            string message;
            switch (device.EquipmentType.ServiceType.InternalCode)
            {
                case "E":
                    switch (device.EquipmentType.InternalCode)
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

            TransactionLog transaction = this.TransactionLogManager.GetTransaction(transactionId);
            Company company = this.DeviceManager.GetCompanyByInternalCode("0".ToString(CultureInfo.InvariantCulture));

            string message;
            switch (batch.EquipmentType)
            {
                case "EM":
                    TransactionHelper.ProcessMetersBatch(batch, transaction.DeviceBatch, company, this.ProcessMeter, this.ProcessMeterTestResults);
                    break;
                default:
                    message = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("BatchDeviceTypeNotSupported", CultureInfo.CurrentCulture), batch.EquipmentType);
                    Log.Error(message);
                    throw new ArgumentException(message);
            }
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
            string message = CustomStringManager.GetString("NotSupportedByLabTrack", CultureInfo.CurrentCulture);
            Log.Info(message);
            this.TransactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Skipped, message, null);
            return;
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
            if (TransactionHelper.SkipNewBatchTransaction(meter, transactionId) ||
                TransactionHelper.SkipDuplicateTransaction(deviceTest, transactionId, null))
            {
                return;
            }

            TransactionLog currentTransaction = this.TransactionLogManager.GetTransaction(transactionId);

            switch (currentTransaction.TransactionType.ExternalSystem.Name)
            {
                case "LabTrack":
                    string labTrackEntry = this.PrepareElectricMeterTestResultsForLabTrack(device, deviceTest, meter);
                    Utilities.AppendLineToFile(labTrackEntry, ConfigurationManager.AppSettings["ExportFileLocation.LabTrack"]);
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("ExternalSystemNotSupported", CultureInfo.CurrentCulture), currentTransaction.TransactionType.ExternalSystem.Name);
                    Log.Error(message);
                    throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Prepares the electric meter test results for lab track.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="meter">The meter.</param>
        /// <returns>
        /// The electric meter test results as a string.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Meter test results can not be found in WNP.
        /// or
        /// Can not prepare LabTrack file export entry, because there is more than one KWH reading related to this test.
        /// </exception>
        /// <exception cref="System.NotImplementedException"></exception>
        private string PrepareElectricMeterTestResultsForLabTrack(Device device, DeviceTest deviceTest, Meter meter)
        {
            int owner = int.Parse(device.Company.InternalCode, CultureInfo.InvariantCulture);

            IList<MeterTestResult> meterTestResults = this.WnpSystem.GetEquipmentTestResult<MeterTestResult>(device.EquipmentNumber, owner, deviceTest.TestDate);
            if (meterTestResults.Count == 0)
            {
                throw new InvalidOperationException("Meter test results can not be found in WNP.");
            }

            MeterTestResult meterTest = meterTestResults.First<MeterTestResult>();

            LabTrackFileFormat labTrackEntry = new LabTrackFileFormat()
            {
                StockHeaderCompanyCode = device.Company.ExternalCode,
                StockHeaderDeviceTypeCode = device.EquipmentType.ExternalCode,
                StockHeaderManufacturerCode = meter.Manufacturer,
                StockHeaderDeviceId = meter.EquipmentNumber,
                StockHeaderAlternativeDeviceId = meter.SerialNumber,
                StockHeaderPurchaseOrderGroupNumber = meter.PurchaseOrderReference,
                StockHeaderCurrentLocationCode = meter.Location,
                StockHeaderHoldDevice = 'N',
                StockHeaderLimits = 'Y',
                StockHeaderInventory = 'N',
                StockHeaderChangeDate = meter.EquipmentStatusDate,
                StockHeaderEventTrigger1 = 0,
                StockHeaderEventTrigger2 = 0,
                
                MeterHeaderTypeCode = meter.ModelNumber,
                MeterHeaderSetupCode = meter.AepCode,
                MeterHeaderKwhDials = meter.KwhDials,
                MeterHeaderKWDials = meter.KWDials.HasValue ? meter.KWDials.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
                MeterHeaderKwhMultiplier = meter.EnergyMultiplier.HasValue ? meter.EnergyMultiplier.Value : 0m,
                MeterHeaderKWMultiplier = meter.DemandMultiplier.HasValue ? meter.DemandMultiplier.Value : 0m,

                MeterSetupBase = meter.Base,
                MeterSetupTestVoltage = meter.TestVolts,
                MeterSetupTestCurrent = meter.TestAmps,
                MeterSetupPhase = meter.Phase,
                MeterSetupWire = meter.Wire,
                MeterSetupRegisterRatio = meter.RegisterRatio,

                ShopTestHistoryAsFoundTestDate = meterTest.TestDate,
                ShopTestHistoryAsFoundTestTime = meterTest.TestDate,
                ShopTestHistoryAsFoundTesterId = meterTest.TesterId,
                ShopTestHistoryAsFoundBoardNumber = int.Parse(meterTest.WecoSerialNumber, CultureInfo.InvariantCulture),
                ShopTestHistoryAsFoundSeriesFull = Transformations.GetAsFound(meterTestResults, 'S', "FL"),
                ShopTestHistoryAsFoundSeriesPower = Transformations.GetAsFound(meterTestResults, 'S', "PF"),
                ShopTestHistoryAsFoundSeriesLight = Transformations.GetAsFound(meterTestResults, 'S', "LL"),
                ShopTestHistoryAsFoundElementAFull = Transformations.GetAsFound(meterTestResults, 'A', "FL"),
                ShopTestHistoryAsFoundElementAPower = Transformations.GetAsFound(meterTestResults, 'A', "PF"),
                ShopTestHistoryAsFoundElementALight = Transformations.GetAsFound(meterTestResults, 'A', "LL"),
                ShopTestHistoryAsFoundElementBFull = Transformations.GetAsFound(meterTestResults, 'B', "FL"),
                ShopTestHistoryAsFoundElementBPower = Transformations.GetAsFound(meterTestResults, 'B', "PF"),
                ShopTestHistoryAsFoundElementBLight = Transformations.GetAsFound(meterTestResults, 'B', "LL"),
                ShopTestHistoryAsFoundElementCFull = Transformations.GetAsFound(meterTestResults, 'C', "FL"),
                ShopTestHistoryAsFoundElementCPower = Transformations.GetAsFound(meterTestResults, 'C', "PF"),
                ShopTestHistoryAsFoundElementCLight = Transformations.GetAsFound(meterTestResults, 'C', "LL"),
                ShopTestHistoryAsLeftTestDate = meterTest.TestDate,
                ShopTestHistoryAsLeftTestTime = meterTest.TestDate,
                ShopTestHistoryAsLeftTesterId = meterTest.TesterId,
                ShopTestHistoryAsLeftSeriesFull = Transformations.GetAsLeft(meterTestResults, 'S', "FL"),
                ShopTestHistoryAsLeftSeriesPower = Transformations.GetAsLeft(meterTestResults, 'S', "PF"),
                ShopTestHistoryAsLeftSeriesLight = Transformations.GetAsLeft(meterTestResults, 'S', "LL"),
                ShopTestHistoryAsLeftElementAFull = Transformations.GetAsLeft(meterTestResults, 'A', "FL"),
                ShopTestHistoryAsLeftElementAPower = Transformations.GetAsLeft(meterTestResults, 'A', "PF"),
                ShopTestHistoryAsLeftElementALight = Transformations.GetAsLeft(meterTestResults, 'A', "LL"),
                ShopTestHistoryAsLeftElementBFull = Transformations.GetAsLeft(meterTestResults, 'B', "FL"),
                ShopTestHistoryAsLeftElementBPower = Transformations.GetAsLeft(meterTestResults, 'B', "PF"),
                ShopTestHistoryAsLeftElementBLight = Transformations.GetAsLeft(meterTestResults, 'B', "LL"),
                ShopTestHistoryAsLeftElementCFull = Transformations.GetAsLeft(meterTestResults, 'C', "FL"),
                ShopTestHistoryAsLeftElementCPower = Transformations.GetAsLeft(meterTestResults, 'C', "PF"),
                ShopTestHistoryAsLeftElementCLight = Transformations.GetAsLeft(meterTestResults, 'C', "LL"),
                ShopTestHistoryComment = this.WnpSystem.GetTestCommentsConcatenated(device.EquipmentNumber, owner, device.EquipmentType.InternalCode, deviceTest.TestDate),
                ShopTestHistoryCreepPass = 'N',

                AsFoundWeightedAverage = Transformations.GetAsFound(meterTestResults, 'S', "WA"),
                AsLeftWeightedAverage = Transformations.GetAsLeft(meterTestResults, 'S', "WA")
            };

            IList<Reading> testReadings = this.WnpSystem.GetTestReading(device.EquipmentNumber, owner, deviceTest.TestDate, "KWH READING");
            if (testReadings.Count > 0)
            {
                if (testReadings.Count > 1)
                {
                    throw new InvalidOperationException("Can not prepare LabTrack file export entry, because there is more than one KWH reading related to this test.");
                }

                Reading testReading = testReadings.First<Reading>();
                labTrackEntry.ShopTestHistoryAsFoundDialReading = testReading.ReadingValue;
            }

            int tempInt;
            if (int.TryParse(meter.ShopStatus, out tempInt))
            {
                labTrackEntry.StockHeaderStatusCode = tempInt;
            }

            if (int.TryParse(meter.Form, out tempInt))
            {
                labTrackEntry.MeterSetupForm = tempInt;
            }

            decimal tempDecimal;
            if (decimal.TryParse(meter.KH, out tempDecimal))
            {
                labTrackEntry.MeterSetupDiskConstant = tempDecimal;
            }
            else
            {
                labTrackEntry.MeterSetupDiskConstant = 0m;
            }
                        
            // check if there are test steps that didn't pass
            IList<MeterTestResult> failedTests = meterTestResults.Where(item => item.AccuracyStatus != 'P').ToList();
            if (failedTests.Count() > 0)
            {
                labTrackEntry.ShopTestHistoryTestInLimits = 'N';
            }
            else
            {
                labTrackEntry.ShopTestHistoryTestInLimits = 'Y';
            }

            FileHelperEngine engine = new FileHelperEngine(typeof(LabTrackFileFormat));
            return engine.WriteString(new LabTrackFileFormat[] { labTrackEntry });
        }
    }
}

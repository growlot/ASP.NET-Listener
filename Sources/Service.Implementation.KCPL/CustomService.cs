﻿//-----------------------------------------------------------------------
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
                            this.ProcessMeterTestResults(device, deviceTest, meter, request.TransactionId);
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
        /// Processes the test results of electric meter.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="meter">The meter.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <exception cref="System.ArgumentException">Throws exception if device company is not supported.</exception>
        private void ProcessMeterTestResults(Device device, DeviceTest deviceTest, Meter meter, int transactionId)
        {
            switch (meter.CustomField1)
            {
                case "KCPL":
                    this.ProcessKcplTestResults(device, deviceTest, meter, transactionId);
                    break;
                case "MPS":
                case "SJLP":
                    this.ProcessGmoTestResults(device, deviceTest, meter, transactionId);
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, StringManager.GetString("CompanyNotSupported", CultureInfo.CurrentCulture), device.Company.Name);
                    Log.Error(message);
                    throw new ArgumentException(message);
            }
        }
        
        /// <summary>
        /// Processes the KCPL test results.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="meter">The meter.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        private void ProcessKcplTestResults(Device device, DeviceTest deviceTest, Meter meter, int transactionId)
        {
            TransactionLog transactionLog = this.TransactionLogManager.GetTransaction(transactionId);
            
            if (transactionLog.TransactionType.ExternalSystem.Name == "CIS")
            {
                string kcplCisEntry = this.PrepareElectricMeterTestResultsForKcplCisFile(device, deviceTest, meter);
                SaveResultsToFile(kcplCisEntry, ConfigurationManager.AppSettings["ExportFileLocation.KcplCis"]);
            }
            else
            {
                TestResultServiceRequest serviceRequest = this.PrepareElectricMeterTestResultsForODM(device, deviceTest, meter);
                serviceRequest.listenerTransactionId = transactionId.ToString(CultureInfo.InvariantCulture);
                Uri address = new Uri(ConfigurationManager.AppSettings["Kcpl.AssetTestResult.Url"]);

                try
                {
                    this.TransactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ServiceSendMessage);
                    MessageBasedSoapWebService.CallWebService<TestResultServiceRequest>(address, serviceRequest);
                }
                catch (Exception ex)
                {
                    string message = StringManager.GetString("ServiceCallFailed", CultureInfo.CurrentCulture);
                    Log.Error(message, ex);
                    this.TransactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Failed, message, ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>
        /// Processes the GMO test results.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="meter">The meter.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        private void ProcessGmoTestResults(Device device, DeviceTest deviceTest, Meter meter, int transactionId)
        {
            TransactionLog transactionLog = this.TransactionLogManager.GetTransaction(transactionId);

            if (transactionLog.TransactionType.ExternalSystem.Name == "CIS")
            {
                string gmoCisEntry = this.PrepareElectricMeterTestResultsForGmoCisFile(device, deviceTest, meter);
                SaveResultsToFile(gmoCisEntry, ConfigurationManager.AppSettings["ExportFileLocation.GmoCis"]);
            }
            else
            {
                string message = CustomStringManager.GetString("SkipGMODevice", CultureInfo.CurrentCulture);
                Log.Info(message);
                this.TransactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Skipped, message, null);
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
                DemandInterval2 = "M",
                MeterConstant = 1,
                BoardId = meterTest.WecoSerialNumber,
                CommentsPrefix = meter.CustomField1 == "MPS" ? "MPS" : "SJ ",
                Comments = this.WnpSystem.GetTestCommentsConcatenated(device.EquipmentNumber, owner, device.EquipmentType.InternalCode, deviceTest.TestDate),
                CommentDate = meterTest.TestDate,
                Codes = "RSN TEST 09 REPR CODE 08",
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
                Volts = (int)Math.Ceiling(meter.TestVolts),
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
        /// <exception cref="System.InvalidOperationException">Meter can not be found in WNP.
        /// or
        /// Can not prepare GMO CIS file export entry, because there is more than one comment related to this test.
        /// or
        /// Can not prepare GMO CIS file export entry, because there is more than one reading related to this test.</exception>
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
                badgeNo = meter.EquipmentNumber,
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
                    string message = string.Format(CultureInfo.InvariantCulture, "Test Type {0} is not supported by ODM.", meterTest.Location);
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
    }
}

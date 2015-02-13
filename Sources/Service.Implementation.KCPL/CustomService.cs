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
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Globalization;
    using AMSLLC.Listener.Service.Contract;
    using AMSLLC.Listener.Service.Implementation;
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
        private ResourceManager stringManager = Init.StringManager;

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
                                    message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("CompanyNotSupported", CultureInfo.CurrentCulture), device.Company.Name);
                                    Log.Error(message);
                                    throw new ArgumentException(message);
                            }

                            break;
                        default:
                            message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("DeviceTypeNotSupported", CultureInfo.CurrentCulture), device.EquipmentType.Description, device.EquipmentType.ServiceType.Description);
                            Log.Error(message);
                            throw new ArgumentException(message);
                    }

                    break;
                default:
                    string message1 = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("ServiceTypeNotSupported", CultureInfo.CurrentCulture), device.EquipmentType.ServiceType.Description);
                    Log.Error(message1);
                    throw new ArgumentException(message1);
            } 
            
            Log.Info("KCPL Send Test Data");
            this.TransactionLogManager.UpdateTransactionState(request.TransactionId, TransactionStateLookup.ServiceSendMessage);
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

            switch (meter.CustomField15)
            {
                case "A":
                    gmoCisFile.MeterStatus = "01";
                    break;
                case "R":
                    gmoCisFile.MeterStatus = "08";
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, "Meter status {0} is not supported by CIS.", meter.CustomField15);
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
    }
}

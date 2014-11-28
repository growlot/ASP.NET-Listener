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
                                    SaveResultsToFile(kcplCisEntry, ConfigurationManager.AppSettings["ExportFileLocation.KcplCis"], "Kcpl.txt");
                                    break;
                                case "MPS":
                                case "SJLP":
                                    string gmoCisEntry = this.PrepareElectricMeterTestResultsForGmoCisFile(device, deviceTest, meter);
                                    SaveResultsToFile(gmoCisEntry, ConfigurationManager.AppSettings["ExportFileLocation.GmoCis"], "Gmo.txt");
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
        /// <param name="fileLocation">The file location.</param>
        /// <param name="fileName">Name of the file.</param>
        private static void SaveResultsToFile(string results, string fileLocation, string fileName)
        {
            string exportFile = Path.Combine(fileLocation, fileName);
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
                Amps = (decimal)meter.TestAmps,
                AsFoundAFullLoad = (decimal)Transformations.GetAsFound(meterTestResults, 'A', "FL"),
                AsFoundBFullLoad = (decimal)Transformations.GetAsFound(meterTestResults, 'B', "FL"),
                AsFoundCFullLoad = (decimal)Transformations.GetAsFound(meterTestResults, 'C', "FL"),
                AsFoundFullLoad = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "FL"),
                AsFoundLightLoad = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "LL"),
                AsFoundPowerFactor = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "PF"),
                AsFoundWeightedAverage = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "WA"),
                AsLeftAFullLoad = (decimal)Transformations.GetAsLeft(meterTestResults, 'A', "FL"),
                AsLeftBFullLoad = (decimal)Transformations.GetAsLeft(meterTestResults, 'B', "FL"),
                AsLeftCFullLoad = (decimal)Transformations.GetAsLeft(meterTestResults, 'C', "FL"),
                AsLeftFullLoad = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "FL"),
                AsLeftLightLoad = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "LL"),
                AsLeftPowerFactor = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "PF"),
                AsLeftWeightedAverage = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "WA"),
                BarcodeId = meterTest.TestStandard,
                Base = meter.Base.ToString(),
                Class = meter.CustomField3,
                Comments = this.WnpSystem.GetTestCommentsConcatenated(device.EquipmentNumber, owner, device.EquipmentType.InternalCode, deviceTest.TestDate),
                DateTested = meterTest.TestDate,
                Form = meter.Form,
                Manufacturer = meter.CustomField16,
                MeterSerialNumber = meter.SerialNumber,
                MeterType = meter.ModelNumber,
                NWH = "NWH",
                RegisterRatio = meter.RegisterRatio,
                RegisterType = meter.CustomField7,
                RepairCode = meterTest.CustomField1,
                TesterId = meterTest.TesterId,
                TestType = meterTest.TestReason,
                Volts = (decimal)meter.TestVolts
            };
            
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
                gmoCisFile.KH = tempDecimal;
            }

            IList<Reading> testReadings = this.WnpSystem.GetTestReading(device.EquipmentNumber, owner, deviceTest.TestDate, "KWH READING");
            if (testReadings.Count > 0)
            {
                if (testReadings.Count > 1)
                {
                    throw new InvalidOperationException("Can not prepare GMO CIS file export entry, because there is more than one reading related to this test.");
                }

                Reading testReading = testReadings.First<Reading>();
                gmoCisFile.AsFoundRead = testReading.ReadingValue;
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
                AmrModuleNumber = meter.AmiId1,
                AsFoundDemand = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "DMD"),
                AsFoundFullLoad = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "FL"),
                AsFoundLightLoad = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "LL"),
                AsFoundPowerFactor = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "PF"),
                AsFoundWeightedAverage = (decimal)Transformations.GetAsFound(meterTestResults, 'S', "WA"),
                AsLeftDemand = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "DMD"),
                AsLeftFullLoad = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "FL"),
                AsLeftLightLoad = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "LL"),
                AsLeftPowerFactor = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "PF"),
                AsLeftWeightedAverage = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "WA"),
                Balance = meterTest.CustomField4,
                Base = meter.Base.ToString(),
                Comments = this.WnpSystem.GetTestCommentsConcatenated(device.EquipmentNumber, owner, device.EquipmentType.InternalCode, deviceTest.TestDate),
                Company = "KCP&L",
                CompanyCode = meter.MeterCode,
                FirmwareRevision = meter.FirmwareRevision1,
                Form = meter.Form.PadLeft(2, '0'),
                KYZPresent = meter.CustomField8,
                Location = meterTest.Location,
                Manufacturer = meter.Manufacturer,
                MeterNumber = meter.SerialNumber,
                MeterStatus = meter.CustomField13,
                ProgramCode = meter.ProgramId,
                RepairCode1 = meterTest.CustomField1,
                RepairCode2 = meterTest.CustomField2,
                RepairCode3 = meterTest.CustomField3,
                StationNumber = meterTest.StationId,
                TestCode = meterTest.TestReason,
                TestEndTime = meterTest.TestDateStop,
                TesterId = meterTest.TesterId.PadLeft(7, '0'),
                TestStartDate = meterTest.TestDate,
                Volts = (int)Math.Ceiling(meter.TestVolts)
            };

            decimal tempDecimal;
            if (decimal.TryParse(meter.KH, NumberStyles.Float, CultureInfo.InvariantCulture, out tempDecimal))
            {
                kcplCisFile.KH = tempDecimal;
            }

            IList<Reading> testReadings = this.WnpSystem.GetTestReading(device.EquipmentNumber, owner, deviceTest.TestDate, "AL DMD");
            if (testReadings.Count > 0)
            {
                if (testReadings.Count > 1)
                {
                    throw new InvalidOperationException("Can not prepare GMO CIS file export entry, because there is more than one reading related to this test.");
                }

                Reading testReading = testReadings.First<Reading>();
                kcplCisFile.KWReading = testReading.ReadingValue.PadLeft(8, '0');
            }

            FileHelperEngine engine = new FileHelperEngine(typeof(KcplCisFile));
            return engine.WriteString(new KcplCisFile[] { kcplCisFile });
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="RequestConstructor.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.Alliant
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Service.Implementation.Alliant.GetDevice;
    using AMSLLC.Listener.Service.Implementation.Alliant.SendTestResult;

    /// <summary>
    /// Construct CC&amp;B web service requests.
    /// </summary>
    public class RequestConstructor
    {
        /// <summary>
        /// The WNP system
        /// </summary>
        /// <value>
        /// The WNP system.
        /// </value>
        private WNPSystem wnpSystem;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestConstructor"/> class.
        /// </summary>
        /// <param name="wnpSystem">The WNP system.</param>
        public RequestConstructor(WNPSystem wnpSystem)
        {
            this.wnpSystem = wnpSystem;
        }

        /// <summary>
        /// Prepares the electric meter test results request.
        /// </summary>
        /// <param name="deviceTest">The device test.</param>
        /// <returns>The alliant request.</returns>
        /// <exception cref="System.InvalidOperationException">Meter can not be found in WNP.</exception>
        public CreateDeviceTestResultABMType PrepareElectricMeterTestResultsRequest(DeviceTest deviceTest)
        {
            if (deviceTest == null)
            {
                throw new ArgumentNullException("deviceTest", "Can not prepare electric meter test results request if device test is not specified");
            }

            Device device = deviceTest.Device;

            CreateDeviceTestResultABMType alliantRequest;
            int owner = int.Parse(device.Company.InternalCode, CultureInfo.InvariantCulture);
            Meter meter = this.wnpSystem.GetEquipment<Meter>(device.EquipmentNumber, owner);
            if (meter == null)
            {
                throw new InvalidOperationException("Meter can not be found in WNP.");
            }

            IList<MeterTestResult> meterTestResults = this.wnpSystem.GetEquipmentTestResult<MeterTestResult>(device.EquipmentNumber, owner, deviceTest.TestDate);
            if (meterTestResults.Count == 0)
            {
                throw new InvalidOperationException("Meter test results can not be found in WNP.");
            }

            MeterTestResult meterTest = meterTestResults.First<MeterTestResult>();

            alliantRequest = new CreateDeviceTestResultABMType()
            {
                ClassificationCode = meter.MeterCode,
                Comments = this.wnpSystem.GetTestCommentsConcatenated(device.EquipmentNumber, owner, device.EquipmentType.InternalCode, deviceTest.TestDate),
                Company = device.Company.ExternalCode,
                DeviceAttribute = new CreateDeviceTestResultABMTypeDeviceAttribute()
                {
                    ElectricDevice = new CreateDeviceTestResultABMTypeDeviceAttributeElectricDevice()
                    {
                        AFL = (decimal)Transformations.GetAsLeft(meterTestResults, 'A', "FL"),
                        APF = (decimal)Transformations.GetAsLeft(meterTestResults, 'A', "PF"),
                        BFL = (decimal)Transformations.GetAsLeft(meterTestResults, 'B', "FL"),
                        BPF = (decimal)Transformations.GetAsLeft(meterTestResults, 'B', "PF"),
                        CFL = (decimal)Transformations.GetAsLeft(meterTestResults, 'C', "FL"),
                        CPF = (decimal)Transformations.GetAsLeft(meterTestResults, 'C', "PF"),
                        CreepCode = meterTest.CustomField6,
                        SFL = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "FL"),
                        SLL = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "LL"),
                        SPF = (decimal)Transformations.GetAsLeft(meterTestResults, 'S', "PF")
                    }
                },
                DeviceNumber = meter.EquipmentNumber,
                DeviceType = device.EquipmentType.ExternalCode,
                NewDeviceIndicator = meter.CustomField3,
                RepairedBy = meterTest.CustomField2,
                RepairType = meterTest.CustomField4,
                RetirementReason = meterTest.CustomField5,
                ServiceType = device.EquipmentType.ServiceType.ExternalCode,
                Tester = meterTest.TesterId,
                TestLocation = meterTest.Location,
                TestReason = meterTest.CustomField1,
                TestStandard = meterTest.TestStandard,
                TestStartDateTime = meterTest.TestDate
            };
            alliantRequest.DeviceAttribute.ElectricDevice.AFLSpecified = alliantRequest.DeviceAttribute.ElectricDevice.AFL == 0 ? false : true;
            alliantRequest.DeviceAttribute.ElectricDevice.APFSpecified = alliantRequest.DeviceAttribute.ElectricDevice.APF == 0 ? false : true;
            alliantRequest.DeviceAttribute.ElectricDevice.BFLSpecified = alliantRequest.DeviceAttribute.ElectricDevice.BFL == 0 ? false : true;
            alliantRequest.DeviceAttribute.ElectricDevice.BPFSpecified = alliantRequest.DeviceAttribute.ElectricDevice.BPF == 0 ? false : true;
            alliantRequest.DeviceAttribute.ElectricDevice.CFLSpecified = alliantRequest.DeviceAttribute.ElectricDevice.CFL == 0 ? false : true;
            alliantRequest.DeviceAttribute.ElectricDevice.CPFSpecified = alliantRequest.DeviceAttribute.ElectricDevice.CPF == 0 ? false : true;
            alliantRequest.DeviceAttribute.ElectricDevice.SFLSpecified = alliantRequest.DeviceAttribute.ElectricDevice.SFL == 0 ? false : true;
            alliantRequest.DeviceAttribute.ElectricDevice.SLLSpecified = alliantRequest.DeviceAttribute.ElectricDevice.SLL == 0 ? false : true;
            alliantRequest.DeviceAttribute.ElectricDevice.SPFSpecified = alliantRequest.DeviceAttribute.ElectricDevice.SPF == 0 ? false : true;

            int tempInt;
            if (int.TryParse(meterTest.CustomField7, out tempInt))
            {
                alliantRequest.DeviceAttribute.ElectricDevice.ConditionCodeSpecified = true;
                alliantRequest.DeviceAttribute.ElectricDevice.ConditionCode = tempInt;
            }

            DateTime tempDate;
            if (DateTime.TryParse(meterTest.CustomField3, out tempDate))
            {
                alliantRequest.RepairDateTimeSpecified = true;
                alliantRequest.RepairDateTime = tempDate;
            }

            return alliantRequest;
        }

        /// <summary>
        /// Prepares the current transformer test results request.
        /// </summary>
        /// <param name="deviceTest">The device test.</param>
        /// <returns>The alliant request.</returns>
        /// <exception cref="System.InvalidOperationException">Current transformer can not be found in WNP.</exception>
        public CreateDeviceTestResultABMType PrepareCurrentTransformerTestResultsRequest(DeviceTest deviceTest)
        {
            if (deviceTest == null)
            {
                throw new ArgumentNullException("deviceTest", "Can not prepare current transformer test results request if device test is not specified");
            }

            Device device = deviceTest.Device;

            CreateDeviceTestResultABMType alliantRequest;
            int owner = int.Parse(device.Company.InternalCode, CultureInfo.InvariantCulture);
            CurrentTransformer ct = this.wnpSystem.GetEquipment<CurrentTransformer>(device.EquipmentNumber, owner);
            if (ct == null)
            {
                throw new InvalidOperationException("Current transformer can not be found in WNP.");
            }

            IList<CurrentTransformerTestResult> ctTestResults = this.wnpSystem.GetEquipmentTestResult<CurrentTransformerTestResult>(device.EquipmentNumber, owner, deviceTest.TestDate);
            if (ctTestResults.Count == 0)
            {
                throw new InvalidOperationException("Current transformer test results can not be found in WNP.");
            }

            CurrentTransformerTestResult ctTestFullLoad = ctTestResults.Single<CurrentTransformerTestResult>(e => e.LoadLabel == "FL");
            CurrentTransformerTestResult ctTestLightLoad = ctTestResults.Single<CurrentTransformerTestResult>(e => e.LoadLabel == "LL");

            alliantRequest = new CreateDeviceTestResultABMType()
            {
                ClassificationCode = ct.TransformerCode,
                Comments = this.wnpSystem.GetTestCommentsConcatenated(device.EquipmentNumber, owner, device.EquipmentType.InternalCode, deviceTest.TestDate),
                Company = device.Company.ExternalCode,
                DeviceNumber = ct.EquipmentNumber,
                DeviceType = device.EquipmentType.ExternalCode,
                NewDeviceIndicator = ct.CustomField3,
                RepairedBy = ctTestFullLoad.CustomField2,
                RepairType = ctTestFullLoad.CustomField4,
                RetirementReason = ctTestFullLoad.CustomField5,
                ServiceType = device.EquipmentType.ServiceType.ExternalCode,
                Tester = ctTestFullLoad.TesterId,
                TestLocation = ctTestFullLoad.Location,
                TestReason = ctTestFullLoad.PrimaryTestReason,
                TestStandard = ctTestFullLoad.CustomField1,
                TestStartDateTime = ctTestFullLoad.TestDate,
                TransformerAttribute = new CreateDeviceTestResultABMTypeTransformerAttribute()
                {
                    AccuracyClassFL = (decimal)ctTestFullLoad.AccuracyClass,
                    AccuracyClassFLSpecified = true,
                    AccuracyClassLL = (decimal)ctTestLightLoad.AccuracyClass,
                    AccuracyClassLLSpecified = true,
                    HighBurdenFLAngle = (decimal)ctTestFullLoad.PhaseError,
                    HighBurdenFLAngleSpecified = true,
                    HighBurdenFLRatio = (decimal)ctTestFullLoad.RatioCorrection,
                    HighBurdenFLRatioSpecified = true,
                    HighBurdenLLAngle = (decimal)ctTestLightLoad.PhaseError,
                    HighBurdenLLAngleSpecified = true,
                    HighBurdenLLRatio = (decimal)ctTestLightLoad.RatioCorrection,
                    HighBurdenLLRatioSpecified = true,
                    RatioTested = ctTestFullLoad.SelectedRatio,
                    InsulationTestPassOrFail = ctTestFullLoad.CustomField7,
                    TestAmpsFL = (decimal)ctTestFullLoad.SecondaryCurrent,
                    TestAmpsFLSpecified = true,
                    TestAmpsLL = (decimal)ctTestLightLoad.SecondaryCurrent,
                    TestAmpsLLSpecified = true,
                    TestBurden = ctTestFullLoad.Burden.ToString(CultureInfo.InvariantCulture),
                    TestVoltsFLSpecified = false,
                    VoltageForInsulationTest = ctTestFullLoad.CustomField6
                }
            };

            DateTime tempDate;
            if (DateTime.TryParse(ctTestFullLoad.CustomField3, out tempDate))
            {
                alliantRequest.RepairDateTimeSpecified = true;
                alliantRequest.RepairDateTime = tempDate;
            }

            return alliantRequest;
        }

        /// <summary>
        /// Prepares the potential transformer test results request.
        /// </summary>
        /// <param name="deviceTest">The device test.</param>
        /// <returns>The alliant request.</returns>
        /// <exception cref="System.InvalidOperationException">Potential transformer can not be found in WNP.</exception>
        public CreateDeviceTestResultABMType PreparePotentialTransformerTestResultsRequest(DeviceTest deviceTest)
        {
            if (deviceTest == null)
            {
                throw new ArgumentNullException("deviceTest", "Can not prepare potential transformer test results request if device test is not specified");
            }

            Device device = deviceTest.Device;

            CreateDeviceTestResultABMType alliantRequest;
            int owner = int.Parse(device.Company.InternalCode, CultureInfo.InvariantCulture);
            PotentialTransformer pt = this.wnpSystem.GetEquipment<PotentialTransformer>(device.EquipmentNumber, owner);
            if (pt == null)
            {
                throw new InvalidOperationException("Potential transformer can not be found in WNP.");
            }

            IList<PotentialTransformerTestResult> ptTestResults = this.wnpSystem.GetEquipmentTestResult<PotentialTransformerTestResult>(device.EquipmentNumber, owner, deviceTest.TestDate);
            if (ptTestResults.Count == 0)
            {
                throw new InvalidOperationException("Potential transformer test results can not be found in WNP.");
            }

            PotentialTransformerTestResult ptTestFullLoad = ptTestResults.Single<PotentialTransformerTestResult>(e => e.LoadLabel == "FL");

            alliantRequest = new CreateDeviceTestResultABMType()
            {
                ClassificationCode = pt.TransformerCode,
                Comments = this.wnpSystem.GetTestCommentsConcatenated(device.EquipmentNumber, owner, device.EquipmentType.InternalCode, deviceTest.TestDate),
                Company = device.Company.ExternalCode,
                DeviceNumber = pt.EquipmentNumber,
                DeviceType = device.EquipmentType.ExternalCode,
                NewDeviceIndicator = pt.CustomField3,
                RepairedBy = ptTestFullLoad.CustomField2,
                RepairType = ptTestFullLoad.CustomField4,
                RetirementReason = ptTestFullLoad.CustomField5,
                ServiceType = device.EquipmentType.ServiceType.ExternalCode,
                Tester = ptTestFullLoad.TesterId,
                TestLocation = ptTestFullLoad.Location,
                TestReason = ptTestFullLoad.PrimaryTestReason,
                TestStandard = ptTestFullLoad.CustomField1,
                TestStartDateTime = ptTestFullLoad.TestDate,
                TransformerAttribute = new CreateDeviceTestResultABMTypeTransformerAttribute()
                {
                    AccuracyClassFL = (decimal)ptTestFullLoad.AccuracyClass,
                    AccuracyClassFLSpecified = true,
                    AccuracyClassLLSpecified = false,
                    HighBurdenFLAngle = (decimal)ptTestFullLoad.PhaseError,
                    HighBurdenFLAngleSpecified = true,
                    HighBurdenFLRatio = (decimal)ptTestFullLoad.RatioCorrection,
                    HighBurdenFLRatioSpecified = true,
                    HighBurdenLLAngleSpecified = false,
                    HighBurdenLLRatioSpecified = false,
                    RatioTested = ptTestFullLoad.SelectedRatio,
                    InsulationTestPassOrFail = ptTestFullLoad.CustomField7,
                    TestAmpsFLSpecified = false,
                    TestAmpsLLSpecified = false,
                    TestBurden = ptTestFullLoad.Burden,
                    TestVoltsFL = (decimal)ptTestFullLoad.SecondaryVoltage,
                    TestVoltsFLSpecified = true,
                    VoltageForInsulationTest = ptTestFullLoad.CustomField6
                }
            };

            DateTime tempDate;
            if (DateTime.TryParse(ptTestFullLoad.CustomField3, out tempDate))
            {
                alliantRequest.RepairDateTimeSpecified = true;
                alliantRequest.RepairDateTime = tempDate;
            }

            return alliantRequest;
        }
    }
}

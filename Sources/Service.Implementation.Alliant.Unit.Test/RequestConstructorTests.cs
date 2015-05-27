//-----------------------------------------------------------------------
// <copyright file="RequestConstructorTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Service.Implementation.Alliant.Unit.Test
{
    using System;
    using System.Collections.Generic;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.WNP;
    using AMSLLC.Listener.Common.WNP.Unit.Test;
    using AMSLLC.Listener.Service.Implementation.Alliant;
    using AMSLLC.Listener.Service.Implementation.Alliant.GetDevice;
    using AMSLLC.Listener.Service.Implementation.Alliant.SendTestResult;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ListenerModel = AMSLLC.Listener.Common.Model;
    using WnpModel = AMSLLC.Listener.Common.WNP.Model;

    /// <summary>
    /// Tests request constructor class methods.
    /// </summary>
    [TestClass]
    public class RequestConstructorTests
    {
        /// <summary>
        /// The default repair date time
        /// </summary>
        private DateTime defaultRepairDateTime = new DateTime(2014, 08, 24, 10, 25, 35);

        /// <summary>
        /// Tests if all the fields for meter test results are filled correctly.
        /// </summary>
        [TestMethod]
        public void AllFieldsForMeterTestResultsShouldBeFilledCorrectly()
        {
            RequestConstructor requestConstructor = null;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<WnpModel.Meter>((criteria) =>
                {
                    WnpModel.Meter meter = MotherObjects.DefaultMeter();
                    return meter;
                });
                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<WnpModel.MeterTestResult>((criteria) =>
                {                    
                    return CustomizedMeterTestResults();
                });
                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<WnpModel.Comment>((criteria) =>
                {
                    IList<WnpModel.Comment> comments = new List<WnpModel.Comment>();
                    comments.Add(new WnpModel.Comment() { CommentText = "Comment 1." });
                    comments.Add(new WnpModel.Comment() { CommentText = "Comment 2." });

                    return comments;
                });

                IWNPPersistenceController persistenceController = new WNPPersistenceController();
                persistenceController.InitializeListenerSystems(persistenceManager);
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                WNPSystem wnpSystem = persistenceController.WNPSystem;

                requestConstructor = new RequestConstructor(wnpSystem);
            }

            ListenerModel.DeviceTest deviceTest = DefaultDeviceTest();

            CreateDeviceTestResultABMType actualRequest = requestConstructor.PrepareElectricMeterTestResultsRequest(deviceTest);
            CreateDeviceTestResultABMType expectedRequest = this.DefaultMeterTestResultsRequest();

            // TODO: implement object comparer and compare full objects
            Assert.AreEqual(expectedRequest.Comments, actualRequest.Comments);
        }

        /// <summary>
        /// Tests fields are not included in request if they are empty strings.
        /// </summary>
        [TestMethod]
        public void FieldsShouldNotBeIncludedInRequestIfTheyAreEmptyStrings()
        {
            RequestConstructor requestConstructor = null;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<WnpModel.Meter>((criteria) =>
                {
                    WnpModel.Meter meter = MotherObjects.DefaultMeter();
                    return meter;
                });
                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<WnpModel.MeterTestResult>((criteria) =>
                {
                    IList<WnpModel.MeterTestResult> meterTestResults = CustomizedMeterTestResults();
                    foreach (WnpModel.MeterTestResult testStep in meterTestResults)
                    {
                        testStep.CustomField4 = " ";
                        testStep.CustomField5 = " ";
                    }

                    return meterTestResults;
                });
                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<WnpModel.Comment>((criteria) =>
                {
                    IList<WnpModel.Comment> comments = new List<WnpModel.Comment>();
                    comments.Add(new WnpModel.Comment() { CommentText = "Comment 1." });
                    comments.Add(new WnpModel.Comment() { CommentText = "Comment 2." });

                    return comments;
                });

                IWNPPersistenceController persistenceController = new WNPPersistenceController();
                persistenceController.InitializeListenerSystems(persistenceManager);
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                WNPSystem wnpSystem = persistenceController.WNPSystem;

                requestConstructor = new RequestConstructor(wnpSystem);
            }

            ListenerModel.DeviceTest deviceTest = DefaultDeviceTest();

            CreateDeviceTestResultABMType actualRequest = requestConstructor.PrepareElectricMeterTestResultsRequest(deviceTest);

            Assert.AreEqual(null, actualRequest.RetirementReason);
            Assert.AreEqual(null, actualRequest.RepairType);
        }

        /// <summary>
        /// Constructs the default device test.
        /// </summary>
        /// <returns>The device test.</returns>
        private static ListenerModel.DeviceTest DefaultDeviceTest()
        {
            ListenerModel.Company company = new ListenerModel.Company()
            {
                Id = 1,
                ExternalCode = "W",
                InternalCode = "0"
            };

            ListenerModel.ServiceType serviceType = new ListenerModel.ServiceType()
            {
                Id = 1,
                ExternalCode = "S"
            };

            ListenerModel.EquipmentType equipmentType = new ListenerModel.EquipmentType()
            {
                Id = 1,
                ExternalCode = "MR",
                InternalCode = "EM",
                ServiceType = serviceType
            };

            ListenerModel.Device device = new ListenerModel.Device()
            {
                Id = 1,
                Company = company,
                EquipmentNumber = "123456789",
                EquipmentType = equipmentType
            };

            ListenerModel.DeviceTest deviceTest = new ListenerModel.DeviceTest()
            {
                Id = 1,
                Device = device,
                TestDate = MotherObjects.DefaultTestStartDateTime
            };

            return deviceTest;
        }

        /// <summary>
        /// Construct the meter test results needed for current tests.
        /// </summary>
        /// <returns>The meter test results</returns>
        private IList<WnpModel.MeterTestResult> CustomizedMeterTestResults()
        {
            IList<WnpModel.MeterTestResult> testResults = MotherObjects.DefaultMeterTestResults();

            foreach (WnpModel.MeterTestResult testStep in testResults)
            {
                testStep.CustomField1 = "TestReason";
                testStep.CustomField2 = "RepairedBy";
                testStep.CustomField3 = this.defaultRepairDateTime.ToString();
                testStep.CustomField4 = "RepaireType";
                testStep.CustomField5 = "1"; // retirement reason
                testStep.CustomField6 = "13"; // creep code
                testStep.CustomField7 = "2"; // condition code
            }

            return testResults;
        }

        /// <summary>
        /// Constructs the default meter test results request.
        /// </summary>
        /// <returns>The meter test results request.</returns>
        private CreateDeviceTestResultABMType DefaultMeterTestResultsRequest()
        {
            CreateDeviceTestResultABMType meterTestResultsRequst = new CreateDeviceTestResultABMType()
            {
                ClassificationCode = "AA0",
                Comments = "Comment 1. Comment 2.",
                Company = "W",
                DeviceAttribute = new CreateDeviceTestResultABMTypeDeviceAttribute() 
                { 
                    ElectricDevice = new CreateDeviceTestResultABMTypeDeviceAttributeElectricDevice() 
                    {
                        AFL = (decimal)100.21,
                        AFLSpecified = true,
                        APF = (decimal)100.22,
                        APFSpecified = true,
                        BFL = (decimal)100.31,
                        BFLSpecified = true,
                        BPF = (decimal)100.33,
                        BPFSpecified = true,
                        CFL = (decimal)100.41,
                        CFLSpecified = true,
                        ConditionCode = 2,
                        ConditionCodeSpecified = true,
                        CPF = (decimal)100.43,
                        CPFSpecified = true,
                        CreepCode = "13",
                        SFL = (decimal)100.11,
                        SFLSpecified = true,
                        SLL = (decimal)100.13,
                        SLLSpecified = true,
                        SPF = (decimal)100.15,
                        SPFSpecified = true
                    }
                },
                DeviceNumber = "123456789",
                DeviceType = "MR",
                NewDeviceIndicator = "Y",
                RepairDateTime = this.defaultRepairDateTime,
                RepairDateTimeSpecified = true,
                RepairedBy = "RepairedBy",
                RepairType = "RepaireType",
                RetirementReason = "1",
                ServiceType = "S",
                Tester = "TesterID",
                TestLocation = "TestLocation",
                TestReason = "TestReason",
                TestStandard = "TestStandard",
                TestStartDateTime = MotherObjects.DefaultTestStartDateTime
            };

            return meterTestResultsRequst;
        }
    }
}

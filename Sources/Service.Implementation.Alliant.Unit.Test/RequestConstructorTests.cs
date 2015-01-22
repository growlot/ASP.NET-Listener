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
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Service.Implementation.Alliant;
    using AMSLLC.Listener.Service.Implementation.Alliant.GetDevice;
    using AMSLLC.Listener.Service.Implementation.Alliant.SendTestResult;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        /// The default test start date time
        /// </summary>
        private DateTime defaultTestStartDateTime = new DateTime(2014, 08, 24, 10, 25, 35);

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
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<Meter>((criteria) =>
                {
                    Meter meter = DefaultMeter();
                    return meter;
                });
                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<MeterTestResult>((criteria) =>
                {
                    IList<MeterTestResult> meterTestResults = new List<MeterTestResult>();
                    for (int id = 0; id < 10; id++)
                    {
                        meterTestResults.Add(this.DefaultMeterTestResult(id));
                    }

                    return meterTestResults;
                });
                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<Comment>((criteria) =>
                {
                    IList<Comment> comments = new List<Comment>();
                    comments.Add(new Comment() { CommentText = "Comment 1." });
                    comments.Add(new Comment() { CommentText = "Comment 2." });

                    return comments;
                });

                IWNPPersistenceController persistenceController = new WNPPersistenceController();
                persistenceController.InitializeListenerSystems(persistenceManager);
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                WNPSystem wnpSystem = persistenceController.WNPSystem;

                requestConstructor = new RequestConstructor(wnpSystem);
            }

            DeviceTest deviceTest = this.DefaultDeviceTest();

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
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<Meter>((criteria) =>
                {
                    Meter meter = DefaultMeter();
                    return meter;
                });
                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<MeterTestResult>((criteria) =>
                {
                    IList<MeterTestResult> meterTestResults = new List<MeterTestResult>();
                    for (int id = 0; id < 10; id++)
                    {
                        MeterTestResult meterTestResult = this.DefaultMeterTestResult(id);
                        meterTestResult.CustomField4 = " ";
                        meterTestResult.CustomField5 = " ";
                        meterTestResults.Add(meterTestResult);
                    }

                    return meterTestResults;
                });
                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<Comment>((criteria) =>
                {
                    IList<Comment> comments = new List<Comment>();
                    comments.Add(new Comment() { CommentText = "Comment 1." });
                    comments.Add(new Comment() { CommentText = "Comment 2." });

                    return comments;
                });

                IWNPPersistenceController persistenceController = new WNPPersistenceController();
                persistenceController.InitializeListenerSystems(persistenceManager);
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                WNPSystem wnpSystem = persistenceController.WNPSystem;

                requestConstructor = new RequestConstructor(wnpSystem);
            }

            DeviceTest deviceTest = this.DefaultDeviceTest();

            CreateDeviceTestResultABMType actualRequest = requestConstructor.PrepareElectricMeterTestResultsRequest(deviceTest);

            Assert.AreEqual(null, actualRequest.RetirementReason);
            Assert.AreEqual(null, actualRequest.RepairType);
        }

        /// <summary>
        /// Constructs the default meter object.
        /// </summary>
        /// <returns>The meter.</returns>
        private static Meter DefaultMeter()
        {
            Meter meter = new Meter()
            {
                Id = 1,
                MeterCode = "AA0",
                EquipmentNumber = "23456789",
                CustomField13 = "Y"
            };

            return meter;
        }

        /// <summary>
        /// Constructs the default device test.
        /// </summary>
        /// <returns>The device test.</returns>
        private DeviceTest DefaultDeviceTest()
        {
            Company company = new Company()
            {
                Id = 1,
                ExternalCode = "W",
                InternalCode = "0"
            };

            ServiceType serviceType = new ServiceType()
            {
                Id = 1,
                ExternalCode = "S"
            };

            EquipmentType equipmentType = new EquipmentType()
            {
                Id = 1,
                ExternalCode = "MR",
                InternalCode = "EM",
                ServiceType = serviceType
            };

            Device device = new Device()
            {
                Id = 1,
                Company = company,
                EquipmentNumber = "123456789",
                EquipmentType = equipmentType
            };

            DeviceTest deviceTest = new DeviceTest()
            {
                Id = 1,
                Device = device,
                TestDate = this.defaultTestStartDateTime
            };

            return deviceTest;
        }

        /// <summary>
        /// Construct the default meter test result.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The meter test result</returns>
        private MeterTestResult DefaultMeterTestResult(int id)
        {
            MeterTestResult meterTestResult = new MeterTestResult()
            {
                TesterId = "TesterID",
                Location = "TestLocation",
                TestStandard = "TestStandard",
                TestDate = this.defaultTestStartDateTime,
                CustomField1 = "TestReason",
                CustomField2 = "RepairedBy",
                CustomField3 = this.defaultRepairDateTime.ToString(),
                CustomField4 = "RepaireType",
                CustomField5 = "1", // retirement reason
                CustomField6 = "13", // creep code
                CustomField7 = "2" // condition code
            };

            switch (id)
            {
                case 1:
                    meterTestResult.Id = id;
                    meterTestResult.Element = 'S';
                    meterTestResult.TestType = "FL";
                    meterTestResult.AsFound = 100.10M;
                    meterTestResult.AsLeft = 100.11M;
                    break;
                case 2:
                    meterTestResult.Id = id;
                    meterTestResult.Element = 'S';
                    meterTestResult.TestType = "LL";
                    meterTestResult.AsFound = 100.12M;
                    meterTestResult.AsLeft = 100.13M;
                    break;
                case 3:
                    meterTestResult.Id = id;
                    meterTestResult.Element = 'S';
                    meterTestResult.TestType = "PF";
                    meterTestResult.AsFound = 100.14M;
                    meterTestResult.AsLeft = 100.15M;
                    break;

                case 4:
                    meterTestResult.Id = id;
                    meterTestResult.Element = 'A';
                    meterTestResult.TestType = "FL";
                    meterTestResult.AsFound = 100.20M;
                    meterTestResult.AsLeft = 100.21M;
                    break;
                case 5:
                    meterTestResult.Id = id;
                    meterTestResult.Element = 'A';
                    meterTestResult.TestType = "PF";
                    meterTestResult.AsFound = 100.24M;
                    meterTestResult.AsLeft = 100.25M;
                    break;

                case 6:
                    meterTestResult.Id = id;
                    meterTestResult.Element = 'B';
                    meterTestResult.TestType = "FL";
                    meterTestResult.AsFound = 100.30M;
                    meterTestResult.AsLeft = 100.31M;
                    break;
                case 7:
                    meterTestResult.Id = id;
                    meterTestResult.Element = 'B';
                    meterTestResult.TestType = "PF";
                    meterTestResult.AsFound = 100.34M;
                    meterTestResult.AsLeft = 100.35M;
                    break;

                case 8:
                    meterTestResult.Id = id;
                    meterTestResult.Element = 'C';
                    meterTestResult.TestType = "FL";
                    meterTestResult.AsFound = 100.40M;
                    meterTestResult.AsLeft = 100.41M;
                    break;
                case 9:
                    meterTestResult.Id = id;
                    meterTestResult.Element = 'C';
                    meterTestResult.TestType = "PF";
                    meterTestResult.AsFound = 100.44M;
                    meterTestResult.AsLeft = 100.45M;
                    break;
            }

            return meterTestResult;
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
                TestStartDateTime = this.defaultTestStartDateTime
            };

            return meterTestResultsRequst;
        }
    }
}

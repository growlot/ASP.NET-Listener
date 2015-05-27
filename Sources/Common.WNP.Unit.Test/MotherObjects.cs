//-----------------------------------------------------------------------
// <copyright file="MotherObjects.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Unit.Test
{
    using System;
    using System.Collections.Generic;
    using AMSLLC.Listener.Common.WNP.Model;

    /// <summary>
    /// Default test objects
    /// </summary>
    public static class MotherObjects
    {
        /// <summary>
        /// The default equipment create date time
        /// </summary>
        public static readonly DateTime DefaultEquipmentCreateDateTime = new DateTime(2014, 1, 20);
        
        /// <summary>
        /// The default test start date time
        /// </summary>
        public static readonly DateTime DefaultTestStartDateTime = new DateTime(2014, 2, 15);

        /// <summary>
        /// Constructs the default meter object.
        /// </summary>
        /// <returns>The meter.</returns>
        public static Meter DefaultMeter()
        {
            Meter meter = new Meter()
            {
                Id = 1,
                MeterCode = "AA0",
                EquipmentNumber = "123456789",
                CustomField13 = "Y",
                CreateDate = DefaultEquipmentCreateDateTime,
                Owner = DefaultOwner()
            };

            return meter;
        }

        /// <summary>
        /// Constructs the default owner object.
        /// </summary>
        /// <returns>The owner.</returns>
        public static Owner DefaultOwner()
        {
            Owner owner = new Owner()
            {
                Id = 1,
                Description = "OwnerName"
            };

            return owner;
        }

        /// <summary>
        /// Construct the default meter test result step.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The meter test result step</returns>
        public static MeterTestResult DefaultMeterTestResultStep(int id)
        {
            MeterTestResult meterTestResult = new MeterTestResult()
            {
                TesterId = "TesterID",
                Location = "TestLocation",
                TestStandard = "TestStandard",
                TestDate = DefaultTestStartDateTime,
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
        /// Construct the default meter test results.
        /// </summary>
        /// <returns>The meter test results</returns>
        public static IList<MeterTestResult> DefaultMeterTestResults()
        {
            IList<MeterTestResult> meterTestResults = new List<MeterTestResult>();
            for (int id = 0; id < 10; id++)
            {
                MeterTestResult meterTestResult = DefaultMeterTestResultStep(id);
                meterTestResults.Add(meterTestResult);
            }

            return meterTestResults;
        }
    }
}

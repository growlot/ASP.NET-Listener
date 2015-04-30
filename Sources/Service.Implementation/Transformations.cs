//-----------------------------------------------------------------------
// <copyright file="Transformations.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Service.Contract;

    /// <summary>
    /// Contains static methods for data transformations between various data structures.
    /// </summary>
    public static class Transformations
    {
        /// <summary>
        /// Creates the device receive service request.
        /// </summary>
        /// <param name="request">The device receive request.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// Instance of <see cref="GetDeviceServiceRequest" />
        /// </returns>
        /// <exception cref="ArgumentNullException">request;Can not transform data when initial data is not specified</exception>
        public static GetDeviceServiceRequest CreateDeviceReceiveServiceRequest(GetDeviceServiceRequest request, int transactionId, int deviceId)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not transform data when initial data is not specified");
            }

            GetDeviceServiceRequest result;
            result = new GetDeviceServiceRequest()
            {
                TransactionId = transactionId,
                DeviceId = deviceId,
                Location = request.Location,
                TesterId = request.TesterId,
                TestStandard = request.TestStandard
            };

            return result;
        }

        /// <summary>
        /// Creates the device shop test service request.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="meter">The meter.</param>
        /// <returns>
        /// The device shop service request.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// device;Can not transform data when initial data is not specified
        /// or
        /// meter;Can not transform data when initial data is not specified
        /// </exception>
        /// <exception cref="ArgumentNullException">device;Can not transform data when initial data is not specified
        /// or
        /// meter;Can not transform data when initial data is not specified</exception>
        public static SendDataServiceRequest CreateDeviceShopTestServiceRequest(Device device, int transactionId, Meter meter)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device", "Can not transform data when initial data is not specified");
            }

            if (meter == null)
            {
                throw new ArgumentNullException("meter", "Can not transform data when initial data is not specified");
            }

            //// MeterTestResult meterTest = meterTestResults.First<MeterTestResult>();

            SendDataServiceRequest serviceRequest = new SendDataServiceRequest()
            {
                TransactionId = transactionId,
                ////EquipmentNumber = meter.EquipmentNumber,
                ////ExternalCompanyId = device.Company.ExternalId,
                ////ExternalServiceType = "E",
                ////ExternalDeviceType = "MR",
                ////Location = meterTest.Location,
                ////TesterId = meterTest.TesterId,
                ////TestStandard = meterTest.TestStandard,
                ////NewDevice = (meter.CustomField3 == 'Y') ? true : false,
                ////TestDate = meterTest.TestDate,
                ////ReasonForTest = meterTest.CustomField1,
                ////RepairerId = meterTest.CustomField2,
                ////RepairDate = meterTest.CustomField3,
                ////RepairTypeId = meterTest.CustomField4,
                ////RetirementReasonId = meterTest.CustomField5,
                ////ClassificationCode = meter.MeterCode,
                ////MeterDetails = new MeterTestDetails()
                ////{
                ////    SeriesFullLoad = GetAsLeft(meterTestResults, 'S', "FL"),
                ////    SeriesLightLoad = GetAsLeft(meterTestResults, 'S', "LL"),
                ////    SeriesPowerFactor = GetAsLeft(meterTestResults, 'S', "PF"),
                ////    ElementAFullLoad = GetAsLeft(meterTestResults, 'A', "FL"),
                ////    ElementAPowerFactor = GetAsLeft(meterTestResults, 'A', "PF"),
                ////    ElementBFullLoad = GetAsLeft(meterTestResults, 'B', "FL"),
                ////    ElementBPowerFactor = GetAsLeft(meterTestResults, 'B', "PF"),
                ////    ElementCFullLoad = GetAsLeft(meterTestResults, 'C', "FL"),
                ////    ElementCPowerFactor = GetAsLeft(meterTestResults, 'C', "PF"),
                ////    Creep = meterTest.CustomField6,
                ////    ConditionCode = meterTest.CustomField7
                ////},
                ////Comments = string.Empty
            };

            return serviceRequest;
        }

        /// <summary>
        /// Gets the AsLeft test value for specific test result.
        /// </summary>
        /// <param name="meterTestResults">The list of meter test results.</param>
        /// <param name="element">The element.</param>
        /// <param name="testType">Type of the test.</param>
        /// <returns>
        /// Returns the AsLeft if specific element and testType exists in test result set. Returns specified defaultNotFound value otherwise.
        /// </returns>
        public static decimal? GetAsLeft(IList<MeterTestResult> meterTestResults, char element, string testType)
        {
            decimal result;

            try
            {
                MeterTestResult meterTestResult = meterTestResults.Single<MeterTestResult>(e => e.Element == element && e.TestType == testType);
                result = meterTestResult.AsLeft;
                result = Math.Round((decimal)result, 2, MidpointRounding.AwayFromZero);
            }
            catch (InvalidOperationException)
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Gets the AsLeft test value for specific test result.
        /// </summary>
        /// <param name="meterTestResults">The list of meter test results.</param>
        /// <param name="element">The element.</param>
        /// <param name="testType">Type of the test.</param>
        /// <returns>Returns the AsLeft if specific element and testType exists in test result set. Returns specified defaultNotFound value otherwise.</returns>
        public static decimal? GetAsFound(IList<MeterTestResult> meterTestResults, char element, string testType)
        {
            decimal result;

            try
            {
                MeterTestResult meterTestResult = meterTestResults.Single<MeterTestResult>(e => e.Element == element && e.TestType == testType);
                result = meterTestResult.AsFound;
                result = Math.Round((decimal)result, 2, MidpointRounding.AwayFromZero);
            }
            catch (InvalidOperationException)
            {
                return null;
            }

            return result;
        }
    }
}

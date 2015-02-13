//-----------------------------------------------------------------------
// <copyright file="IDeviceManager.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;

    /// <summary>
    /// Interface for device management.
    /// </summary>
    public interface IDeviceManager
    {
        /// <summary>
        /// Gets the device if it is already in database. Creates new if it is not.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns>The newly created or loaded from database device</returns>
        Device GetOrCreateDevice(Device device);

        /// <summary>
        /// Creates new device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns>
        /// The new device.
        /// </returns>
        Device NewDevice(Device device);

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="equipmentTypeId">The equipment type identifier.</param>
        /// <returns>
        /// The device.
        /// </returns>
        Device GetDevice(int companyId, string equipmentNumber, int equipmentTypeId);

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// The device.
        /// </returns>
        Device GetDevice(int deviceId);

        /// <summary>
        /// Gets the device test if it is already in database. Creates new if it is not.
        /// </summary>
        /// <param name="deviceTest">The device test.</param>
        /// <returns>
        /// The newly created or loaded from database device test
        /// </returns>
        DeviceTest GetOrCreateDeviceTest(DeviceTest deviceTest);

        /// <summary>
        /// Creates or updates the device test.
        /// </summary>
        /// <param name="deviceTest">The device test object.</param>
        /// <returns>
        /// The device test entity.
        /// </returns>
        DeviceTest SaveDeviceTest(DeviceTest deviceTest);

        /// <summary>
        /// Gets the device test.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="testDate">The test date.</param>
        /// <returns>Device test entry.</returns>
        DeviceTest GetDeviceTest(int deviceId, DateTime testDate);

        /// <summary>
        /// Gets the device test.
        /// </summary>
        /// <param name="deviceTestId">The device test identifier.</param>
        /// <returns>
        /// Device test entry.
        /// </returns>
        DeviceTest GetDeviceTest(int deviceTestId);

        /// <summary>
        /// Gets the equipment type by internal service and equipment type codes.
        /// </summary>
        /// <param name="serviceTypeInternalCode">The service type internal code.</param>
        /// <param name="equipmentTypeInternalCode">The equipment type internal code.</param>
        /// <returns>The equipment type</returns>
        EquipmentType GetEquipmentTypeByInternalCode(string serviceTypeInternalCode, string equipmentTypeInternalCode);

        /// <summary>
        /// Gets the company using internal code.
        /// </summary>
        /// <param name="internalCode">The internal code.</param>
        /// <returns>
        /// The company.
        /// </returns>
        Company GetCompanyByInternalCode(string internalCode);
        
        /// <summary>
        /// Gets all companies.
        /// </summary>
        /// <returns>
        /// Company list.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Method is more appropriate in this place, because data is queried from database")]
        IList<Company> GetCompanies();

        /// <summary>
        /// Gets the device batch.
        /// </summary>
        /// <param name="batchNumber">The batch number.</param>
        /// <returns>
        /// The device batch
        /// </returns>
        DeviceBatch GetDeviceBatchByBatchNumber(string batchNumber);

        /// <summary>
        /// Gets the device batch.
        /// </summary>
        /// <param name="deviceBatchId">The device batch identifier.</param>
        /// <returns>
        /// The device batch
        /// </returns>
        DeviceBatch GetDeviceBatch(int deviceBatchId);

        /// <summary>
        /// Creates or updates the device batch.
        /// </summary>
        /// <param name="deviceBatch">The device batch object.</param>
        /// <returns>
        /// The device batch entity.
        /// </returns>
        DeviceBatch SaveDeviceBatch(DeviceBatch deviceBatch);
    }
}

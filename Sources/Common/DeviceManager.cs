//-----------------------------------------------------------------------
// <copyright file="DeviceManager.cs" company="Advanced Metering Services LLC">
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
    using log4net;

    /// <summary>
    /// Implements device management interface.
    /// </summary>
    public class DeviceManager : IDeviceManager
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The listener system
        /// </summary>
        private ListenerSystem listenerSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceManager"/> class.
        /// </summary>
        /// <param name="persistenceController">The persistence controller.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when persistence controllers is not provided.
        /// </exception>
        public DeviceManager(IPersistenceController persistenceController)
        {
            if (persistenceController == null)
            {
                string exceptionMessage = "DeviceManager class can not be created because persistenceController is null.";
                Logger.Error(exceptionMessage);
                throw new ArgumentNullException("persistenceController", exceptionMessage);
            }

            this.listenerSystem = persistenceController.ListenerSystem;
        }

        /// <summary>
        /// Gets the device if it is already in database. Creates new if it is not.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns>
        /// The newly created or loaded from database device
        /// </returns>
        /// <exception cref="ArgumentNullException">device;Can not get or create device if it is not specified</exception>
        public Device GetOrCreateDevice(Device device)
        {
            if (device == null)
            {
                throw new ArgumentNullException("device", "Can not get or create device if it is not specified");
            }

            Device result = this.GetDevice(device.Company.Id, device.EquipmentNumber, device.EquipmentType.Id);
            if (result == null)
            {
                result = this.NewDevice(device);
            }

            return result;
        }

        /// <summary>
        /// Creates new device.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns>
        /// The new device.
        /// </returns>
        public Device NewDevice(Device device)
        {
            return this.listenerSystem.AddDevice(device);
        }

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="equipmentTypeId">The equipment type identifier.</param>
        /// <returns>
        /// The device.
        /// </returns>
        public Device GetDevice(int companyId, string equipmentNumber, int equipmentTypeId)
        {
            return this.listenerSystem.GetDevice(companyId, equipmentNumber, equipmentTypeId);
        }

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns>
        /// The device.
        /// </returns>
        public Device GetDevice(int deviceId)
        {
            return this.listenerSystem.GetDevice(deviceId);
        }

        /// <summary>
        /// Gets the device test if it is already in database. Creates new if it is not.
        /// </summary>
        /// <param name="deviceTest">The device test.</param>
        /// <returns>
        /// The newly created or loaded from database device test
        /// </returns>
        /// <exception cref="ArgumentNullException">deviceTest;Can not get or create device test if it is not specified</exception>
        public DeviceTest GetOrCreateDeviceTest(DeviceTest deviceTest)
        {
            if (deviceTest == null)
            {
                throw new ArgumentNullException("deviceTest", "Can not get or create device test if it is not specified");
            }

            DeviceTest result = this.GetDeviceTest(deviceTest.Device.Id, deviceTest.TestDate);
            if (result == null)
            {
                result = this.SaveDeviceTest(deviceTest);
            }

            return result;
        }

        /// <summary>
        /// Creates or updates the device test.
        /// </summary>
        /// <param name="deviceTest">The device test object.</param>
        /// <returns>
        /// The device test entity.
        /// </returns>
        public DeviceTest SaveDeviceTest(DeviceTest deviceTest)
        {
            return this.listenerSystem.SaveDeviceTest(deviceTest);
        }

        /// <summary>
        /// Gets the device test.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="testDate">The test date.</param>
        /// <returns>Device test entry.</returns>
        public DeviceTest GetDeviceTest(int deviceId, DateTime testDate)
        {
            return this.listenerSystem.GetDeviceTest(deviceId, testDate);
        }

        /// <summary>
        /// Gets the device test.
        /// </summary>
        /// <param name="deviceTestId">The device test identifier.</param>
        /// <returns>
        /// Device test entry.
        /// </returns>
        public DeviceTest GetDeviceTest(int deviceTestId)
        {
            return this.listenerSystem.GetDeviceTest(deviceTestId);
        }

        /// <summary>
        /// Gets the equipment type by internal service and equipment type codes.
        /// </summary>
        /// <param name="serviceTypeInternalCode">The service type internal code.</param>
        /// <param name="equipmentTypeInternalCode">The equipment type internal code.</param>
        /// <returns>The equipment type</returns>
        public EquipmentType GetEquipmentTypeByInternalCode(string serviceTypeInternalCode, string equipmentTypeInternalCode)
        {
            return this.listenerSystem.GetEquipmentTypeByInternalCode(serviceTypeInternalCode, equipmentTypeInternalCode);
        }

        /// <summary>
        /// Gets all the equipment types.
        /// </summary>
        /// <returns>The list of equipment types</returns>
        public IList<EquipmentType> GetEquipmentTypes()
        {
            return this.listenerSystem.GetEquipmentTypes();
        }

        /// <summary>
        /// Gets the company using internal code.
        /// </summary>
        /// <param name="internalCode">The internal code.</param>
        /// <returns>
        /// The company.
        /// </returns>
        public Company GetCompanyByInternalCode(string internalCode)
        {
            return this.listenerSystem.GetCompanyByInternalCode(internalCode);
        }

        /// <summary>
        /// Gets all companies.
        /// </summary>
        /// <returns>
        /// Company list.
        /// </returns>
        public IList<Company> GetCompanies()
        {
            return this.listenerSystem.GetCompanies();
        }

        /// <summary>
        /// Gets the device batch.
        /// </summary>
        /// <param name="batchNumber">The batch number.</param>
        /// <returns>
        /// The device batch
        /// </returns>
        public DeviceBatch GetDeviceBatchByBatchNumber(string batchNumber)
        {
            return this.listenerSystem.GetDeviceBatchByBatchNumber(batchNumber);
        }

        /// <summary>
        /// Gets the device batch.
        /// </summary>
        /// <param name="deviceBatchId">The device batch identifier.</param>
        /// <returns>
        /// The device batch
        /// </returns>
        public DeviceBatch GetDeviceBatch(int deviceBatchId)
        {
            return this.listenerSystem.GetDeviceBatch(deviceBatchId);
        }

        /// <summary>
        /// Creates or updates the device batch.
        /// </summary>
        /// <param name="deviceBatch">The device batch object.</param>
        /// <returns>
        /// The device batch entity.
        /// </returns>
        public DeviceBatch SaveDeviceBatch(DeviceBatch deviceBatch)
        {
            return this.listenerSystem.SaveDeviceBatch(deviceBatch);
        }
    }
}

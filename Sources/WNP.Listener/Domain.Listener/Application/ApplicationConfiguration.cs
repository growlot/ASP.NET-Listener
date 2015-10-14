//-----------------------------------------------------------------------
// <copyright file="ApplicationConfiguration.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Application
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using Lookups;

    /// <summary>
    /// Represents application configuration in listener transaction bounded context
    /// </summary>
    public class ApplicationConfiguration : Entity<int>, IAggregateRoot
    {
        /// <summary>
        /// The device types that are supported by this application.
        /// </summary>
        private ICollection<DeviceTypeLookup> supportedDeviceTypes;

        /// <summary>
        /// The transactions supporeted by this application.
        /// </summary>
        private ICollection<ApplicationTransaction> supportedTransactions;

        /// <summary>
        /// Enables the support of device type in this application.
        /// </summary>
        /// <param name="deviceTypeId">Type of the device represented as lookup id.</param>
        /// <exception cref="System.ArgumentException">Device Type is not recognized.;deviceTypeId</exception>
        /// <exception cref="System.InvalidOperationException">This Device Type is already supported by the application.</exception>
        public void EnableDeviceType(int deviceTypeId)
        {
            if (!Enum.IsDefined(typeof(DeviceTypeLookup), deviceTypeId) && deviceTypeId != (int)DeviceTypeLookup.Undefined)
            {
                throw new ArgumentException("Device Type is not recognized.", "deviceTypeId");
            }

            DeviceTypeLookup deviceType = (DeviceTypeLookup)deviceTypeId;

            if (this.supportedDeviceTypes.Contains(deviceType))
            {
                // exception is thrown in order to force correct usage. It is considered bad practice to send repeated requests.
                // if there is a legitimate business reason to support duplicate request, then this exception can be removed.
                throw new InvalidOperationException("This Device Type is already supported by the application.");
            }
            else
            {
                this.supportedDeviceTypes.Add(deviceType);
                EventsRegister.Raise(new DeviceTypeEnabledForApplication(this.Id, deviceTypeId));
            }
        }

        /// <summary>
        /// Disables the support of device type in this application.
        /// </summary>
        /// <param name="deviceTypeId">Type of the device represented as lookup id.</param>
        public void DisableDeviceType(int deviceTypeId)
        {
            if (!Enum.IsDefined(typeof(DeviceTypeLookup), deviceTypeId) && deviceTypeId != (int)DeviceTypeLookup.Undefined)
            {
                throw new ArgumentException("Device Type is not recognized.", "deviceTypeId");
            }

            DeviceTypeLookup deviceType = (DeviceTypeLookup)deviceTypeId;

            if (this.supportedDeviceTypes.Contains(deviceType))
            {
                // check if any of the supported transactions use this device type
                foreach (var supportedTransaction in this.supportedTransactions)
                {
                    if (supportedTransaction.SupportsDeviceType(deviceType))
                    {
                        string message = string.Format(CultureInfo.CurrentCulture, "Can not disable Device Type, because it is used by transaction {0}", supportedTransaction.Name);
                        throw new InvalidOperationException(message);
                    }
                }

                this.supportedDeviceTypes.Remove(deviceType);
                EventsRegister.Raise(new DeviceTypeDisabledForApplication(this.Id, deviceTypeId));
            }
            else
            {
                // exception is thrown in order to force correct usage. It is considered bad practice to send repeated requests.
                // if there is a legitimate business reason to support duplicate request, then this exception can be removed.
                throw new InvalidOperationException("This Device Type wasn't supported by the application.");
            }
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(IMemento memento)
        {
            var applicationConfigurationMemento = (ApplicationConfigurationMemento)memento;
            this.Id = applicationConfigurationMemento.Id;
            this.supportedDeviceTypes = applicationConfigurationMemento.SupportedDeviceTypes;
            this.supportedTransactions = new Collection<ApplicationTransaction>();
            foreach (var supportedTransaction in applicationConfigurationMemento.SupportedTransactions)
            {
                var applicationTransaction = new ApplicationTransaction();
                ((IOriginator)applicationTransaction).SetMemento(supportedTransaction);
                this.supportedTransactions.Add(applicationTransaction);
            }
        }
    }
}

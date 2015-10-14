//-----------------------------------------------------------------------
// <copyright file="ApplicationTransaction.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Application
{
    using System;
    using System.Collections.Generic;
    using Lookups;

    /// <summary>
    /// Transaction supported by specific application
    /// </summary>
    public class ApplicationTransaction : Entity<int>
    {
        /// <summary>
        /// The device types supported by this transaction type.
        /// </summary>
        private ICollection<DeviceTypeLookup> supportedDeviceTypes;

        /// <summary>
        /// Gets the transaction name.
        /// </summary>
        /// <value>
        /// The transaction name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Checks if this transaction supports specified device type.
        /// </summary>
        /// <param name="deviceType">Type of the device.</param>
        /// <returns>True if transaction supports specified device type, False othervise.</returns>
        public bool SupportsDeviceType(DeviceTypeLookup deviceType)
        {
            return this.supportedDeviceTypes.Contains(deviceType);
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(IMemento memento)
        {
            var applicationTransactionMemento = (ApplicationTransactionMemento)memento;
            this.Id = applicationTransactionMemento.Id;
            this.Name = applicationTransactionMemento.Name;
            this.supportedDeviceTypes = applicationTransactionMemento.SupportedDeviceTypes;
        }
    }
}

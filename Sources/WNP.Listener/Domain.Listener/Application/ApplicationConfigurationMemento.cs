//-----------------------------------------------------------------------
// <copyright file="ApplicationConfigurationMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Application
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Lookups;

    /// <summary>
    /// Memento class for application
    /// </summary>
    public class ApplicationConfigurationMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurationMemento" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="supportedDeviceTypes">The supported device types.</param>
        /// <param name="supportedTransactions">The supported transactions.</param>
        public ApplicationConfigurationMemento(int id, ICollection<int> supportedDeviceTypes, IEnumerable<ApplicationTransactionMemento> supportedTransactions)
        {
            if (supportedDeviceTypes == null)
            {
                throw new ArgumentNullException("supportedDeviceTypes", "Application must support at least one device type.");
            }

            this.Id = id;
            this.SupportedTransactions = supportedTransactions;

            this.SupportedDeviceTypes = new Collection<DeviceTypeLookup>();
            foreach (var deviceType in supportedDeviceTypes)
            {
                // no validation is done, becasue memento is loaded from database, and database should always be in consisten state.
                this.SupportedDeviceTypes.Add((DeviceTypeLookup)deviceType);
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        internal int Id { get; private set; }

        /// <summary>
        /// Gets the device types that this application is using.
        /// </summary>
        /// <value>
        /// The supported services.
        /// </value>
        internal ICollection<DeviceTypeLookup> SupportedDeviceTypes { get; private set; }

        /// <summary>
        /// Gets the transactions supporeted by this application.
        /// </summary>
        /// <value>
        /// The supported transactions.
        /// </value>
        internal IEnumerable<ApplicationTransactionMemento> SupportedTransactions { get; private set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="DeviceTypeEnabledForApplication.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Application
{
    /// <summary>
    /// The event that enables device type for application
    /// </summary>
    public class DeviceTypeEnabledForApplication : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceTypeEnabledForApplication" /> class.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="deviceTypeId">The device type identifier.</param>
        public DeviceTypeEnabledForApplication(int applicationId, int deviceTypeId)
        {
            this.ApplicationId = applicationId;
            this.DeviceTypeId = deviceTypeId;
        }

        /// <summary>
        /// Gets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationId { get; private set; }

        /// <summary>
        /// Gets the device type identifier.
        /// </summary>
        /// <value>
        /// The device type identifier.
        /// </value>
        public int DeviceTypeId { get; private set; }
    }
}

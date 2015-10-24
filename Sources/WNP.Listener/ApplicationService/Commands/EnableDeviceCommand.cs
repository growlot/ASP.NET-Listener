//-----------------------------------------------------------------------
// <copyright file="EnableDeviceCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.Commands
{
    using Domain;
    using Domain.Listener.Lookups;

    /// <summary>
    /// Information needed enabling device type in application.
    /// </summary>
    public class EnableDeviceCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the type of the device.
        /// </summary>
        /// <value>
        /// The type of the device.
        /// </value>
        public DeviceTypeLookup DeviceType { get; set; }
    }
}

// //-----------------------------------------------------------------------
// // <copyright file="IApplicationConfigurationService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

using AMSLLC.Listener.Domain.Listener.Lookups;

namespace AMSLLC.Listener.ApplicationService
{
    public interface IApplicationConfigurationService
    {
        /// <summary>
        /// Enable the device.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="deviceType">Type of the device.</param>
        void EnableDevice(int applicationId, DeviceTypeLookup deviceType);
    }
}
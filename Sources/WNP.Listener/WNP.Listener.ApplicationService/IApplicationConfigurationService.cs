// //-----------------------------------------------------------------------
// // <copyright file="IApplicationConfigurationService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace WNP.Listener.ApplicationService
{
    using AMSLLC.Listener.Domain.Listener.Lookups;

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
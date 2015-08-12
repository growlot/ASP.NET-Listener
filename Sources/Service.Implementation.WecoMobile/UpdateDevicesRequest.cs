//-----------------------------------------------------------------------
// <copyright file="UpdateDevicesRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using System.Collections.Generic;
    using Common.Model;

    /// <summary>
    /// Devices message for web service
    /// </summary>
    public class UpdateDevicesRequest
    {
        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        /// <value>
        /// The devices.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is needed for WCF access")]
        public ICollection<UpdateDeviceRequest> Devices { get; set; }
    }
}
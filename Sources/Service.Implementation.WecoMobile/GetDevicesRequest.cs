//-----------------------------------------------------------------------
// <copyright file="GetDevicesRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using System.Collections.Generic;

    /// <summary>
    /// Get devices request message format
    /// </summary>
    public class GetDevicesRequest
    {
        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        /// <value>
        /// The devices.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is needed for WCF access")]
        public ICollection<InventoryItem> Devices { get; set; }
    }
}
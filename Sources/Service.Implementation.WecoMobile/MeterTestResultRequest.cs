//-----------------------------------------------------------------------
// <copyright file="MeterTestResultRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using AMSLLC.Listener.Common.Model;

    /// <summary>
    /// Meter test result message format
    /// </summary>
    public class MeterTestResultRequest
    {
        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the meter test.
        /// </summary>
        /// <value>
        /// The meter test.
        /// </value>
        public DeviceTest MeterTest { get; set; }
    }
}
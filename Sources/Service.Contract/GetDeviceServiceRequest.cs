//-----------------------------------------------------------------------
// <copyright file="GetDeviceServiceRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Contract
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Data contract for device receive service request.
    /// </summary>
    [DataContract]
    public class GetDeviceServiceRequest
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        [DataMember]
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        [DataMember]
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [DataMember]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the tester identifier.
        /// </summary>
        /// <value>
        /// The tester identifier.
        /// </value>
        [DataMember]
        public string TesterId { get; set; }

        /// <summary>
        /// Gets or sets the test standard.
        /// </summary>
        /// <value>
        /// The test standard.
        /// </value>
        [DataMember]
        public string TestStandard { get; set; }
     }
}

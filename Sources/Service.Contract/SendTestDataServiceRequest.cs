﻿//-----------------------------------------------------------------------
// <copyright file="SendTestDataServiceRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Contract
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Data contract for device receive service response. 
    /// </summary>
    [DataContract]
    public class SendTestDataServiceRequest
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
        [Obsolete("Device can be extracted from DeviceTestId.")]
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the device test identifier.
        /// </summary>
        /// <value>
        /// The device test identifier.
        /// </value>
        [DataMember]
        public int DeviceTestId { get; set; }
    }
}

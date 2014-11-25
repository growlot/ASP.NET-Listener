//-----------------------------------------------------------------------
// <copyright file="ServiceFaultDetails.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Contract
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Contract to transfer fault details between Listener service and Listener client
    /// </summary>
    [DataContract]
    public class ServiceFaultDetails
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the debug information.
        /// </summary>
        /// <value>
        /// The debug information.
        /// </value>
        [DataMember]
        public string DebugInfo { get; set; }
    }
}

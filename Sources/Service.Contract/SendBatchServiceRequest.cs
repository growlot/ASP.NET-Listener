//-----------------------------------------------------------------------
// <copyright file="SendBatchServiceRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Contract
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Data contract for batch sending service request. 
    /// </summary>
    [DataContract]
    public class SendBatchServiceRequest
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
        /// Gets or sets the batch number.
        /// </summary>
        /// <value>
        /// The batch number.
        /// </value>
        [DataMember]
        public string BatchNumber { get; set; }
    }
}
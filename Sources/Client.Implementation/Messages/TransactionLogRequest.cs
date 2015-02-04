//-----------------------------------------------------------------------
// <copyright file="TransactionLogRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation.Messages
{
    using System;

    /// <summary>
    /// Request message type for transaction log used in WNP
    /// </summary>
    public class TransactionLogRequest
    {
        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public string EquipmentType { get; set; }

        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        public string ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the log date.
        /// </summary>
        /// <value>
        /// The log date.
        /// </value>
        public DateTime? LogDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include error message and debug info.
        /// </summary>
        /// <value>
        ///   <c>true</c> if error message and debug info should be included in response; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether only failed logs should be queried.
        /// </summary>
        /// <value>
        ///   <c>true</c> if only failed logs should be queried; otherwise, <c>false</c>.
        /// </value>
        public bool FailedOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether skipped transactions should be included in the log output.
        /// </summary>
        /// <value>
        ///   <c>true</c> if skipped transactions are included in the log output; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeSkipped { get; set; }
    }
}

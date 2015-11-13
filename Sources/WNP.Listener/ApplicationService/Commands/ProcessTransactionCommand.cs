// //-----------------------------------------------------------------------
// <copyright file="ProcessTransactionCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.Commands
{
    using System;
    using Domain.Listener.Transaction;

    /// <summary>
    /// Process transaction command
    /// </summary>
    public class ProcessTransactionCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the record key.
        /// </summary>
        /// <value>
        /// The record key.
        /// </value>
        public Guid RecordKey { get; set; }

        /// <summary>
        /// Gets or sets the retry policy type.
        /// </summary>
        /// <value>The retry.</value>
        public RetryPolicyType RetryPolicy { get; set; }
    }
}
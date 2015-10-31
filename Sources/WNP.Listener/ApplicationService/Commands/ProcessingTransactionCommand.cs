// <copyright file="ProcessingTransactionCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Commands
{
    using System;

    /// <summary>
    /// Processing transaction message
    /// </summary>
    public class ProcessingTransactionCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the record key.
        /// </summary>
        /// <value>The record key.</value>
        public Guid RecordKey { get; set; }
    }
}
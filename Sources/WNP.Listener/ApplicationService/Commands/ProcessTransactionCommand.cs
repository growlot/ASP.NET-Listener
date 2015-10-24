// //-----------------------------------------------------------------------
// <copyright file="ProcessTransactionCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.Commands
{
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
        public string RecordKey { get; set; }
    }
}
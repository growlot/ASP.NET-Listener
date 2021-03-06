﻿// //-----------------------------------------------------------------------
// <copyright file="SucceedTransactionCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.Commands
{
    using System;

    /// <summary>
    /// Succeed transaction command
    /// </summary>
    public class SucceedTransactionCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the record key.
        /// </summary>
        /// <value>
        /// The record key.
        /// </value>
        public Guid RecordKey { get; set; }
    }
}
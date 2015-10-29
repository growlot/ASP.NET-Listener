// <copyright file="OpenBatchTransactionCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Commands
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Model;

    /// <summary>
    /// Open batch transaction command
    /// </summary>
    public class OpenBatchTransactionCommand
    {
        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        /// <value>The company code.</value>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Gets or sets the source application key.
        /// </summary>
        /// <value>The source application key.</value>
        public string SourceApplicationKey { get; set; }

        /// <summary>
        /// Gets or sets the operation key.
        /// </summary>
        /// <value>The operation key.</value>
        public string OperationKey { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; set; }

        /// <summary>
        /// Gets the batch.
        /// </summary>
        /// <value>The batch.</value>
        public Collection<BatchTransactionEntry> Batch { get; } = new Collection<BatchTransactionEntry>();
    }
}
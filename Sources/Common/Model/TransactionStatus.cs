//-----------------------------------------------------------------------
// <copyright file="TransactionStatus.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data model class representing TransactionStatus
    /// </summary>
    public class TransactionStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionStatus"/> class.
        /// </summary>
        public TransactionStatus()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionStatus"/> class.
        /// </summary>
        /// <param name="id">The transaction status identifier.</param>
        public TransactionStatus(int id)
        {
            this.Id = id;
            this.Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the transaction status identifier.
        /// </summary>
        /// <value>
        /// The transaction status identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction status description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}

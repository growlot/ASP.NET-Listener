//-----------------------------------------------------------------------
// <copyright file="TransactionData.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Data model class representing TransactionData
    /// </summary>
    public class TransactionData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionData"/> class.
        /// </summary>
        public TransactionData()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionData"/> class.
        /// </summary>
        /// <param name="id">The transaction data identifier.</param>
        public TransactionData(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the transaction data identifier.
        /// </summary>
        /// <value>
        /// The transaction data identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction data description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}

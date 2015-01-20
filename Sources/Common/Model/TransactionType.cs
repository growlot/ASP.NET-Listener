//-----------------------------------------------------------------------
// <copyright file="TransactionType.cs" company="Advanced Metering Services LLC">
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
    /// Data model class representing TransactionType 
    /// </summary>
    public class TransactionType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionType"/> class.
        /// </summary>
        public TransactionType()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionType"/> class.
        /// </summary>
        /// <param name="id">The transaction type identifier.</param>
        public TransactionType(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the transaction type identifier.
        /// </summary>
        /// <value>
        /// The transaction type identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction type name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the transaction type description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the external system.
        /// </summary>
        /// <value>
        /// The external system.
        /// </value>
        public ExternalSystem ExternalSystem { get; set; }

        /// <summary>
        /// Gets or sets the transaction data.
        /// </summary>
        /// <value>
        /// The transaction data.
        /// </value>
        public TransactionData TransactionData { get; set; }

        /// <summary>
        /// Gets or sets the transaction direction.
        /// </summary>
        /// <value>
        /// The transaction direction.
        /// </value>
        public TransactionDirection TransactionDirection { get; set; }

        /// <summary>
        /// Gets or sets the transaction source.
        /// </summary>
        /// <value>
        /// The transaction source.
        /// </value>
        public TransactionSource TransactionSource { get; set; }
    }
}

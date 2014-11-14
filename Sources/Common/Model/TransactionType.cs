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
            this.Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the transaction type identifier.
        /// </summary>
        /// <value>
        /// The transaction type identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction type description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}

//-----------------------------------------------------------------------
// <copyright file="TransactionState.cs" company="Advanced Metering Services LLC">
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
    /// Data model class representing TransactionState
    /// </summary>
    public class TransactionState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionState"/> class.
        /// </summary>
        public TransactionState()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionState"/> class.
        /// </summary>
        /// <param name="id">The transaction state identifier.</param>
        public TransactionState(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the transaction state identifier.
        /// </summary>
        /// <value>
        /// The transaction state identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction state description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
